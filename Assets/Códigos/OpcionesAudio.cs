using UnityEngine;
using UnityEngine.UI;

public class OpcionesAudio : MonoBehaviour
{
    public Toggle toggleMusica;
    public Toggle toggleEfectos;

    private void Start()
    {
        // Cargar preferencias previas
        toggleMusica.isOn = PlayerPrefs.GetInt("MusicaActiva", 1) == 1;
        toggleEfectos.isOn = PlayerPrefs.GetInt("EfectosActivos", 1) == 1;

        // Aplicar configuraciones iniciales
        AplicarConfiguracion();

        // Escuchar cambios
        toggleMusica.onValueChanged.AddListener(delegate { CambiarMusica(); });
        toggleEfectos.onValueChanged.AddListener(delegate { CambiarEfectos(); });
    }

    public void CambiarMusica()
    {
        PlayerPrefs.SetInt("MusicaActiva", toggleMusica.isOn ? 1 : 0);
        PlayerPrefs.Save();
        AplicarConfiguracion();
    }

    public void CambiarEfectos()
    {
        PlayerPrefs.SetInt("EfectosActivos", toggleEfectos.isOn ? 1 : 0);
        PlayerPrefs.Save();
        AplicarConfiguracion();
    }

    private void AplicarConfiguracion()
    {
        // Música
        AudioListener.pause = !toggleMusica.isOn;

        // Efectos (los controlaremos por etiqueta o mixer luego)
        bool efectosActivos = toggleEfectos.isOn;
        foreach (var fuente in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
        {
            if (fuente.CompareTag("EfectoSonido"))
                fuente.mute = !efectosActivos;
        }
    }

    public void CerrarVentana()
    {
        gameObject.SetActive(false);
    }
}