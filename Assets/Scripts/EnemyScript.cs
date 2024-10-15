using Assets.Code;
using Assets.Code.Model;
using TMPro;
using UnityEngine;

public class EnemyScript : EntityScript<Enemy> {
    public GameObject goStatus, goArmor;
    public TextMeshPro tmpHealth, tmpArmor;
    public ParticleSystem particles;
    public SpriteRenderer srShadow;

    Enemy enemy;
    float tmpHealthInitialSize, tmpArmorInitialSize;
    bool destroying;
    float vParticlesSpeed, vShadowAlpha;

    public override EntityScript<Enemy> Init(Enemy enemy) {
        this.enemy = enemy;
        tmpHealthInitialSize = tmpHealth.fontSize;
        tmpArmorInitialSize = tmpArmor.fontSize;
        Update();
        return this;
    }

    void Update() {
        if (destroying) {
            var particlesMain = particles.main;
            particlesMain.simulationSpeed = Mathf.SmoothDamp(particlesMain.simulationSpeed, 5, ref vParticlesSpeed, 1);
            srShadow.SetAlpha(Mathf.SmoothDamp(srShadow.color.a, 0, ref vShadowAlpha, 2));
            return;
        }
        if (enemy.isDead) {
            particles.Stop();
            goStatus.SetActive(false);
            Invoke("DelayedDestroy", 5);
            destroying = true;
            return;
        }
        transform.localPosition = Util.BoardCoorToWorldCoor(enemy.tile.coor);
        tmpHealth.text = Util.IntToDisplayString(enemy.health);
        tmpHealth.fontSize = tmpHealthInitialSize * Util.IntToDisplayStringScale(enemy.health);
        int armor = enemy.GetArmor();
        goArmor.SetActive(armor > 0);
        tmpArmor.text = armor.ToString();
        tmpArmor.fontSize = tmpArmorInitialSize * Util.IntToDisplayStringScale(armor);
    }

    void DelayedDestroy() {
        Destroy(gameObject);
    }
}
