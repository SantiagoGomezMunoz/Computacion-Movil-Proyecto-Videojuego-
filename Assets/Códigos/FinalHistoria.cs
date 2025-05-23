using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalHistoria : MonoBehaviour
{
    public TMP_Text textoUI;
    public float velocidadTexto = 0.05f;
    [TextArea(4, 10)]
    public string[] parrafos = {"-Presione Enter para continuar- \nEl jugador encontró una radio en el suelo, algo rota pero funcional al lado de un carro policial donde a pesar de las bajas frecuencias aún se podía escuchar a un agente solicitando refuerzos para contrarrestar los ataques de los androides en un refugio de supervivientes.",
    "-Presione Enter para continuar- \nEl hombre está sorprendido, ya que pensó que era la única persona con vida en el mundo, sin pensarlo tomar la rapido y responde a la ayuda. \n- Hola, soy un superviviente... Iré en camino.", "-Presione Enter para continuar- \nQuizás este encuentro con los supervivientes le ayude al hombre a obtener las respuestas que necesita y lo lleve a salvar al mundo... O lo que queda de él.",
    "-Presione Enter para salir al menú- \nLa Beta del videojuego CypherCTRL ha finalizado. \nMuchas gracias por jugarlo."};
    private int indiceParrafo = 0;
    private bool escribiendo = false;
    private Coroutine escrituraActual;

    void Start()
    {
        MostrarSiguienteParrafo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (escribiendo)
            {
                StopCoroutine(escrituraActual);
                textoUI.text = parrafos[indiceParrafo];
                escribiendo = false;
            }
            else
            {
                indiceParrafo++;
                if (indiceParrafo < parrafos.Length)
                {
                    MostrarSiguienteParrafo();
                }
                else
                {
                    SceneManager.LoadScene("PantallaTitulo"); 
                }
            }
        }
    }

    void MostrarSiguienteParrafo()
    {
        escrituraActual = StartCoroutine(EscribirTexto(parrafos[indiceParrafo]));
    }

    IEnumerator EscribirTexto(string parrafo)
    {
        escribiendo = true;
        textoUI.text = "";
        foreach (char letra in parrafo.ToCharArray())
        {
            textoUI.text += letra;
            yield return new WaitForSeconds(velocidadTexto);
        }
        escribiendo = false;
    }
}