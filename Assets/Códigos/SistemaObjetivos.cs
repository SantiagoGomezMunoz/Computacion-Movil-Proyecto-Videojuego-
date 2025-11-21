using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GestorObjetivos : MonoBehaviour
{
    [Header("Referencias individuales (opcional)")]
    public TextMeshProUGUI textoHacha;
    public TextMeshProUGUI textoNPC;
    public TextMeshProUGUI textoBotiquin;

    [Header("Lista de objetivos (en orden, rellenar en inspector)")]
    public List<TextMeshProUGUI> listaObjetivos = new List<TextMeshProUGUI>();

    // Diccionario ID -> TMP (para marcar por ID)
    private Dictionary<string, TextMeshProUGUI> objetivosPorID = new Dictionary<string, TextMeshProUGUI>();

    void Start()
    {
        // Llenado por si no rellenaste la lista manualmente (compatibilidad)
        if (listaObjetivos.Count == 0)
        {
            if (textoHacha != null) listaObjetivos.Add(textoHacha);
            if (textoNPC != null) listaObjetivos.Add(textoNPC);
            if (textoBotiquin != null) listaObjetivos.Add(textoBotiquin);
        }

        // Inicializar diccionario con IDs conocidos — puedes cambiar los keys a lo que uses en tus prefabs
        // Asegúrate de que estos IDs coincidan con los campos ID en tus prefabs ObjetoRecogible (por ejemplo "Hacha", "Botiquin")
        objetivosPorID.Clear();
        // Si tienes 3 objetivos en la lista: [0]=Hacha, [1]=NPC, [2]=Botiquin (asegúrate en inspector)
        if (listaObjetivos.Count >= 1) objetivosPorID["Hacha"] = listaObjetivos[0];
        if (listaObjetivos.Count >= 2) objetivosPorID["NPC"] = listaObjetivos[1];
        if (listaObjetivos.Count >= 3) objetivosPorID["Botiquin"] = listaObjetivos[2];

        // Reset visual inicial a blanco (o al color que uses)
        foreach (var t in listaObjetivos)
        {
            if (t != null)
            {
                t.color = Color.white;
            }
        }
    }

    // Marca por ID (por ejemplo "Hacha", "Botiquin", "NPC")
    public void MarcarComoCumplido(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        if (objetivosPorID.TryGetValue(id, out TextMeshProUGUI txt))
        {
            if (txt != null)
            {
                txt.color = Color.green;
                Debug.Log($"[GestorObjetivos] Objetivo '{id}' marcado como cumplido (por ID).");
            }
        }
    }

    // Marca por índice (0..n-1) — útil si usas triggers que llaman por índice
    public void MarcarComoCumplido(int indice)
    {
        if (indice >= 0 && indice < listaObjetivos.Count)
        {
            if (listaObjetivos[indice] != null)
            {
                listaObjetivos[indice].color = Color.green;
                Debug.Log($"[GestorObjetivos] Objetivo en índice {indice} marcado como cumplido.");
            }
        }
        else
        {
            Debug.LogWarning($"[GestorObjetivos] Índice inválido: {indice}");
        }
    }

    // Reemplaza texto del objetivo en posición 'indice' y deja los demás en blanco (tu comportamiento previo)
    public void CambiarTextoObjetivo(int indice, string nuevoTexto)
    {
        if (indice >= 0 && indice < listaObjetivos.Count)
        {
            for (int i = 0; i < listaObjetivos.Count; i++)
            {
                if (listaObjetivos[i] != null)
                {
                    if (i == indice)
                    {
                        listaObjetivos[i].text = nuevoTexto;
                        listaObjetivos[i].color = Color.white;
                    }
                    else
                    {
                        listaObjetivos[i].text = "";
                    }
                }
            }
            Debug.Log($"[GestorObjetivos] Cambiado texto del objetivo {indice} a '{nuevoTexto}'");
        }
        else Debug.LogWarning($"[GestorObjetivos] CambiarTextoObjetivo: índice fuera de rango ({indice})");
    }
}