using UnityEngine;

public class TriggerCambioObjetivo : MonoBehaviour
{
    public enum TipoCambio
    {
        ReemplazarTexto,
        MarcarComoCompletado
    }

    public TipoCambio tipoCambio;
    [TextArea] public string nuevoTexto;
    public int indiceObjetivo = 0; // El índice del objetivo a modificar en la lista

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GestorObjetivos gestor = FindFirstObjectByType<GestorObjetivos>();
        if (gestor == null)
        {
            Debug.LogWarning("[TriggerCambioObjetivo] No se encontró GestorObjetivos en la escena.");
            return;
        }

        // Trabajo según tipo de cambio
        if (tipoCambio == TipoCambio.ReemplazarTexto)
        {
            // Validar índice
            if (gestor.listaObjetivos != null && indiceObjetivo >= 0 && indiceObjetivo < gestor.listaObjetivos.Count)
            {
                // Reemplazar el texto en el índice indicado
                gestor.listaObjetivos[indiceObjetivo].text = nuevoTexto;
                gestor.listaObjetivos[indiceObjetivo].color = Color.white;

                // Vaciar los textos de los demás objetivos (igual comportamiento anterior)
                for (int i = 0; i < gestor.listaObjetivos.Count; i++)
                {
                    if (i == indiceObjetivo) continue;
                    if (gestor.listaObjetivos[i] != null)
                        gestor.listaObjetivos[i].text = "";
                }

                Debug.Log($"[TriggerCambioObjetivo] Reemplazado objetivo {indiceObjetivo} por: {nuevoTexto}");
            }
            else
            {
                Debug.LogWarning($"[TriggerCambioObjetivo] Indice inválido ({indiceObjetivo}) o lista no asignada en GestorObjetivos.");
            }
        }
        else if (tipoCambio == TipoCambio.MarcarComoCompletado)
        {
            // Llamamos al método público existente para marcar como cumplido por índice
            gestor.MarcarComoCumplido(indiceObjetivo);
            Debug.Log($"[TriggerCambioObjetivo] Marcado como cumplido objetivo índice {indiceObjetivo}.");
        }

        // Autodestruir el trigger (opcional)
        Destroy(gameObject);
    }
}