using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonSeleccion : MonoBehaviour
{
    public Sprite spriteDelPersonaje; // Asigna aquí el sprite del personaje desde el Inspector

    public void Seleccionar()
    {
        PersonajeSeleccionado.Instancia.SeleccionarPersonaje(spriteDelPersonaje);
        SceneManager.LoadScene("EscenaPrincipal"); // cambia a tu escena de juego
    }
}
