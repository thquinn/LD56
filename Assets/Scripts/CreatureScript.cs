using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CreatureScript : MonoBehaviour {
    public Creature creature;
    public SpriteRenderer srSprite, srBlur, srShadow;

    float alphaInitialShadow;

    void Start() {
        alphaInitialShadow = srShadow.color.a;
    }

    public void Init(Creature creature) {
        this.creature = creature;
    }

    void Update() {
        // Move animation.
        float moveAnimationY = InteractionScript.IsGrabbed(creature) ? 0 : transform.position.y;
        srSprite.SetAlpha(1 - moveAnimationY * 2);
        srBlur.SetAlpha(1 - moveAnimationY * 5);
        srShadow.SetAlpha(alphaInitialShadow * (1 - transform.position.y * 2));
        srShadow.transform.SetGlobalY(0.001f);
    }
}
