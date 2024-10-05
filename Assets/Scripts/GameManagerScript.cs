using Assets.Code;
using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public GameObject prefabCreatureParty;

    public Game game;
    Dictionary<Entity, EntityScript> entityScripts;

    void Start() {
        instance = this;
        game = new Game();
        entityScripts = new Dictionary<Entity, EntityScript>();
    }

    void Update() {
        foreach (Entity entity in game.board.GetTiles().Select(t => t.entity).Where(e => e != null && !entityScripts.ContainsKey(e))) {
            if (entity is CreatureParty) {
                entityScripts[entity] = Instantiate(prefabCreatureParty, transform).GetComponent<CreaturePartyScript>().Init(entity as CreatureParty);
            }
        }
    }
}
