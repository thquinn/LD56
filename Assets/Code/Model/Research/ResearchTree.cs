using Assets.Code.Model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model.Research {
    public class ResearchTree {
        Dictionary<Vector2Int, Research> researchByCoor;
        Dictionary<string, Research> researchByID;

        public ResearchTree() {
            researchByCoor = new Dictionary<Vector2Int, Research>();
            researchByID = new Dictionary<string, Research>();

            Add(Vector2Int.zero, new ResearchStart());

            Add(new Vector2Int(-5, 0), new ResearchNewCreature(3, Creature.ANGEL, 50f), ResearchStart.ID);
            Add(new Vector2Int(-8, 0), new ResearchShopSlot(10), ResearchStart.ID, ResearchNewCreature.GetID(Creature.ANGEL));

            Add(new Vector2Int(1, 4), new ResearchFogvision(5), ResearchStart.ID);

            Add(new Vector2Int(4, -2), new ResearchPartyEnable(), ResearchStart.ID);
            Add(new Vector2Int(7, 3), new ResearchShopSlot(15), ResearchPartyEnable.ID);
            Add(new Vector2Int(6, -6), new ResearchPartyMixing(), ResearchPartyEnable.ID);
        }

        public IEnumerable<Research> AllResearch() {
            foreach (Research research in researchByID.Values) {
                if (research.id != ResearchStart.ID) yield return research;
            }
        }

        void Add(Vector2Int coor, Research research, params string[] requirementIDs) {
            researchByCoor[coor] = research;
            research.coor = coor;
            researchByID[research.id] = research;
            foreach (string requirementID in requirementIDs) {
                research.requirements.Add(researchByID[requirementID]);
            }
        }
    }
}
