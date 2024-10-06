using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopScript : MonoBehaviour
{
    static float SPACING_X = -75f;
    static float SPACING_Y = 110f;

    public GameObject prefabShopUnit;

    public RectTransform rtUnits;

    Shop shop;
    int lastNumSlots;

    void Start() {
        shop = GameManagerScript.instance.game.shop;
    }

    void Update() {
        while (lastNumSlots < shop.creatures.Count) {
            UIUnitScript shopUnitScript = Instantiate(prefabShopUnit, rtUnits).GetComponent<UIUnitScript>();
            shopUnitScript.Init(lastNumSlots);
            shopUnitScript.transform.localPosition = GetPositionForIndex(lastNumSlots);
            lastNumSlots++;
        }
    }

    Vector2 GetPositionForIndex(int index) {
        if (index == 0) return Vector2.zero;
        if (index == 1) return new Vector2(-SPACING_X, SPACING_Y);
        return new Vector2(SPACING_X * (index - 1), (index % 2 == 0) ? SPACING_Y : 0);
    }

    public void RefreshShop() {
        shop.Refresh();
    }
}
