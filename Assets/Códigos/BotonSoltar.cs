using UnityEngine;

public class BotonSoltar : MonoBehaviour
{
    public InventarioJugador inventario;
    public void OnSoltar()
    {
        if (inventario == null) return;

        inventario.SendMessage("SoltarObjeto");
    }
}