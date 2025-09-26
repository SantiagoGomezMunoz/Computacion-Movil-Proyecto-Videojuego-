using UnityEngine;
using UnityEngine.UI;

public class BotonCasillaInventario : MonoBehaviour
{
    public InventarioJugador inventario;
    public int indice;

    public void SeleccionarCasilla()
    {
        if (inventario != null)
        {
            inventario.SeleccionarCasilla(indice);
            Debug.Log("Click en slot de inventario: " + indice);
        }
        else
        {
            Debug.LogWarning("Inventario no asignado en " + gameObject.name);
        }
    }
}