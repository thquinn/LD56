using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public GameObject prefabTile;

    Board board;
    int numTiles;

    void Start() {
        board = GameManagerScript.instance.game.board;
    }

    void Update() {
        foreach (Tile tile in board.GetTilesFromIndex(numTiles)) {
            Instantiate(prefabTile, transform).GetComponent<TileScript>().Init(tile);
        }
        numTiles = board.NumTiles();
    }
}
