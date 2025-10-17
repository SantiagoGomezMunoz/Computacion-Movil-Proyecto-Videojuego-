using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonSalirHome : MonoBehaviour
{
    public void BotonSalirInicio()
    {
        SceneManager.LoadScene("PantallaTitulo"); // o el nombre exacto de tu escena
    }
}
