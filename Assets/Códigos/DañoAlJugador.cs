using UnityEngine;

public class DanoAlJugador : MonoBehaviour
{
    public int daño = 1;
    public float tiempoEntreAtaques = 2f;
    private float ultimoAtaque = -Mathf.Infinity;

    private void OnCollisionEnter(Collision collision)
    {
        // Se mira si el objeto con el que colisionamos tiene la etiqueta Player
        if (collision.gameObject.CompareTag("Player"))
        {
            IntentarHacerDaño(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Se mira si el objeto con el que se está colisionando es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            IntentarHacerDaño(collision.gameObject);
        }
    }

    void IntentarHacerDaño(GameObject jugador)
    {
        // Para verificar si hay buen tiempo entre los ataques
        if (Time.time - ultimoAtaque >= tiempoEntreAtaques)
        {
            VidaJugador vida = jugador.GetComponent<VidaJugador>();
            if (vida != null)
            {
                vida.TomarDaño(daño);
                ultimoAtaque = Time.time;
            }
        }
    }
}