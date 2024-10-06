using Assets.Code;
using Assets.Code.Model;
using Assets.Code.Model.Creatures;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class TileScript : MonoBehaviour
{
    static Quaternion PATH_ROTATION_60 = Quaternion.Euler(0, 0, 60);
    static Quaternion PATH_ROTATION_120 = Quaternion.Euler(0, 0, 120);

    public GameObject prefabSpawner;

    public MeshRenderer meshRenderer;
    public GameObject fog, fogIcon, selectionTile, selectionExplore;
    public Collider colliderTile, colliderFog;
    public SpriteRenderer pathRenderer;
    public Sprite spritePathStraight, spritePathEnd, spritePathTurn60, spritePathTurn120;

    public Tile tile;
    Game game;
    GameObject featureObject;
    List<Vector2Int> path;

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
        bool tileSelect = !InteractionScript.IsSpawningNewParty() && BoardScript.instance.hoveredTile == tile;
        tileSelect |= InteractionScript.IsSpawningNewParty() && tile.GetNeighbors().Any(t => t.entity?.HasAbility(CreatureAbilityHome.NAME) == true);
        tileSelect &= path == null;
        selectionTile.SetActive(tileSelect);
        bool fogSelect = UIExpeditionPanelScript.IsSelectedForExploration(tile);
        fogSelect |= BoardScript.instance.hoveredFogTile == tile && UIExpeditionPanelScript.CanToggle(tile);
        fogSelect |= BoardScript.instance.hoveredFogTile == tile && tile.distanceToRevealed == 1 && InteractionScript.GetGrabbed()?.CanExplore(tile) == true;
        selectionExplore.SetActive(fogSelect);
        if (tile.revealed && tile.feature != null && featureObject == null) {
            if (tile.feature is Spawner) {
                featureObject = Instantiate(prefabSpawner, transform);
            }
        }
        if (path != InteractionScript.instance.path) {
            path = InteractionScript.instance.path;
            UpdatePath();
        }
    }

    void UpdatePath() {
        int pathIndex = InteractionScript.PathIndex(tile);
        pathRenderer.gameObject.SetActive(pathIndex > 0);
        if (!pathRenderer.gameObject.activeSelf) {
            return;
        }
        Vector3Int a = Util.BoardCoorToCube(path[pathIndex - 1]);
        Vector3Int b = Util.BoardCoorToCube(path[pathIndex]);
        Vector3Int dAB = b - a;
        if (pathIndex == path.Count - 1) {
            pathRenderer.sprite = spritePathEnd;
            float degrees;
            if (dAB.x == 0) degrees = dAB.y < 0 ? 0 : 180;
            else if (dAB.y == 0) degrees = dAB.z > 0 ? 60 : 240;
            else degrees = dAB.x < 0 ? 120 : 300;
            pathRenderer.transform.localRotation = Quaternion.Euler(0, 0, degrees);
        } else {
            Vector3Int c = Util.BoardCoorToCube(path[pathIndex + 1]);
            Vector3Int dBC = c - b;
            Vector3Int dd = dBC - dAB;
            if (dd == Vector3Int.zero) {
                pathRenderer.sprite = spritePathStraight;
                if (a.x == b.x) pathRenderer.transform.localRotation = Quaternion.identity;
                else if (a.y == b.y) pathRenderer.transform.localRotation = PATH_ROTATION_60;
                else if (a.z == b.z) pathRenderer.transform.localRotation = PATH_ROTATION_120;
            } else if (dd.x + dd.y + dd.z == 0) {
                pathRenderer.sprite = spritePathTurn60;
                float degrees = 0;
                if (dd == new Vector3Int(0, 1, -1)) degrees = -60;
                else if (dd == new Vector3Int(1, 0, -1)) degrees = 0;
                else if (dd == new Vector3Int(1, -1, 0)) degrees = 60;
                else if (dd == new Vector3Int(0, -1, 1)) degrees = 120;
                else if (dd == new Vector3Int(-1, 0, 1)) degrees = 180;
                else if (dd == new Vector3Int(-1, 1, 0)) degrees = 240;
                pathRenderer.transform.localRotation = Quaternion.Euler(0, 0, degrees);
            } else {
                // these are the 120-degree turns, which we never use since the board has no "thin walls"
            }
        }
    }
}
