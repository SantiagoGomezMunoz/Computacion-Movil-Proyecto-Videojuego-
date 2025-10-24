using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IntroNarrativa : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    public Image imagenUI;
    public float velocidadEscritura = 0.1f;

    [TextArea(3, 5)]
    public List<string> parrafosHistoria = new List<string>
    {
        "Es el año 2060.\nEl mundo fue sometido por la revelión de las máquinas, todas al mando de su creadora...\nLa Super Inteligencia Aritificial HM2.",

        "La cual acabó con todos los humanos.\nEspecie que a comparación de la inteligencia de las máquinas, era supremamente inferior. Su dependencia a estas, la cual surgió en décadas pasadas fue su perdición.",

        "Muy poca gente sobreviviría para ver los vestigios del apocalípsis, entre ellos un hombre quien ahora ve mundo que conoció devastado.",

        "Las máquinas le quitaron a su familia, y dentro de sí solo surgía un sentimiento profundo de venganza.\n\nDecide salir de su escondite.",

        "Necesita llegar a la gran ciudad para encontrar más respuestas y conocer así el camino que debe seguir para encontrar la forma de solucionar la catástrofe que le quitó su felicidad.",

        "Se armará, y no solo de valor.\nSi no también con armas para lograr su cometido, buscará la forma de abrirse paso hasta su destino.",
    };
    public List<Sprite> imagenesHistoria = new List<Sprite>();

    private int indiceActual = 0;
    private bool escribiendo = false;
    private bool historiaTerminada = false;

    void Start()
    {
        ActualizarImagen();
        StartCoroutine(EscribirTexto(parrafosHistoria[indiceActual]));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // tap
        {
            if (escribiendo)
            {
                // Termina de mostrar el texto al instante
                StopAllCoroutines();
                textoUI.text = parrafosHistoria[indiceActual];
                escribiendo = false;
            }
            else
            {
                if (!historiaTerminada)
                    MostrarSiguienteParrafo();
                else
                    SceneManager.LoadScene("EscenaPrincipal");
            }
        }
    }

    void MostrarSiguienteParrafo()
    {
        indiceActual++;
        if (indiceActual < parrafosHistoria.Count)
        {
            ActualizarImagen();
            StartCoroutine(EscribirTexto(parrafosHistoria[indiceActual]));
        }
        else
        {
            historiaTerminada = true;
        }
    }

    IEnumerator EscribirTexto(string texto)
    {
        escribiendo = true;
        textoUI.text = "";

        foreach (char letra in texto)
        {
            textoUI.text += letra;
            yield return new WaitForSeconds(velocidadEscritura);
        }

        escribiendo = false;
    }

    void ActualizarImagen()
    {
        if (imagenesHistoria.Count > indiceActual && imagenesHistoria[indiceActual] != null)
        {
            imagenUI.sprite = imagenesHistoria[indiceActual];
            imagenUI.enabled = true;

            RectTransform rect = imagenUI.rectTransform;
            rect.sizeDelta = new Vector2(8f, 6f);
            rect.anchoredPosition = new Vector2(0f, 50f);

            imagenUI.preserveAspect = true;
        }
        else
        {
            imagenUI.enabled = false; // por si hay menos imágenes que textos
        }
    }
}