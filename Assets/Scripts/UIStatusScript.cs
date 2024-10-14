using Assets.Code;
using Assets.Code.Model;
using TMPro;
using UnityEngine;

public class UIStatusScript : MonoBehaviour {
    static float TIME_PARTICLES_DAMP_TIME = .2f;

    public GameObject prefabTimeSpinner;

    public TextMeshProUGUI tmpTime, tmpMoney, tmpResearch, tmpDebt;
    public CanvasGroup timeParticlesCanvasGroup;

    Game game;
    float vTimeParticlesAlpha;

    void Start() {
        game = GameManagerScript.instance.game;
        for (int i = 0; i < 8; i++) {
            Instantiate(prefabTimeSpinner, timeParticlesCanvasGroup.transform);
        }
    }

    void Update() {
        tmpTime.text = Util.IntToDisplayString(game.time);
        tmpMoney.text = Util.IntToDisplayString(game.money);
        tmpResearch.text = Util.IntToDisplayString(game.researchPoints);
        tmpDebt.text = game.gameOver ?
            "GAME OVER" :
            $"-<sprite name=\"money\" tint=1>{Util.IntToDisplayString(game.debt.current)} AT <sprite name=\"time\" tint=1>0";
        timeParticlesCanvasGroup.alpha = Mathf.SmoothDamp(timeParticlesCanvasGroup.alpha, game.waitTicks > 0 ? 1 : 0, ref vTimeParticlesAlpha, TIME_PARTICLES_DAMP_TIME);
    }
}
