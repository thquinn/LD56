using Assets.Code;
using Assets.Code.Model;
using Assets.Code.Model.Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public abstract class MonoBehaviourWithCreature : MonoBehaviour {
    public Creature creature;
}

public class CreatureScript : MonoBehaviourWithCreature {
    public Transform spritesTransform;
    public SpriteRenderer srSprite, srBlur, srShadow;

    float alphaInitialShadow;

    void Start() {
        alphaInitialShadow = srShadow.color.a;
    }

    public void Init(Creature creature) {
        this.creature = creature;
    }

    void Update() {
        // Center solo units visually.
        spritesTransform.localPosition = new Vector3(0, 0, creature.party.creatures.Count == 1 ? -.1f : 0);
        // Move animation.
        float moveAnimationY = InteractionScript.IsGrabbed(creature) ? 0 : transform.position.y;
        srSprite.SetAlpha(1 - moveAnimationY * 2);
        srBlur.SetAlpha(1 - moveAnimationY * 5);
        srShadow.SetAlpha(alphaInitialShadow * (1 - transform.position.y * 2));
        srShadow.transform.SetGlobalY(0.001f);
    }
}
