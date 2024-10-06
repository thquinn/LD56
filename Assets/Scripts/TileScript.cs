using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject prefabSpawner;

    public MeshRenderer meshRenderer;
    public GameObject fog, fogIcon, selectionExplore;
    public Collider colliderTile, colliderFog;

    public Tile tile;
    Game game;
    GameObject featureObject;

    void Start() {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        float h = Random.Range(150f, 165f) / 360f;
        float s = Random.Range(.5f, .75f);
        float v = Random.Range(.85f, 1f);
        meshRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", Color.HSVToRGB(h, s, v));
        meshRenderer.SetPropertyBlock(block);
    }
    public void Init(Tile tile) {
        this.tile = tile;
        game = tile.board.game;
        transform.localPosition = Util.BoardCoorToWorldCoor(tile.coor);
    }

    void Update() {
        fog.SetActive(!tile.revealed);
        fogIcon.SetActive(!tile.revealed && tile.feature != null && tile.distanceToRevealed <= game.researchStatus.fogVisionRadius);
        bool fogSelect = UIExpeditionPanelScript.IsSelectedForExploration(tile);
        fogSelect |= BoardScript.instance.hoveredFogTile == tile && UIExpeditionPanelScript.CanToggle(tile);
        fogSelect |= BoardScript.instance.hoveredFogTile == tile && tile.distanceToRevealed == 1 && InteractionScript.GetGrabbed()?.CanExplore(tile) == true;
        selectionExplore.SetActive(fogSelect);
        if (tile.revealed && tile.feature != null && featureObject == null) {
            if (tile.feature is Spawner) {
                featureObject = Instantiate(prefabSpawner, transform);
            }
        }
    }
}
