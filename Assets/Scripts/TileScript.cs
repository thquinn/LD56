using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject prefabSpawner;

    public MeshRenderer meshRenderer;
    public GameObject fog, fogIcon;

    public Tile tile;
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
        transform.localPosition = Util.BoardCoorToWorldCoor(tile.coor);
    }

    void Update() {
        fog.SetActive(!tile.revealed);
        fogIcon.SetActive(!tile.revealed && tile.feature != null && tile.featureVisibleInFog);
        if (tile.revealed && tile.feature != null && featureObject == null) {
            if (tile.feature is Spawner) {
                featureObject = Instantiate(prefabSpawner, transform);
            }
        }
    }
}
