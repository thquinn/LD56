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
    public SpriteRenderer srSprite, srShadow;

    int animationFrame = -1;

    public void Init(Creature creature) {
        this.creature = creature;
    }

    void Update() {
        int nextAnimationFrame = Mathf.FloorToInt((Time.time * ANIMATION_RATE) % 5);
        if (nextAnimationFrame != animationFrame) {
            animationFrame = nextAnimationFrame;
            string frameName = $"{spriteName}_{animationFrame}";
            srSprite.sprite = atlasCreatures.GetSprite(frameName);
            srShadow.sprite = atlasShadows.GetSprite(frameName);
        }
    }
}
