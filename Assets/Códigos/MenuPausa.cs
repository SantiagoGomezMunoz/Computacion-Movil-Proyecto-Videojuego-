using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI; 
    private bool estaPausado = false;

    void Start()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BotonPausa()
    {
        if (estaPausado)
            ReanudarJuego();
        else
            PausarJuego();
    }

    public void ReanudarJuego()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        estaPausado = false;
    }

    public void PausarJuego()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        estaPausado = true;
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PantallaTitulo");
    }
}