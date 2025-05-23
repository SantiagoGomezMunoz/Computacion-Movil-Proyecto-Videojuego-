using UnityEngine;
using System.Collections;


public class ObjetoRecogible : MonoBehaviour
{
    public string ID;
    public Sprite iconoHUD;
    public TipoItemEquipado tipoItemEquipado = TipoItemEquipado.Ninguno;
    private bool puedeRecogerse = true;
    public bool fueRecogido = false; // Para saber si ya fue recogido

    private GestorObjetivos gestorObjetivos;

    void Start()
    {
        // Si el objeto fue marcado como recogido en una partida previa, se desactiva
        if (GestorGuardado.ObjetoFueRecogido(ID))
        {
            fueRecogido = true;
            gameObject.SetActive(false);
        }

        gestorObjetivos = FindFirstObjectByType<GestorObjetivos>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (puedeRecogerse && other.CompareTag("Player"))
        {
            InventarioJugador inventario = other.GetComponent<InventarioJugador>();
            if (inventario != null)
            {

                // Verifica si el objeto ya ha sido recogido, de no ser así lo agrega
                if (GestorGuardado.ObtenerListaObjetosRecogidos().Contains(ID))
                {
                    return;  // Si el objeto ya está recogido, no hacer nada
                }
                
                if (inventario.AgregarObjeto(gameObject))
                {
                    fueRecogido = true;
                    GestorGuardado.MarcarObjetoComoRecogido(ID); // Guarda que fue recogido
                    gameObject.SetActive(false); 
                    Debug.Log("Objeto recogido por el jugador");
                }

                if (gestorObjetivos != null)
                {
                    gestorObjetivos.MarcarComoCumplido(ID);
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
