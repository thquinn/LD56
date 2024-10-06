using Assets.Code;
using Assets.Code.Model;
using Assets.Code.Model.Creatures;
using Assets.Code.Model.GameEvents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public static GameEventManager events {
        get { return instance.game.gameEventManager; }
    }

    public GameObject prefabCreatureParty, prefabEnemy;

    public Game game;
    Dictionary<Entity, EntityScript> entityScripts;
    Pauser pauser;

    void Start() {
        instance = this;
        game = new Game();
        game.Init();
        entityScripts = new Dictionary<Entity, EntityScript>();
        pauser = new Pauser();
    }

    void Update() {
        var deadEntities = entityScripts.Keys.Where(e => e.isDead).ToList();
        foreach (Entity deadEntity in deadEntities) {
            entityScripts.Remove(deadEntity);
        }
        foreach (Entity entity in game.board.GetTiles().Select(t => t.entity).Where(e => e != null && !entityScripts.ContainsKey(e))) {
            if (entity is Party) {
                entityScripts[entity] = Instantiate(prefabCreatureParty, transform).GetComponent<CreaturePartyScript>().Init(entity as Party);
            } else if (entity is Enemy) {
                entityScripts[entity] = Instantiate(prefabEnemy, transform).GetComponent<EnemyScript>().Init(entity as Enemy);
            }
        }
        if (game.waitTicks > 0 && pauser.IsUnpaused()) {
            game.Tick();
        }
        // DEBUG
        if (Input.GetKeyDown(KeyCode.Space) && pauser.IsUnpaused()) {
            game.Tick();
        }
        if (Input.GetKeyDown(KeyCode.F1)) {
            game.time += 100;
            game.money += 1000;
            game.researchPoints += 100;
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            game.shop.AddSlot();
        }
        if (Input.GetKeyDown(KeyCode.F3)) {
            game.researchStatus.partySize = 100;
        }
    }

    internal static PauseSource GetPauseSource() {
        return instance.pauser.GetSource();
    }
    internal static bool IsInteractable() {
        return !instance.game.gameOver && instance.game.waitTicks == 0 && instance.pauser.IsUnpaused();
    }
}
