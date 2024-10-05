using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CreatureScript : MonoBehaviour {
    static float ANIMATION_RATE = 2;

    public SpriteAtlas atlasCreatures, atlasShadows;

    public Creature creature;
    public string spriteName;
    public SpriteRenderer srSprite, srBlur, srShadow;

    float alphaInitialShadow;
    int animationFrame = -1;

    void Start() {
        alphaInitialShadow = srShadow.color.a;
    }

    public void Init(Creature creature) {
        this.creature = creature;
    }

    void Update() {
        // Sprite animation.
        int nextAnimationFrame = Mathf.FloorToInt((Time.time * ANIMATION_RATE) % 5);
        if (nextAnimationFrame != animationFrame) {
            animationFrame = nextAnimationFrame;
            string frameName = $"{spriteName}_{animationFrame}";
            srSprite.sprite = atlasCreatures.GetSprite(frameName);
            srBlur.sprite = atlasShadows.GetSprite(frameName);
        }
        // Move animation.
        srSprite.SetAlpha(1 - transform.position.y * 2);
        srBlur.SetAlpha(1 - transform.position.y * 5);
        srShadow.SetAlpha(alphaInitialShadow * (1 - transform.position.y * 2));
        srShadow.transform.SetGlobalY(0.001f);
    }
}
