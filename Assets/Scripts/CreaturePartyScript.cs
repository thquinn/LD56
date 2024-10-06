using Assets.Code;
using Assets.Code.Model;
using Assets.Code.Model.Creatures;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreaturePartyScript : EntityScript<Party>
{
    static float CREATURE_SPACING = .14f;
    static float CREATURE_SCALING = .3f;
    static float MOVE_ANIMATION_Y = .66f;
    static float MOVE_ANIMATION_TIME = .075f;

    public GameObject prefabCreature;

    public GameObject ring;
    public TextMeshPro tmpAttack;

    Party party;
    Dictionary<Creature, CreatureScript> creatureScripts;
    PauseSource pauseSource;
    Tile lastTile;
    float dy;
    float randomAngle;
    float initialAttackTextSize;

    void Start() {
        creatureScripts = new Dictionary<Creature, CreatureScript>();
        pauseSource = GameManagerScript.GetPauseSource();
        randomAngle = Random.Range(0, 2 * Mathf.PI);
        initialAttackTextSize = tmpAttack.fontSize;
    }

    public override EntityScript<Party> Init(Party party) {
        this.party = party;
        return this;
    }

    void Update() {
        if (party.isDead) {
            Destroy(gameObject);
            return;
        }
        foreach (Creature creature in party.creatures) {
            if (creatureScripts.ContainsKey(creature)) continue;
            CreatureScript creatureScript = Instantiate(prefabCreature, transform).GetComponent<CreatureScript>();
            creatureScript.Init(creature);
            creatureScripts[creature] = creatureScript;
        }
        // Position party members.
        float creatureScale = Mathf.Exp(-Mathf.Sqrt(party.creatures.Count - 1) * CREATURE_SCALING);
        float creatureRadius = Mathf.Pow(party.creatures.Count - 1, .25f) * CREATURE_SPACING;
        for (int i = 0; i < party.creatures.Count; i++) {
            float theta = randomAngle + i * 2 * Mathf.PI / party.creatures.Count;
            Transform t = creatureScripts[party.creatures[i]].transform;
            t.localPosition = new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta)) * creatureRadius;
            t.localScale = new Vector3(creatureScale, creatureScale, creatureScale);
        }
        // Interactions.
        if (InteractionScript.IsGrabbed(party)) {
            transform.localPosition = Util.GetMouseHoverCoordinate(-.5f);
        } else if (party.tile != null || party.tileMovingFrom != null) {
            Tile tile = party.tile ?? party.tileMovingFrom;
            Vector3 localPosition = Util.BoardCoorToWorldCoor(tile.coor);
            localPosition.y = transform.localPosition.y;
            transform.localPosition = localPosition;
            if (tile != lastTile) {
                dy = 0;
                lastTile = party.tile;
            }
        }
        // Move animation.
        bool moving = party.tileMovingTo != null && transform.localPosition.y < MOVE_ANIMATION_Y * .99f;
        transform.SetLocalY(Mathf.SmoothDamp(transform.localPosition.y, moving ? MOVE_ANIMATION_Y : 0, ref dy, MOVE_ANIMATION_TIME));
        pauseSource.Set(moving);
        // Attack ring.
        ring.SetActive(transform.localPosition.y < .01f);
        int attack = party.GetAttack();
        tmpAttack.text = attack.ToString();
        tmpAttack.fontSize = initialAttackTextSize * Util.IntToDisplayStringScale(attack);
    }
}
