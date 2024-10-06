using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatusScript : MonoBehaviour
{
    public TextMeshProUGUI tmpTime, tmpMoney, tmpResearch, tmpDebt;

    Game game;

    void Start() {
        game = GameManagerScript.instance.game;
    }

    void Update() {
        tmpTime.text = Util.IntToDisplayString(game.time);
        tmpMoney.text = Util.IntToDisplayString(game.money);
        tmpResearch.text = Util.IntToDisplayString(game.researchPoints);
        tmpDebt.text = game.gameOver ?
            "GAME OVER" :
            $"-<sprite name=\"money\" tint=1>{Util.IntToDisplayString(game.debt.current)} AT <sprite name=\"time\" tint=1>0";
    }
}
