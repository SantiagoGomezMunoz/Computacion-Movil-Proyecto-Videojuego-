using UnityEngine;
using System.Collections.Generic;

public class GestorObjetivosBasico : MonoBehaviour
{
    public List<string> objetivos = new List<string>();
    public List<bool> objetivosCumplidos = new List<bool>();

    public void Start()
    {
        for (int i = 0; i < objetivos.Count; i++)
        {
            objetivosCumplidos.Add(false);
        }
    }

    public void MarcarComoCumplido(int indice)
    {
        if (indice >= 0 && indice < objetivosCumplidos.Count)
        {
            objetivosCumplidos[indice] = true;
            Debug.Log("Objetivo cumplido en índice: " + indice);
        }
    }

    public void CambiarTextoObjetivo(int indice, string nuevoTexto)
    {
        if (indice >= 0 && indice < objetivos.Count)
        {
            objetivos[indice] = nuevoTexto;
            Debug.Log("Texto del objetivo actualizado en índice: " + indice);
        }
    }
}