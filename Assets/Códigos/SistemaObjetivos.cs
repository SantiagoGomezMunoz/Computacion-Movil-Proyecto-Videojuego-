using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GestorObjetivos : MonoBehaviour
{
    public TextMeshProUGUI textoHacha;
    public TextMeshProUGUI textoFusil;
    public TextMeshProUGUI textoBotiquin;

    public List<TextMeshProUGUI> listaObjetivos = new List<TextMeshProUGUI>(); // <- LLENA ESTO EN EL INSPECTOR

    private Dictionary<string, TextMeshProUGUI> objetivos = new Dictionary<string, TextMeshProUGUI>();

    void Start()
    {
        objetivos.Add("Hacha", textoHacha);
        objetivos.Add("Fusil", textoFusil);
        objetivos.Add("Botiquin", textoBotiquin);

        if (listaObjetivos.Count == 0) // Por si no se llenó manualmente
        {
            listaObjetivos.Add(textoHacha);
            listaObjetivos.Add(textoFusil);
            listaObjetivos.Add(textoBotiquin);
        }
    }

    public void MarcarComoCumplido(string id)
    {
        if (objetivos.ContainsKey(id))
        {
            objetivos[id].color = Color.green;
            Debug.Log("Objetivo cumplido: " + id);
        }
    }

    public void MarcarComoCumplido(int indice)
    {
        if (indice >= 0 && indice < listaObjetivos.Count)
        {
            listaObjetivos[indice].color = Color.green;
            Debug.Log("Objetivo cumplido en índice: " + indice);
        }
    }

    public void CambiarTextoObjetivo(int indice, string nuevoTexto)
    {
        if (indice >= 0 && indice < listaObjetivos.Count)
        {
            // Reemplaza el texto del objetivo indicado
            listaObjetivos[indice].text = nuevoTexto;
            listaObjetivos[indice].color = Color.white;
            
            // Quita los textos de los demás objetivos
            for (int i = 0; i < listaObjetivos.Count; i++)
            {
                if (i != indice)
                {
                    listaObjetivos[i].text = "";
                }
            }
            Debug.Log("Texto del objetivo actualizado en índice: " + indice + " y otros vaciados.");
        }
    }
}