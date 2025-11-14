using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionPersonaje : MonoBehaviour
{
    public string escenaSiguiente = "Historia";

    public void Seleccionar(int indicePersonaje)
    {
        PlayerPrefs.SetInt("SelectedCharacter", indicePersonaje);
        PlayerPrefs.Save();
        // Cargar la escena siguiente
        if (!string.IsNullOrEmpty(escenaSiguiente))
            SceneManager.LoadScene(escenaSiguiente);
    }
}