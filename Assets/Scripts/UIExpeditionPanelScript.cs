using Assets.Code;
using Assets.Code.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UIExpeditionPanelScript : MonoBehaviour
{
    public static UIExpeditionPanelScript instance;

    public TextMeshProUGUI tmpStrength, tmpTime, tmpResearch, tmpLoss;

    public Expedition expedition;

    void Start() {
        instance = this;
        Cancel();
    }

    void Update() {
        
    }

    public static void StartExpedition(Party party, Tile tile) {
        instance?.StartExpeditionImpl(party, tile);
    }
    void StartExpeditionImpl(Party party, Tile tile) {
        gameObject.SetActive(true);
        expedition.party = party;
        tmpStrength.text = party.GetAttack().ToString();
        ToggleImpl(tile);
    }

    public static bool IsActive() {
        return instance?.gameObject.activeSelf == true;
    }
    public static bool IsSelectedForExploration(Tile tile) {
        return instance?.expedition.tiles.Contains(tile) == true;
    }
    public static bool CanToggle(Tile tile) {
        return instance?.CanToggleImpl(tile) == true;
    }
    public bool CanToggleImpl(Tile tile) {
        if (!IsActive()) return false;
        if (!expedition.tiles.Contains(tile)) {
            if (expedition.tiles.Count == 0 && tile.distanceToRevealed == 1) return true;
            return expedition.tiles.Any(t => Util.HexagonalDistance(t.coor, tile.coor) == 1);
        }
        // Tiles can't be toggled off if they're the only distance 1 tile and there are 2+ tiles.
        if (tile.distanceToRevealed == 1 && expedition.tiles.Count(t => t.distanceToRevealed == 1) == 1 && expedition.tiles.Count >= 2) return false;
        // Tiles can't be toggled off if they would cut the selection in half.
        return Util.CoorsWouldBeContiguousWithout(expedition.tiles.Select(t => t.coor).ToHashSet(), tile.coor);
    }
    public static void Toggle(Tile tile) {
        instance?.ToggleImpl(tile);
    }
    void ToggleImpl(Tile tile) {
        if (!CanToggleImpl(tile)) return;
        if (expedition.tiles.Contains(tile)) {
            expedition.tiles.Remove(tile);
        } else {
            expedition.tiles.Add(tile);
        }
        if (expedition.tiles.Count == 0) {
            Cancel();
            return;
        }
        tmpTime.text = $"{expedition.GetTimeCost()}<sprite name=\"time\" tint=1>";
        tmpResearch.text = $"{expedition.GetResearchGain()}<sprite name=\"research\" tint=1>";
        tmpLoss.text = $"{expedition.GetLossPercent()}%";
    }

    public void Confirm() {
        GameManagerScript.instance.game.StartExpedition(expedition);
        Cancel();
    }
    public void Cancel() {
        expedition = new Expedition();
        gameObject.SetActive(false);
    }
}
