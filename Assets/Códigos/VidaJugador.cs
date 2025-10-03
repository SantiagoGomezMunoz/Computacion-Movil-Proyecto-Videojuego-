using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 5;
    public int vidaActual = 5;

    public GameObject panelMuerte;
    public MovimientoPersonaje movimiento;
    public Transform puntoReaparicion;
    public AudioClip sonidoDaño;
    public AudioClip sonidoCuracion;
    public AudioClip sonidoMuerte;
    private AudioSource audioSource;

    [Header("HUD de Corazones")]
    public GameObject prefabCorazon;
    public Transform contenedorCorazones;

    private List<GameObject> listaCorazones = new List<GameObject>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        panelMuerte.SetActive(false);

        // Inicializa los corazones desde el inicio
        vidaActual = vidaMaxima;
        InicializarCorazones();
        ActualizarCorazones();
    }

    // 🔹 Crea los corazones iniciales según vidaMaxima
    void InicializarCorazones()
    {
        listaCorazones.Clear();

        foreach (Transform hijo in contenedorCorazones)
            Destroy(hijo.gameObject);

        for (int i = 0; i < vidaMaxima; i++)
        {
            GameObject corazon = Instantiate(prefabCorazon, contenedorCorazones);
            listaCorazones.Add(corazon);
        }
    }

    public void TomarDaño(int daño)
    {
        vidaActual -= daño;
        if (vidaActual < 0) vidaActual = 0;

        ActualizarCorazones();
        if (audioSource != null && sonidoDaño != null)
            audioSource.PlayOneShot(sonidoDaño);

        if (vidaActual == 0) Muerte();
    }

    public void Curar(int cantidad)
    {
        vidaActual = Mathf.Min(vidaActual + cantidad, vidaMaxima);
        ActualizarCorazones();

        if (audioSource != null && sonidoCuracion != null)
            audioSource.PlayOneShot(sonidoCuracion);
    }

    // 🔹 Actualiza solo el color de los corazones
    public void ActualizarCorazones()
    {
        for (int i = 0; i < listaCorazones.Count; i++)
        {
            Image img = listaCorazones[i].GetComponent<Image>();
            if (img != null)
            {
                img.color = (i < vidaActual) ? Color.white : Color.black;
            }
        }
    }

    void Muerte()
    {
        if (movimiento != null) movimiento.enabled = false;
        if (panelMuerte != null) panelMuerte.SetActive(true);

        if (audioSource != null && sonidoMuerte != null)
            audioSource.PlayOneShot(sonidoMuerte);

        Time.timeScale = 0f;
    }

    public void Reaparecer()
    {
        vidaActual = vidaMaxima;
        ActualizarCorazones();
        transform.position = puntoReaparicion.position;
        if (movimiento != null) movimiento.enabled = true;
        if (panelMuerte != null) panelMuerte.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Pantalla Título");
    }

    // 🔹 Aplica mejoras de Vitalidad (solo agrega corazones extra)
    public void ApplyVitalityUpgrades(int extraVidas)
    {
        for (int i = 0; i < extraVidas; i++)
        {
            GameObject corazon = Instantiate(prefabCorazon, contenedorCorazones);
            listaCorazones.Add(corazon);
        }

        vidaMaxima += extraVidas;
        vidaActual = vidaMaxima;
        ActualizarCorazones();

        Debug.Log("Mejora aplicada: Vida máxima ahora es " + vidaMaxima);
    }
}