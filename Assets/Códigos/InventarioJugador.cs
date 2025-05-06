using UnityEngine;
using UnityEngine.UI;

public class InventarioJugador : MonoBehaviour
{
    public Image[] casillas; // Asigna en el Inspector
    public GameObject[] objetosEnInventario = new GameObject[3];
    public int casillaSeleccionada = 0;
    public Transform puntoDeSoltar;
    public Sprite imagenPorDefecto;

    void Start() 
    {
    Debug.Log($"🟡 Start() de InventarioJugador en objeto: {gameObject.name} — imagenPorDefecto: {(imagenPorDefecto != null ? imagenPorDefecto.name : "NULL")}", gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SeleccionarCasilla(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SeleccionarCasilla(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SeleccionarCasilla(2);

        if (Input.GetKeyDown(KeyCode.Q)) SoltarObjeto();
    }

    public bool AgregarObjeto(GameObject objeto)
    {
        for (int i = 0; i < objetosEnInventario.Length; i++)
        {
            if (objetosEnInventario[i] == null)
            {
                objetosEnInventario[i] = objeto;
                objeto.SetActive(false);
                ObjetoRecogible data = objeto.GetComponent<ObjetoRecogible>();

                if (data != null && data.iconoHUD != null && casillas[i] != null)
                {
                    casillas[i].sprite = data.iconoHUD;
                    casillas[i].enabled = true;
                }
                else
                {
                    Debug.LogWarning("Falta asignar casilla o ícono HUD en el objeto.");
                }

                ActualizarHUD();
                return true;
            }
        }

        Debug.Log("Inventario lleno");
        return false;
    }

    void SeleccionarCasilla(int indice)
    {
        casillaSeleccionada = indice;
        ActualizarHUD();
    }

    void ActualizarHUD()
    {
        for (int i = 0; i < casillas.Length; i++)
        {
            if (casillas[i] != null)
            {
                casillas[i].color = (i == casillaSeleccionada) ? new Color(0.5f, 0.5f, 0.5f) : Color.white;
            }
        }
    }

    void SoltarObjeto()
    {
        GameObject objeto = objetosEnInventario[casillaSeleccionada];
        if (objeto != null)
        {
            // Si tienes punto de soltar asignado, úsalo. Si no, suéltalo frente al jugador.
            Vector3 posicionSoltar = (puntoDeSoltar != null)
                ? puntoDeSoltar.position
                : transform.position + transform.forward;

            objeto.transform.position = posicionSoltar;
            objeto.SetActive(true);

            ObjetoRecogible data = objeto.GetComponent<ObjetoRecogible>();
            if (data != null)
            {
                data.ActivarCooldown();
            }

            objetosEnInventario[casillaSeleccionada] = null;
            // Cambia nuevamente el sprite del slot
            casillas[casillaSeleccionada].sprite = imagenPorDefecto;
            Debug.Log("Sprite restablecido a: " + imagenPorDefecto);
            casillas[casillaSeleccionada].enabled = true;

            ActualizarHUD();
        }
    }
}