using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnitScript : MonoBehaviour
{
    public GameObject goSprites, goRing, goAttack, goCost;

    Party party;

    void Start() {
        Init(new Party(new Creature(3)));
    }
    public void Init(Party party) {
        this.party = party;
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
