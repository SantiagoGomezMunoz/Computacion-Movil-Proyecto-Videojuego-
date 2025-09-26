using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public Transform player;       // Referencia al jugador
    public float activationRange = 30f; // Distancia para activar este chunk
    private bool isActive = false;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        // Si el jugador está dentro del rango y el chunk está desactivado → activarlo
        if (distance < activationRange && !isActive)
        {
            gameObject.SetActive(true);
            isActive = true;
        }
        // Si el jugador se aleja demasiado → desactivarlo
        else if (distance >= activationRange && isActive)
        {
            gameObject.SetActive(false);
            isActive = false;
        }
    }
}