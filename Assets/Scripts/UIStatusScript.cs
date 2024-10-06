using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatusScript : MonoBehaviour
{
    public TextMeshProUGUI tmpTime, tmpMoney, tmpResearch;

    Game game;

    void Start() {
        game = GameManagerScript.instance.game;
    }

    void Update() {
        tmpTime.text = Util.IntToDisplayString(game.time);
        tmpMoney.text = Util.IntToDisplayString(game.money);
        tmpResearch.text = Util.IntToDisplayString(game.researchPoints);
    }
}
