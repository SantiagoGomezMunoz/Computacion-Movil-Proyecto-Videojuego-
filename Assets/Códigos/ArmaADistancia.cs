using UnityEngine;

public class Arma : MonoBehaviour, IArma
{
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public int municionActual = 30;
    public int municionMaxima = 30;
    public float velocidadProyectil = 20f;

    public AudioClip sonidoDisparo; 
    public AudioSource audioSource;
    private MovimientoPersonaje movimientoJugador;
    
    void Start()
    {
        // Buscar automáticamente el movimiento del jugador en la escena
        movimientoJugador = FindFirstObjectByType<MovimientoPersonaje>();

        audioSource = GetComponent<AudioSource>();
        
        // CAMBIO: usar el AudioSource del jugador
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            audioSource = jugador.GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource no encontrado en el arma equipada.");
        }

        if (movimientoJugador == null)
        {
            Debug.LogWarning("No se encontró el componente MovimientoPersonaje en la escena.");
        }
    }
    
    public void Usar()
    {
        if (municionActual <= 0 || proyectilPrefab == null || puntoDisparo == null) return;

        Vector3 direccionDisparo = Vector3.forward;

        // Obtener la dirección desde el script de movimiento
        if (movimientoJugador != null)
        {
            direccionDisparo = movimientoJugador.ultimaDireccionMovimiento;
        }

        if (direccionDisparo == Vector3.zero)
        {
            direccionDisparo = puntoDisparo.forward; 
        }

        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.LookRotation(direccionDisparo));
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = direccionDisparo * velocidadProyectil;
        }

        municionActual--;

        if (audioSource != null && sonidoDisparo != null)
        {
            Debug.Log("AudioSource asignado: " + (audioSource != null));
            Debug.Log("Clip asignado: " + (sonidoDisparo != null ? sonidoDisparo.name : "null"));
            audioSource.PlayOneShot(sonidoDisparo);
        }
        
    }
}