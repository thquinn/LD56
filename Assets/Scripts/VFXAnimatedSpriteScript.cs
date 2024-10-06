using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class VFXAnimatedSpriteScript : MonoBehaviour
{
    static float ANIMATION_RATE = 2;

    public SpriteAtlas atlasCreatures, atlasShadows;

    public SpriteRenderer srMain, srBlur;
    public Image imageMain, imageBlur;
    public string spriteName;

    int animationFrame = -1;

    void Start() {
        Debug.Assert((srMain != null && srBlur != null) || (imageMain != null && imageBlur != null));
    }

    void Update() {
        // Sprite animation.
        int nextAnimationFrame = Mathf.FloorToInt((Time.time * ANIMATION_RATE) % 5);
        if (nextAnimationFrame != animationFrame) {
            animationFrame = nextAnimationFrame;
            string frameName = $"{spriteName}_{animationFrame}";
            Sprite spriteMain = atlasCreatures.GetSprite(frameName);
            Sprite spriteBlur = atlasShadows.GetSprite(frameName);
            if (srMain != null) {
                srMain.sprite = spriteMain;
                srBlur.sprite = spriteBlur;
            } else {
                imageMain.sprite = spriteMain;
                imageBlur.sprite = spriteBlur;
            }
        }
    }
}
