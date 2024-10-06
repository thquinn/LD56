using Assets.Code;
using Assets.Code.Model;
using Assets.Code.Model.Research;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResearchNodeScript : MonoBehaviour
{
    static float DAMP_TIME = .2f;

    public CanvasGroup canvasGroup;
    public TextMeshProUGUI tmpCost;
    public Image imageButton, imageGlow, imageShadow;
    public Color glowColor;

    Game game;
    Research research;
    float vAlpha;
    Color initialButtonColor;
    float tButtonColor, vButtonColor;
    float tTextColor, vTextColor;
    float initialGlowAlpha, vGlowAlpha;

    void Start() {
        canvasGroup.alpha = 0;
        initialButtonColor = imageButton.color;
        initialGlowAlpha = imageGlow.color.a;
        imageGlow.SetAlpha(0);
    }
    public void Init(Research research) {
        game = GameManagerScript.instance.game;
        this.research = research;
        tmpCost.text = $"{research.cost}<sprite name=\"research\" tint=1>";
    }

    void Update() {
        if (canvasGroup.enabled) {
            canvasGroup.alpha = Mathf.SmoothDamp(canvasGroup.alpha, 1, ref vAlpha, .2f);
            if (canvasGroup.alpha > .99f) canvasGroup.enabled = false;
        }
        tTextColor = Mathf.SmoothDamp(tTextColor, CanAfford() ? 1 : 0, ref vTextColor, DAMP_TIME);
        if (tmpCost != null) {
            tmpCost.color = Color.Lerp(Color.black, glowColor, tTextColor);
        }
        
        if (research.unlocked) {
            tButtonColor = Mathf.SmoothDamp(tButtonColor, 1, ref vButtonColor, DAMP_TIME);
            imageButton.color = Color.Lerp(initialButtonColor, glowColor, tButtonColor);
            imageGlow.SetAlpha(Mathf.SmoothDamp(imageGlow.color.a, initialGlowAlpha, ref vGlowAlpha, DAMP_TIME));
        }
    }

    public void Click() {
        if (!CanAfford()) return;
        game.researchPoints -= research.cost;
        research.Unlock(game);
        Destroy(tmpCost.gameObject);
    }
    bool CanAfford() {
        return game.researchPoints >= research.cost;
    }
}
