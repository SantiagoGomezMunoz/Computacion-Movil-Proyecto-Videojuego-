using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ChunkManager : MonoBehaviour
{
    [System.Serializable]
    public class ChunkData
    {
        public string name;
        public GameObject chunkObject;      // root del chunk
        public Vector2Int gridPosition;     // (0,0), (1,0), etc.
        public Vector2 manualSize = Vector2.zero; 
        [HideInInspector] public Rect boundsXZ;
        [HideInInspector] public Vector3 centerWorld;
    }

    public static ChunkManager Instance;

    [Header("Player")]
    public Transform jugador; // asigna el Player aquí

    [Header("Chunks (lista en el Inspector)")]
    public List<ChunkData> chunks = new List<ChunkData>();

    [Header("Opciones")]
    [Tooltip("Si true, cuando no se detecta chunk exacto, se activa el chunk más cercano")]
    public bool fallbackActivateNearest = true;
    [Tooltip("Radio de precarga (0 = solo el actual, 1 = vecinos inmediatos, 2 = vecinos a 2 de distancia, etc.)")]
    public int preloadRadius = 1;

    private Dictionary<Vector2Int, ChunkData> chunkMap = new Dictionary<Vector2Int, ChunkData>();
    private Vector2Int currentChunk = new Vector2Int(int.MinValue, int.MinValue);

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        if (jugador == null)
        {
            return;
        }

        BuildMapAndBounds();

        // Desactivar todos al inicio
        foreach (var cd in chunks)
            if (cd.chunkObject != null) cd.chunkObject.SetActive(false);

        Vector2Int initial = GetChunkGridForPosition(jugador.position);
        if (initial.x == int.MinValue)
        {
            if (fallbackActivateNearest) ActivateNearestChunkToPosition(jugador.position);
        }
        else
        {
            PlayerEnteredChunk(initial);
        }
    }

    void BuildMapAndBounds()
    {
        chunkMap.Clear();

        foreach (var cd in chunks)
        {
            if (cd == null || cd.chunkObject == null) continue;

            Bounds? union = null;
            var rends = cd.chunkObject.GetComponentsInChildren<Renderer>(true);
            if (rends != null && rends.Length > 0)
            {
                Bounds b = rends[0].bounds;
                for (int i = 1; i < rends.Length; i++) b.Encapsulate(rends[i].bounds);
                union = b;
            }
            else
            {
                var cols = cd.chunkObject.GetComponentsInChildren<Collider>(true);
                if (cols != null && cols.Length > 0)
                {
                    Bounds b = cols[0].bounds;
                    for (int i = 1; i < cols.Length; i++) b.Encapsulate(cols[i].bounds);
                    union = b;
                }
            }

            Vector3 center;
            float sizeX, sizeZ;
            if (union.HasValue)
            {
                center = union.Value.center;
                sizeX = union.Value.size.x;
                sizeZ = union.Value.size.z;
            }
            else if (cd.manualSize != Vector2.zero)
            {
                center = cd.chunkObject.transform.position;
                sizeX = cd.manualSize.x;
                sizeZ = cd.manualSize.y;
            }
            else
            {
                center = cd.chunkObject.transform.position;
                sizeX = 10f;
                sizeZ = 10f;
            }

            float minX = center.x - sizeX * 0.5f;
            float minZ = center.z - sizeZ * 0.5f;

            cd.boundsXZ = new Rect(minX, minZ, sizeX, sizeZ);
            cd.centerWorld = center;

            chunkMap[cd.gridPosition] = cd;
        }
    }

    public Vector2Int GetChunkGridForPosition(Vector3 worldPos)
    {
        foreach (var cd in chunks)
        {
            if (cd == null) continue;
            if (cd.boundsXZ.Contains(new Vector2(worldPos.x, worldPos.z)))
                return cd.gridPosition;
        }
        return new Vector2Int(int.MinValue, int.MinValue);
    }

    public Vector2Int ActivateNearestChunkToPosition(Vector3 pos)
    {
        if (chunks.Count == 0) return new Vector2Int(int.MinValue, int.MinValue);
        float best = float.MaxValue;
        ChunkData bestC = null;
        foreach (var cd in chunks)
        {
            if (cd == null) continue;
            float d = (cd.centerWorld - pos).sqrMagnitude;
            if (d < best) { best = d; bestC = cd; }
        }
        if (bestC != null)
        {
            PlayerEnteredChunk(bestC.gridPosition);
            return bestC.gridPosition;
        }
        return new Vector2Int(int.MinValue, int.MinValue);
    }

    public void PlayerEnteredChunk(Vector2Int newChunk)
    {
        if (newChunk.x == int.MinValue) return;

        // 🔹 Apagar todos
        foreach (var cd in chunks)
            if (cd != null && cd.chunkObject != null) cd.chunkObject.SetActive(false);

        // 🔹 Activar actual + vecinos dentro del preloadRadius
        foreach (var kvp in chunkMap)
        {
            Vector2Int pos = kvp.Key;
            int dx = Mathf.Abs(pos.x - newChunk.x);
            int dz = Mathf.Abs(pos.y - newChunk.y);

            if (dx <= preloadRadius && dz <= preloadRadius)
                ActivateChunk(pos);
        }

        currentChunk = newChunk;
    }

    void ActivateChunk(Vector2Int pos)
    {
        if (chunkMap.ContainsKey(pos))
        {
            var cd = chunkMap[pos];
            if (cd != null && cd.chunkObject != null)
            {
                cd.chunkObject.SetActive(true);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (chunks == null) return;
        Gizmos.color = Color.yellow;
        foreach (var cd in chunks)
        {
            if (cd == null) continue;
            Rect r = cd.boundsXZ;
            Vector3 center = new Vector3(r.x + r.width/2f, 0.1f, r.y + r.height/2f);
            Gizmos.DrawWireCube(center, new Vector3(r.width, 1f, r.height));
        }

        if (jugador != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(jugador.position, 0.25f);
        }
    }
}