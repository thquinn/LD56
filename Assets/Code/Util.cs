using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

namespace Assets.Code {
    public static class Util {
        public static string IntToDisplayString(int i) {
            if (i < 0) return "-" + IntToDisplayString(-i);
            if (i < 1000) return i.ToString();
            if (i < 10000) return $"{i / 1000}.{(i % 1000)/100}K";
            if (i < 1e6) return $"{i / 1000}K";
            if (i < 1e7) return $"{i / 1e6}.{(i % 1e6) / 1e5}M";
            return $"{i / 1e6}M";
        }
        public static float IntToDisplayStringScale(int i) {
            if (i < 100) return 1;
            return .666f;
        }

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
        static Vector3Int[] CUBE_NEIGHBOR_OFFSETS = new Vector3Int[] { new Vector3Int(1, 0, -1), new Vector3Int(1, -1, 0), new Vector3Int(0, -1, 1), new Vector3Int(-1, 0, 1), new Vector3Int(-1, 1, 0), new Vector3Int(0, 1, -1) };
        public static IEnumerable<Vector2Int> GetNeighboringHexCoors(Vector2Int coor) {
            Vector3Int cube = BoardCoorToCube(coor);
            foreach (Vector3Int offset in CUBE_NEIGHBOR_OFFSETS) {
                yield return CubeCoorToBoard(cube + offset);
            }
        }
        static Vector3Int BoardCoorToCube(Vector2Int coor) {
            int q = coor.x;
            int r = coor.y - (coor.x - (coor.x & 1)) / 2;
            return new Vector3Int(q, r, -q - r);
        }
        static Vector2Int CubeCoorToBoard(Vector3Int coor) {
            int x = coor.x;
            int y = coor.y + (coor.x - (coor.x & 1)) / 2;
            return new Vector2Int(x, y);
        }

        static Camera mainCamera;
        public static Collider GetMouseCollider(LayerMask layerMask) {
            if (mainCamera == null) mainCamera = Camera.main;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                return null;
            }
            return hit.collider;
        }
    }

    public static class ArrayExtensions {
        public static T Pick<T>(this T[] array) {
            if (array.Length == 0) return default(T);
            return array[Random.Range(0, array.Length)];
        }
        public static T[] Shuffle<T>(this T[] array) {
            int n = array.Length;
            for (int i = 0; i < n; i++) {
                int r = i + Random.Range(0, n - i);
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
            return array;
        }
    }

    public static class TransformExtensions {
        public static void SetLocalY(this Transform transform, float y) {
            Vector3 localPosition = transform.localPosition;
            localPosition.y = y;
            transform.localPosition = localPosition;
        }
        public static void SetGlobalY(this Transform transform, float y) {
            Vector3 position = transform.position;
            position.y = y;
            transform.position = position;
        }
    }

    public static class SpriteRendererExtensions {
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha) {
            Color c = spriteRenderer.color;
            c.a = Mathf.Clamp01(alpha);
            spriteRenderer.color = c;
        }
    }
}
