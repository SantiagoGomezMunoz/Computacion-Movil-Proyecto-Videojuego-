using UnityEngine;
using UnityEngine.SceneManagement;
public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 5;
    public int vidaActual = 5;

    public GameObject[] corazones; 
    public GameObject panelMuerte; 
    public MovimientoPersonaje movimiento; // Script de movimiento del jugador
    private Vector3 puntoInicial;
    public Transform puntoReaparicion;
    public AudioClip sonidoDaño; 
    public AudioClip sonidoCuracion; 
    public AudioClip sonidoMuerte; 
    private AudioSource audioSource;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarCorazones();
        panelMuerte.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void TomarDaño(int daño)
    {
        vidaActual -= daño;
        if (vidaActual < 0) vidaActual = 0;

        ActualizarCorazones();

        if (audioSource != null && sonidoDaño != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }

        if (vidaActual == 0)
        {
            Muerte();
        }
    }

    public void RecibirDaño(int cantidad)
    {
        TomarDaño(cantidad); 
    }

    public void Curar(int cantidad)
    {
        vidaActual = Mathf.Min(vidaActual + cantidad, vidaMaxima);
        Debug.Log("Curado. Vidas actuales: " + vidaActual);
        ActualizarCorazones(); 

        if (audioSource != null && sonidoCuracion != null)
        {
            audioSource.PlayOneShot(sonidoCuracion); 
        }
    }

    void ActualizarCorazones()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            
            corazones[i].SetActive(i < vidaActual); // Oculta el objeto completo
            
        }
    }
        void Muerte()
    {
        if (movimiento != null) movimiento.enabled = false;
        if (panelMuerte != null) panelMuerte.SetActive(true);

        if (audioSource != null && sonidoMuerte != null)
        {
            audioSource.PlayOneShot(sonidoMuerte); 
        }

        Time.timeScale = 0f; // Pausa el juego
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
}
