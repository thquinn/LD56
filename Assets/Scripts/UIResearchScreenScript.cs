using Assets.Code.Model.Research;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResearchScreenScript : MonoBehaviour
{
    static float SCALE = 80;
    static float DAMP_TIME = .1f;

    public GameObject prefabResearchNode;

    public CanvasGroup canvasGroup;
    public RectTransform rtPan;
    public float panSensitivity;

    ResearchTree tree;
    public bool showing;
    Dictionary<Research, UIResearchNodeScript> nodes;
    float vAlpha;

    void Start() {
        tree = GameManagerScript.instance.game.researchTree;
        nodes = new Dictionary<Research, UIResearchNodeScript>();
        canvasGroup.alpha = 0;
    }

    void Update() {
        canvasGroup.alpha = Mathf.SmoothDamp(canvasGroup.alpha, showing ? 1 : 0, ref vAlpha, DAMP_TIME);
        if (canvasGroup.alpha < .01) {
            rtPan.anchoredPosition = Vector2.zero;
        }
        canvasGroup.blocksRaycasts = canvasGroup.alpha > .02f;
        foreach (Research research in tree.AllResearch()) {
            if (research.RequirementsMet() && !nodes.ContainsKey(research)) {
                UIResearchNodeScript node = Instantiate(prefabResearchNode, rtPan).GetComponent<UIResearchNodeScript>();
                node.Init(research);
                (node.transform as RectTransform).anchoredPosition = new Vector3(research.coor.x * SCALE, research.coor.y * SCALE);
                nodes[research] = node;
            }
        }
        UpdateInput();
    }
    void UpdateInput() {
        if (!showing) return;
        Vector2 inputVector = new Vector2(
            Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0,
            Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0
        );
        rtPan.anchoredPosition -= inputVector * panSensitivity;
    }

    public void Show() {
        showing = true;
    }
    public void Hide() {
        showing = false;
    }
}
