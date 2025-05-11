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
        if (other.CompareTag("Player"))
        {
            GestorObjetivos gestor = FindFirstObjectByType<GestorObjetivos>();
            if (gestor == null) return;

            if (tipoCambio == TipoCambio.ReemplazarTexto)
            {
                gestor.CambiarTextoObjetivo(indiceObjetivo, nuevoTexto);
            }
            else if (tipoCambio == TipoCambio.MarcarComoCompletado)
            {
                gestor.MarcarComoCumplido(indiceObjetivo);
            }

            Destroy(gameObject); // El trigger se autodestruye luego de activarse
        }
    }
}
