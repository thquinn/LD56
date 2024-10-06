using Assets.Code;
using Assets.Code.Model;
using Assets.Code.Model.Creatures;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    static InteractionScript instance;

    public GameObject prefabCreatureParty;

    public UIExpeditionPanelScript expeditionScript;

    Game game;
    BoardScript boardScript;
    Entity grabbedEntity;
    CreaturePartyScript floatingParty;

    void Start() {
        instance = this;
        game = GameManagerScript.instance.game;
        boardScript = BoardScript.instance;
    }

    void Update() {
        if (!GameManagerScript.IsInteractable()) return;
        if (Input.GetMouseButtonDown(0)) {
            UpdateMouseDown();
        }
        if (Input.GetMouseButtonUp(0)) {
            UpdateMouseUp();
        }
    }
    void UpdateMouseDown() {
        if (UIExpeditionPanelScript.IsActive() && boardScript.hoveredFogTile != null) {
            UIExpeditionPanelScript.Toggle(boardScript.hoveredFogTile);
        } else if (grabbedEntity == null) {
            grabbedEntity = boardScript.hoveredTile?.entity;
        }
    }
    void UpdateMouseUp() {
        if (grabbedEntity != null) {
            Tile targetTile = boardScript.hoveredTile;
            if (targetTile != grabbedEntity.tile && targetTile?.CanBeMovedTo() == true) {
                Party party = grabbedEntity as Party;
                // Move the entity.
                if (party.tile != null) {
                    boardScript.hoveredTile.MoveEntityHereSlow(grabbedEntity, 1);
                } else if (targetTile.GetNeighbors().Any(t => t.entity?.HasAbility(CreatureAbilityHome.NAME) == true)) {
                    List<Creature> shopCreatures = game.shop.creatures;
                    int shopIndex = shopCreatures.IndexOf(party.creatures[0]);
                    Debug.Assert(shopIndex >= 0, "Failed to find purchased creature in the shop.");
                    if (game.money >= shopCreatures[shopIndex].cost) {
                        game.shop.Buy(shopIndex);
                        boardScript.hoveredTile.MoveEntityHereImmediate(grabbedEntity);
                    }
                }
            }
            if (targetTile.distanceToRevealed == 1 && grabbedEntity.CanExplore(targetTile)) {
                // Start an expedition.
                UIExpeditionPanelScript.StartExpedition(grabbedEntity as Party, targetTile);
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
    public static Entity GetGrabbed() {
        return instance.grabbedEntity;
    }
    public static void SetGrabbed(Party party) {
        instance.SetGrabbedImpl(party);
    }
    void SetGrabbedImpl(Party party) {
        grabbedEntity = party;
        if (party.tile == null) {
            floatingParty = Instantiate(prefabCreatureParty, transform).GetComponent<CreaturePartyScript>();
            floatingParty.Init(party);
        }
    }
    public static bool IsSpawningNewParty() {
        return instance.floatingParty != null;
    }
}
