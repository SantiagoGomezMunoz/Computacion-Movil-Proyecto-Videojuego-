using UnityEngine;

public class BarricadaDestructible : MonoBehaviour
{
    public int golpesParaDestruir = 5;
    private int golpesRecibidos = 0;
    private Renderer rend;
    private Color colorOriginal;

    void Start()
    {
        rend = GetComponent<Renderer>();
        colorOriginal = rend.material.color;
    }

    public void RecibirGolpe(string arma)
    {
        if (arma == "Hacha") // Solo funciona si tiene el hacha
        {
            golpesRecibidos++;
            StartCoroutine(ParpadeoRojo());

            if (golpesRecibidos >= golpesParaDestruir)
            {
                gameObject.SetActive(false);
                Debug.Log("Barricada destruida");
            }
        }
    }

    private System.Collections.IEnumerator ParpadeoRojo()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = colorOriginal;
    }
}