using Assets.Code;
using Assets.Code.Model;
using Assets.Code.Model.GameEvents;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    public static GameManagerScript instance;
    public static GameEventManager events {
        get { return instance.game.gameEventManager; }
    }

    static float WAIT_TICK_SECONDS = .5f;
    static float IDLE_TICK_SECONDS = 3;

    public GameObject prefabCreatureParty, prefabEnemy;

    public Game game;
    Dictionary<Entity, EntityScript> entityScripts;
    Pauser pauser;
    float idleTimer;

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
        if (IsRunning()) {
            idleTimer += Time.deltaTime;
            if (idleTimer > (game.waitTicks > 0 ? WAIT_TICK_SECONDS : IDLE_TICK_SECONDS)) {
                Tick();
            }
        }
        // DEBUG
        if (Input.GetKeyDown(KeyCode.Space) && pauser.IsUnpaused()) {
            Tick();
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

    void Tick() {
        game.Tick();
        idleTimer = 0;
    }

    internal static PauseSource GetPauseSource() {
        return instance.pauser.GetSource();
    }
    internal static bool IsRunning() {
        return !instance.game.gameOver && instance.pauser.IsUnpaused();
    }
    internal static bool IsInteractable() {
        return IsRunning() && instance.game.waitTicks == 0;
    }
}
