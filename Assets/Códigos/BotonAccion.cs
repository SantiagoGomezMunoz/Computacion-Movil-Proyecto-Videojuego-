using UnityEngine;

public class BotonAccion : MonoBehaviour
{
    public InventarioJugador inventario;
    public void OnAccion()
    {
        if (inventario == null) return;

        // Si hay un arma equipada
        if (inventario.armaEquipada != null)
        {
            inventario.armaEquipada.Usar();
            inventario.SendMessage("ActualizarUIArma"); // Refresca la munición en HUD
        }
        else
        {
            // Si no hay arma, intentamos usar curativo
            inventario.SendMessage("UsarObjetoCurativo");
        }
    }
}