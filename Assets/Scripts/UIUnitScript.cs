using Assets.Code.Model;
using Assets.Code.Model.Creatures;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUnitScript : MonoBehaviourWithCreature {
    public GameObject goSprites, goRing, goAttack, goCost;
    public TextMeshProUGUI tmpAttack, tmpCost;

    Game game;

    int shopIndex;
    Party party;

    public void Init(int shopIndex) {
        game = GameManagerScript.instance.game;
        this.shopIndex = shopIndex;
    }

    void Update() {
        if (game.shop.creatures[shopIndex] != creature) {
            creature = game.shop.creatures[shopIndex];
            party = creature == null ? null : new Party(creature);
            if (creature != null) {
                tmpAttack.text = creature.GetAttack().ToString();
                tmpCost.text = creature.cost.ToString();
            }
        }
        goSprites.SetActive(creature != null && !InteractionScript.IsGrabbed(party));
        goRing.SetActive(creature != null && !InteractionScript.IsGrabbed(party));
        goAttack.SetActive(creature != null && !InteractionScript.IsGrabbed(party));
        goCost.SetActive(party != null);
    }

    public void OnClick() {
        if (party != null && game.money >= creature.cost) {
            InteractionScript.SetGrabbed(party);
        }
    }
}
