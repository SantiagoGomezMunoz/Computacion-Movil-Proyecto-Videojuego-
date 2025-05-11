using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public int daño = 1;
    public float tiempoDeVida = 3f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto tiene el tag "Enemigo"
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            EnemigoSeguidor enemigo = collision.gameObject.GetComponent<EnemigoSeguidor>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(daño); // Aplica daño configurable
            }

            Destroy(gameObject); // Destruye el proyectil tras impactar
        }
    }
}