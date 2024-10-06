using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph;
using UnityEngine;

namespace Assets.Code {
    public static class Util {
        public static int RandRound(float f) {
            float fract = f % 1f;
            return Random.value > fract ? Mathf.FloorToInt(f) : Mathf.CeilToInt(f);
        }

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
        public static IEnumerable<Vector2Int> GetHexCoorsWithinRange(Vector2Int coor, int range) {
            Vector3Int cube = BoardCoorToCube(coor);
            for (int q = -range; q <= range; q++) {
                for (int r = Mathf.Max(-range, -q - range); r <= Mathf.Min(range, -q + range); r++) {
                    int s = -q - r;
                    yield return CubeCoorToBoard(cube + new Vector3Int(q, r, s));
                }
            }
        }
        public static bool CoorsWouldBeContiguousWithout(HashSet<Vector2Int> coors, Vector2Int except) {
            Debug.Assert(coors.Contains(except), "Tile contiguity check without exception in set.");
            if (coors.Count == 1) return true;
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(coors.First(c => c != except));
            HashSet<Vector2Int> seen = new HashSet<Vector2Int>() { queue.Peek() };
            while (queue.Count > 0) {
                Vector2Int current = queue.Dequeue();
                foreach (Vector2Int neighbor in GetNeighboringHexCoors(current)) {
                    if (!coors.Contains(neighbor)) continue;
                    if (seen.Contains(neighbor) || neighbor == except) continue;
                    queue.Enqueue(neighbor);
                    seen.Add(neighbor);
                }
            }
            return seen.Count == coors.Count - 1;
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
        public static Vector3 GetMouseHoverCoordinate(float offset) {
            if (mainCamera == null) mainCamera = Camera.main;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane ground = new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (!ground.Raycast(ray, out distance)) {
                throw new System.Exception("Camera raycast couldn't find the ground plane.");
            }
            return ray.GetPoint(distance + offset);
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

    public static class ListExtensions {
        public static T Pick<T>(this List<T> list) {
            if (list.Count == 0) return default(T);
            return list[Random.Range(0, list.Count)];
        }
        public static List<T> Shuffle<T>(this List<T> list) {
            int n = list.Count;
            for (int i = 0; i < n; i++) {
                int r = i + Random.Range(0, n - i);
                T t = list[r];
                list[r] = list[i];
                list[i] = t;
            }
            return list;
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
