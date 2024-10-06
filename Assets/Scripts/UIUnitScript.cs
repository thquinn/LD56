using Assets.Code.Model;
using Assets.Code.Model.Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnitScript : MonoBehaviourWithCreature {
    public GameObject goSprites, goRing, goAttack, goCost;

    Party party;

    void Start() {
        Init(new CreatureQueen());
    }
    public void Init(Creature creature) {
        this.creature = creature;
        party = new Party(creature);
    }

    void Update() {
        if (party == null) return;
        if (party.tile != null) {
            // This unit has been placed.
            party = null;
        }
        goSprites.SetActive(!InteractionScript.IsGrabbed(party));
        goRing.SetActive(!InteractionScript.IsGrabbed(party));
        goAttack.SetActive(!InteractionScript.IsGrabbed(party));
        goCost.SetActive(party != null);
    }

    public void OnClick() {
        if (party != null) {
            InteractionScript.SetGrabbed(party);
        }
    }
}
