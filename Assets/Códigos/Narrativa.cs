using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class IntroNarrativa : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    public float velocidadEscritura = 0.1f;

    [TextArea(3, 5)]
    public List<string> parrafosHistoria = new List<string>
    {
        "Es el año 2060.\nEl mundo fue sometido por la revelión de las máquinas, todas al mando de su creadora...\nLa Super Inteligencia Aritificial HM2, acabó con todos los humanos.\nEspecie que a comparación de la inteligencia de las máquinas, era supremamente inferior. Su dependencia a estas, la cual surgió en décadas pasadas fue su perdición.",

        "Pero un hombre sobreviviría para ver los vestigios del apocalípsis, el mundo que conoció devastado. Las máquinas le quitaron a su familia, y dentro de sí solo surgía un sentimiento profundo de venganza.\n\nDecide salir de su escondite, necesita llegar a la gran ciudad para encontrar más respuestas y conocer así el camino que debe seguir para encontrar la forma de solucionar la catástrofe que le quitó su felicidad.",

        "Se armará, y no solo de valor.\nSi no también con armas para lograr su cometido, buscará la forma de abrirse paso hasta su destino.\n\nJurando ser la única persona en este mundo... O eso es lo que él cree."
    };

    private int indiceActual = 0;
    private bool escribiendo = false;
    private bool historiaTerminada = false;

    void Start()
    {
        StartCoroutine(EscribirTexto(parrafosHistoria[indiceActual]));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // click izquierdo o tap
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
            StartCoroutine(EscribirTexto(parrafosHistoria[indiceActual]));
        }
        else
        {
            historiaTerminada = true;
            textoUI.text = "Presione Enter para empezar el juego";
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
}