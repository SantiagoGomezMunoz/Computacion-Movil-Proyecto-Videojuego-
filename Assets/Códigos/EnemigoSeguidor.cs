using UnityEngine;
using System.Collections;

public class EnemigoSeguidor : MonoBehaviour
{
    public Transform objetivo;         // Jugador a seguir
    public float velocidad = 3.0f;     // Velocidad de movimiento
    public float rangoVision = 10f;    // Distancia para empezar a seguir
    public float rangoPerdida = 15f;   // Distancia para dejar de seguir

    private bool persiguiendo = false; // Estado del enemigo
    private Rigidbody rb;

    public int vida = 7;               // Vida del enemigo

    private PlayerLevel sistemaXP;

    private Renderer[] renderers;      // Enemigo cambia de color cuando lo golpean
    private Color colorOriginal;

    // Patrullaje
    public float tiempoEntrePatrullas = 2f; // Tiempo entre cambios de dirección
    public float duracionPatrulla = 3f; // Tiempo que el enemigo patrulla en una dirección
    private float tiempoEspera = 0f; // Tiempo restante de espera
    private float tiempoMovimiento = 0f; // Tiempo restante de movimiento
    private Vector3 direccionPatrulla = Vector3.zero; // Dirección actual de patrullaje

    public AudioClip sonidoDaño;
    public AudioClip sonidoMuerte;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sistemaXP = FindFirstObjectByType<PlayerLevel>();
        ElegirNuevaDireccionPatrulla(); // Inicia el primer patrón de patrullaje

        renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            colorOriginal = renderers[0].material.color;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (objetivo != null)
        {
            float distancia = Vector3.Distance(transform.position, objetivo.position);

            if (!persiguiendo && distancia <= rangoVision)
            {
                persiguiendo = true;
            }
            else if (persiguiendo && distancia >= rangoPerdida)
            {
                persiguiendo = false;
                ElegirNuevaDireccionPatrulla(); // Reinicia patrullaj
            }
        }
    }

    void FixedUpdate()
    {
        if (persiguiendo && objetivo != null)
        {
            Vector3 direccion = (objetivo.position - transform.position).normalized;
            Vector3 nuevaPos = transform.position + direccion * velocidad * Time.fixedDeltaTime;
            rb.MovePosition(nuevaPos);
        }
        else
        {
            Patrullar();
        }
    }

    void Patrullar()
    {
        if (tiempoMovimiento > 0)
        {
            Vector3 nuevaPos = transform.position + direccionPatrulla * velocidad * Time.fixedDeltaTime;
            rb.MovePosition(nuevaPos);
            tiempoMovimiento -= Time.fixedDeltaTime;
        }
        else
        {
            tiempoEspera -= Time.fixedDeltaTime;
            if (tiempoEspera <= 0)
            {
                ElegirNuevaDireccionPatrulla();
            }
        }
    }

    void ElegirNuevaDireccionPatrulla()
    {
        // Elige una nueva dirección aleatoria para patrullar
        float angulo = Random.Range(0f, 360f);
        direccionPatrulla = new Vector3(Mathf.Cos(angulo), 0f, Mathf.Sin(angulo)).normalized;

        tiempoMovimiento = duracionPatrulla; // Establece el tiempo de movimiento
        tiempoEspera = tiempoEntrePatrullas; // Establece el tiempo de espera
    }

    IEnumerator CambiarColorTemporal()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            Color colorOriginal = rend.material.color;
            rend.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            rend.material.color = colorOriginal;
        }
    }

    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;

        //Retroceso
        Vector3 direccionEmpuje = (transform.position - objetivo.position).normalized;
        rb.AddForce(direccionEmpuje * 4f, ForceMode.Impulse);
        // Cambio de color
        StartCoroutine(CambiarColorTemporal());

        if (audioSource != null && sonidoDaño != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }

        if (vida <= 0)
        {
            Morir();
        }
    }

    IEnumerator FeedbackDaño()
    {
        // Cambiar color a rojo
        foreach (Renderer rend in renderers)
        {
            rend.material.color = Color.red;
        }

        // Retroceso leve hacia atrás
        if (objetivo != null && rb != null)
        {
            Vector3 direccionRetroceso = (transform.position - objetivo.position).normalized;
            rb.AddForce(direccionRetroceso * 2f, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(0.1f);

        // Volver al color original
        foreach (Renderer rend in renderers)
        {
            rend.material.color = colorOriginal;
        }
    }

    void Morir()
    {
        if (audioSource != null && sonidoMuerte != null)
        {
            audioSource.PlayOneShot(sonidoMuerte);
        }

        if (sistemaXP != null)
        {
            sistemaXP.GanarExperiencia(35); // Gana 2 de experiencia al morir
        }
        Destroy(gameObject);

        Destroy(gameObject, 0.2f); // Esperar a que suene el audio

        if (UpgradeManager.Instance != null) UpgradeManager.Instance.AddUpgradePoint(1);
    }
}