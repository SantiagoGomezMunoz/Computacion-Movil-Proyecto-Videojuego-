using UnityEngine;

[DisallowMultipleComponent]
public class ChunkLoader : MonoBehaviour
{
    public Transform player;           // Asigna el Player en el inspector
    public float checkInterval = 0.12f; // cada cuánto revisa

    private Vector2Int lastChunk = new Vector2Int(int.MinValue, int.MinValue);

    void Start()
    {
        if (player == null) Debug.LogError("[ChunkLoader] Asigna el player en el inspector.");
        InvokeRepeating(nameof(Check), 0f, checkInterval);
    }

    void Check()
    {
        if (ChunkManager.Instance == null) return;

        Vector2Int detected = ChunkManager.Instance.GetChunkGridForPosition(player.position);

        if (detected.x == int.MinValue)
        {
            if (ChunkManager.Instance != null)
            {
                if (lastChunk.x != int.MinValue) return;
                detected = ChunkManager.Instance.ActivateNearestChunkToPosition(player.position);
            }
        }

        if (detected != lastChunk)
        {
            lastChunk = detected;
            if (ChunkManager.Instance != null)
                ChunkManager.Instance.PlayerEnteredChunk(detected); // 🔹 aquí ahora activará vecinos

            Debug.Log($"[ChunkLoader] Jugador cambiado a chunk {detected} (pos {player.position.x:F1},{player.position.z:F1})");
        }
    }
}