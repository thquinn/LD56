using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePartyScript : EntityScript<Party>
{
    static float MOVE_ANIMATION_Y = .66f;
    static float MOVE_ANIMATION_TIME = .075f;

    public GameObject prefabCreature;

    public GameObject ring;

    Party party;
    Dictionary<Creature, CreatureScript> creatureScripts;
    PauseSource pauseSource;
    Tile lastTile;
    float dy;

    void Start() {
        creatureScripts = new Dictionary<Creature, CreatureScript>();
        pauseSource = GameManagerScript.GetPauseSource();
    }

    public override EntityScript<Party> Init(Party party) {
        this.party = party;
        return this;
    }

    void Update() {
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
        foreach (Creature creature in party.creatures) {
            if (creatureScripts.ContainsKey(creature)) continue;
            CreatureScript creatureScript = Instantiate(prefabCreature, transform).GetComponent<CreatureScript>();
            creatureScript.Init(creature);
            creatureScripts[creature] = creatureScript;
        }
        // Move animation.
        bool moving = party.tileMovingTo != null && transform.localPosition.y < MOVE_ANIMATION_Y * .99f;
        transform.SetLocalY(Mathf.SmoothDamp(transform.localPosition.y, moving ? MOVE_ANIMATION_Y : 0, ref dy, MOVE_ANIMATION_TIME));
        pauseSource.Set(moving);
        // Attack ring.
        ring.SetActive(transform.localPosition.y < .01f);
    }
}
