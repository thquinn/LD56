using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    static InteractionScript instance;

    public GameObject prefabCreatureParty;

    BoardScript boardScript;
    public Entity grabbedEntity;
    CreaturePartyScript floatingParty;

    void Start() {
        instance = this;
        boardScript = BoardScript.instance;
    }

    void Update() {
        if (!GameManagerScript.IsUnpaused()) return;
        if (Input.GetMouseButtonDown(0) && grabbedEntity == null) {
            grabbedEntity = boardScript.hoveredTile?.entity;
        }
        if (Input.GetMouseButtonUp(0) && grabbedEntity != null) {
            Tile targetTile = boardScript.hoveredTile;
            if (targetTile != grabbedEntity.tile && targetTile?.CanBeMovedTo() == true) {
                // Move the entity.
                if ((grabbedEntity as CreatureParty).tile != null) {
                    boardScript.hoveredTile.MoveEntityHereSlow(grabbedEntity, 1);
                } else {
                    boardScript.hoveredTile.MoveEntityHereImmediate(grabbedEntity);
                }
            }
            grabbedEntity = null;
            if (floatingParty != null) {
                Destroy(floatingParty.gameObject);
                floatingParty = null;
            }
        }
    }

    public static bool IsGrabbed(Entity entity) {
        return instance.grabbedEntity == entity;
    }
    public static bool IsGrabbed(Creature creature) {
        return creature.party == instance.grabbedEntity;
    }
    public static void SetGrabbed(CreatureParty party) {
        instance.SetGrabbedImpl(party);
    }
    void SetGrabbedImpl(CreatureParty party) {
        grabbedEntity = party;
        if (party.tile == null) {
            floatingParty = Instantiate(prefabCreatureParty, transform).GetComponent<CreaturePartyScript>();
            floatingParty.Init(party);
        }
    }
}
