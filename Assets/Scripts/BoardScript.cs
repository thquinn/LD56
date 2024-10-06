using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public static BoardScript instance;

    public GameObject prefabTile;

    public LayerMask layerMaskTile, layerMaskFog;

    Board board;
    Dictionary<Collider, TileScript> colliderToTile;
    public Tile hoveredTile, hoveredFogTile;
    int tileScriptCount;

    void Start() {
        instance = this;
        board = GameManagerScript.instance.game.board;
        colliderToTile = new Dictionary<Collider, TileScript>();
    }

    void Update() {
        foreach (Tile tile in board.GetTilesFromIndex(tileScriptCount)) {
            TileScript tileScript = Instantiate(prefabTile, transform).GetComponent<TileScript>();
            tileScript.Init(tile);
            colliderToTile[tileScript.colliderTile] = tileScript;
            colliderToTile[tileScript.colliderFog] = tileScript;
            tileScriptCount++;
        }
        Collider hoveredTileCollider = Util.GetMouseCollider(layerMaskTile);
        Collider hoveredFogCollider = Util.GetMouseCollider(layerMaskFog);
        hoveredTile = hoveredTileCollider == null ? null : colliderToTile[hoveredTileCollider].tile;
        hoveredFogTile = hoveredFogCollider == null ? null : colliderToTile[hoveredFogCollider].tile;
    }
}
