using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonSeleccionPersonaje : MonoBehaviour
{
    public DatosPersonaje datosDelPersonaje;
    public string escenaJuego = "EscenaPrincipal";

    public void AlSeleccionarPersonaje()
    {
        PersonajeSeleccionado.Instancia.SeleccionarPersonaje(datosDelPersonaje);
        SceneManager.LoadScene(escenaJuego);
    }
}
