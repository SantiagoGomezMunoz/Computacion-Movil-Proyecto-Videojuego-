using UnityEngine;

public class BarricadaDestructibleBasica : MonoBehaviour
{
    public int golpesParaDestruir = 3;
    private int golpesRecibidos = 0;
    public bool destruida = false;

    public void RecibirGolpe(string arma)
    {
        if (arma == "Hacha")
        {
            golpesRecibidos++;
            if (golpesRecibidos >= golpesParaDestruir)
            {
                destruida = true;
                Debug.Log("Barricada destruida");
            }
        }
    }
}