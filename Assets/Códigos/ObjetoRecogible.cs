using UnityEngine;
using System.Collections;

public class ObjetoRecogible : MonoBehaviour
{
    public Sprite iconoHUD;

    private bool puedeRecogerse = true;

    void OnTriggerEnter(Collider other)
    {
        if (puedeRecogerse && other.CompareTag("Player"))
        {
            InventarioJugador inventario = other.GetComponent<InventarioJugador>();
            if (inventario != null)
            {
                if (inventario.AgregarObjeto(gameObject))
                {
                    Debug.Log("Objeto recogido por el jugador");
                }
            }
        }
    }

    public void ActivarCooldown()
    {
        puedeRecogerse = false;
        StartCoroutine(HabilitarRecoleccion());
    }

    IEnumerator HabilitarRecoleccion()
    {
        yield return new WaitForSeconds(2f);
        puedeRecogerse = true;
    }

   
}
