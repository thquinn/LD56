using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code {
    public static class Util {
        public static Vector3 BoardCoorToWorldCoor(Vector2Int boardCoor) {
            float x = boardCoor.x * 3 / 4f;
            float yOffset = boardCoor.x % 2 == 0 ? 0 : .5f;
            float z = (boardCoor.y + yOffset) * -Mathf.Sqrt(3) / 2;
            return new Vector3(x, 0, z);
        }
        public static int HexagonalDistance(Vector2Int coorOne, Vector2Int coorTwo) {
            Vector3Int vec = BoardCoorToCube(coorOne) - BoardCoorToCube(coorTwo);
            return (Mathf.Abs(vec.x) + Mathf.Abs(vec.y) + Mathf.Abs(vec.z)) / 2;
        }
        static Vector3Int BoardCoorToCube(Vector2Int coor) {
            int q = coor.x;
            int r = coor.y - (coor.x + (coor.x & 1)) / 2;
            return new Vector3Int(q, r, -q - r);
        }
    }
}
