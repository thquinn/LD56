using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyScript : EntityScript<Enemy>
{
    public TextMeshPro tmp;
    public ParticleSystem particles;
    public SpriteRenderer srShadow;

    Enemy enemy;
    bool destroying;
    float vParticlesSpeed, vShadowAlpha;

    public override EntityScript<Enemy> Init(Enemy enemy) {
        this.enemy = enemy;
        return this;
    }

    void Update() {
        if (destroying) {
            var particlesMain = particles.main;
            particlesMain.simulationSpeed = Mathf.SmoothDamp(particlesMain.simulationSpeed, 5, ref vParticlesSpeed, 2);
            srShadow.SetAlpha(Mathf.SmoothDamp(srShadow.color.a, 0, ref vShadowAlpha, 2));
            return;
        }
        if (enemy.isDead) {
            particles.Stop();
            tmp.gameObject.SetActive(false);
            Invoke("DelayedDestroy", 5);
            destroying = true;
            return;
        }
        transform.localPosition = Util.BoardCoorToWorldCoor(enemy.tile.coor);
        tmp.text = Util.IntToDisplayString(enemy.health);
    }

    void DelayedDestroy() {
        Destroy(gameObject);
    }
}
