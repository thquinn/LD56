using Assets.Code;
using Assets.Code.Model;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIExpeditionPanelScript : MonoBehaviour {
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
        return instance?.expedition.selectedTiles.Contains(tile) == true;
    }
    public static bool IsSurroundedForExploration(Tile tile) {
        return instance?.expedition.surroundedTiles.Contains(tile) == true;
    }
    public static bool CanToggle(Tile tile) {
        return instance?.CanToggleImpl(tile) == true;
    }
    public bool CanToggleImpl(Tile tile) {
        if (!IsActive()) return false;
        if (!expedition.selectedTiles.Contains(tile)) {
            if (expedition.selectedTiles.Count == 0 && tile.distanceToRevealed == 1) return true;
            return expedition.selectedTiles.Any(t => Util.HexagonalDistance(t.coor, tile.coor) == 1);
        }
        // Tiles can't be toggled off if they're the only one next to the party.
        var adjacents = expedition.selectedTiles.Where(t => Util.HexagonalDistance(t.coor, expedition.party.tile.coor) == 1);
        if (adjacents.Count() == 1 && adjacents.First() == tile) return false;
        // Tiles can't be toggled off if they would cut the selection in half.
        return Util.CoorsWouldBeContiguousWithout(expedition.selectedTiles.Select(t => t.coor).ToHashSet(), tile.coor);
    }
    public static void Toggle(Tile tile) {
        instance?.ToggleImpl(tile);
    }
    void ToggleImpl(Tile tile) {
        if (!CanToggleImpl(tile)) return;
        if (expedition.selectedTiles.Contains(tile)) {
            expedition.selectedTiles.Remove(tile);
        } else {
            expedition.selectedTiles.Add(tile);
        }
        if (expedition.selectedTiles.Count == 0) {
            Cancel();
            return;
        }
        expedition.FindSurrounded();
        tmpTime.text = $"<sprite name=\"time\" tint=1>{expedition.GetTimeCost()}";
        tmpResearch.text = $"<sprite name=\"research\" tint=1>{expedition.GetResearchGain()}";
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
