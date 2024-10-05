using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePartyScript : EntityScript<CreatureParty>
{
    public GameObject prefabCreature;

    CreatureParty party;
    Dictionary<Creature, CreatureScript> creatureScripts;

    void Start() {
        creatureScripts = new Dictionary<Creature, CreatureScript>();
    }

    public override EntityScript<CreatureParty> Init(CreatureParty party) {
        this.party = party;
        return this;
    }

    void Update() {
        transform.localPosition = Util.BoardCoorToWorldCoor(party.tile.coor);
        foreach (Creature creature in party.creatures) {
            if (creatureScripts.ContainsKey(creature)) continue;
            CreatureScript creatureScript = Instantiate(prefabCreature, transform).GetComponent<CreatureScript>();
            creatureScript.Init(creature);
            creatureScripts[creature] = creatureScript;
        }
    }
}
