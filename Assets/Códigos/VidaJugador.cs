using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 5;
    public int vidaActual = 5;

    public GameObject[] corazones; 

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

    public void RecibirDaño(int cantidad)
    {
        TomarDaño(cantidad); 
    }

    public void Curar(int cantidad)
    {
        vidaActual = Mathf.Min(vidaActual + cantidad, vidaMaxima);
        Debug.Log("Curado. Vidas actuales: " + vidaActual);
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
