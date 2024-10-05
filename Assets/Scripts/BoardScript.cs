using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public static BoardScript instance;

    public GameObject prefabTile;

    public LayerMask layerMaskTile;

    Board board;
    Dictionary<Collider, TileScript> tileScripts;
    public Tile hoveredTile;

    void Start() {
        instance = this;
        board = GameManagerScript.instance.game.board;
        tileScripts = new Dictionary<Collider, TileScript>();
    }

    void Update() {
        foreach (Tile tile in board.GetTilesFromIndex(tileScripts.Count)) {
            TileScript tileScript = Instantiate(prefabTile, transform).GetComponent<TileScript>();
            tileScript.Init(tile);
            tileScripts[tileScript.GetComponentInChildren<Collider>()] = tileScript;
        }
        Collider hoveredCollider = Util.GetMouseCollider(layerMaskTile);
        hoveredTile = hoveredCollider == null ? null : tileScripts[hoveredCollider].tile;
    }
}
