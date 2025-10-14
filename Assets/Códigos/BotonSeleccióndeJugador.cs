using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonPersonajes : MonoBehaviour
{
    public void AbrirPantallaPersonajes()
    {
        SceneManager.LoadScene("SeleccionPersonaje"); // o el nombre exacto de tu escena
    }
}
