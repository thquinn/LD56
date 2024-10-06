using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public GameObject prefabCreatureParty, prefabEnemy;

    public Game game;
    Dictionary<Entity, EntityScript> entityScripts;
    Pauser pauser;

    void Start() {
        instance = this;
        game = new Game();
        entityScripts = new Dictionary<Entity, EntityScript>();
        pauser = new Pauser();
    }

    void Update() {
        foreach (Entity entity in game.board.GetTiles().Select(t => t.entity).Where(e => e != null && !entityScripts.ContainsKey(e))) {
            if (entity is CreatureParty) {
                entityScripts[entity] = Instantiate(prefabCreatureParty, transform).GetComponent<CreaturePartyScript>().Init(entity as CreatureParty);
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
    }

    internal static PauseSource GetPauseSource() {
        return instance.pauser.GetSource();
    }
    internal static bool IsInteractable() {
        return instance.game.waitTicks == 0 && instance.pauser.IsUnpaused();
    }
}
