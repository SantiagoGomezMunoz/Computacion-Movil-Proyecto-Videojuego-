using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    public ChunkManager manager;
    public Vector2Int gridPosition; // ejemplo: (0,0), (1,0)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.PlayerEnteredChunk(gridPosition);
        }
    }
}