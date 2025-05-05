using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 5;
    public int vidaActual;

    public GameObject[] corazones; // Ahora arrastras directamente los objetos visuales

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarCorazones();
    }

    public void TomarDaño(int daño)
    {
        vidaActual -= daño;
        if (vidaActual < 0) vidaActual = 0;

        ActualizarCorazones();
    }

    void ActualizarCorazones()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            corazones[i].SetActive(i < vidaActual); // Oculta el objeto completo
        }
    }
}