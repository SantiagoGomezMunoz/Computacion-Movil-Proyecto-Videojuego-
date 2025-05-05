using UnityEngine;

public class DañoAlJugador : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaJugador vida = other.GetComponent<VidaJugador>();
            if (vida != null)
            {
                vida.TomarDaño(1);
                Debug.Log("¡El jugador recibió daño!");
            }
        }
    }
}