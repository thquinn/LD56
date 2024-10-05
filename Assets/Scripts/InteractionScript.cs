using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    BoardScript boardScript;
    Entity grabbedEntity;

    void Start() {
        boardScript = BoardScript.instance;
    }

    void Update() {
        if (!GameManagerScript.IsUnpaused()) return;
        if (Input.GetMouseButtonDown(0)) {
            grabbedEntity = boardScript.hoveredTile?.entity;
        }
        if (Input.GetMouseButtonUp(0) && grabbedEntity != null) {
            Tile targetTile = boardScript.hoveredTile;
            if (targetTile != grabbedEntity.tile && targetTile?.CanBeMovedTo() == true) {
                boardScript.hoveredTile.MoveEntityHereSlow(grabbedEntity, 1);
            }
            grabbedEntity = null;
        }
    }
}
