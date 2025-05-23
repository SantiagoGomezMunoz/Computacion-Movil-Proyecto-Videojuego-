using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventarioJugador : MonoBehaviour
{
    public Image[] casillas; // Se a asigna en el Inspector
    public GameObject[] objetosEnInventario = new GameObject[3];
    public int casillaSeleccionada = 0;
    public Transform puntoDeSoltar;
    public Sprite imagenPorDefecto;
    public IArma armaEquipada;
    public TMP_Text textoMunicion;

    private AnimacionJugador animacionJugador;

    void Start() 
    {
        animacionJugador = GetComponent<AnimacionJugador>();

        if (textoMunicion != null)
        {
            textoMunicion.enabled = false;  
        }
    }

    void Update()
    {
        // Cambio de tecla para verificar el arma equipada en cada slot
        if (Input.GetKeyDown(KeyCode.Alpha1)) SeleccionarCasilla(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SeleccionarCasilla(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SeleccionarCasilla(2);
        
        if (Input.GetKeyDown(KeyCode.Q)) SoltarObjeto();
        
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            if (armaEquipada != null)
            {
                armaEquipada.Usar();
                ActualizarUIArma();
            }
            else if (armaEquipada == null)
            {
                Debug.Log("No tienes un arma equipada.");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UsarObjetoCurativo();
        }
    }

    void UsarObjetoCurativo()
    {
        GameObject objeto = objetosEnInventario[casillaSeleccionada];
        if (objeto == null) return;
        
        ObjetoCurativo curativo = objeto.GetComponent<ObjetoCurativo>();
        if (curativo != null)
        {
            VidaJugador vida = GetComponent<VidaJugador>(); 
            if (vida == null) return;
            if (vida.vidaActual >= vida.vidaMaxima)
            {
                Debug.Log("Tienes tu vida al máximo.");
                return;
            }
            
            int vidasACurar = Mathf.Min(curativo.cantidadCuracion, vida.vidaMaxima - vida.vidaActual);
            vida.Curar(vidasACurar);

            // Eliminar el objeto del inventario
            objetosEnInventario[casillaSeleccionada] = null;
            casillas[casillaSeleccionada].sprite = imagenPorDefecto;
            casillas[casillaSeleccionada].enabled = true;
            
            armaEquipada = null;
            ActualizarUIArma();
            ActualizarSpriteJugador();
            Destroy(objeto);
            Debug.Log($"Consumiste un bendaje y curaste {vidasACurar} corazones.");
        }
    }

    public bool AgregarObjeto(GameObject objeto)
    {
        for (int i = 0; i < objetosEnInventario.Length; i++)
        {
            if (objetosEnInventario[i] == null) // Si la casilla está vacía
            {
                objetosEnInventario[i] = objeto;  // Asigna el objeto al inventario
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
                IArma arma = objeto.GetComponent<IArma>();  // Se obtiene el componente Arma
                if (arma != null) // Si tiene un componente Arma
                {
                    armaEquipada = arma;  // Se asigna el arma al inventario
                    Debug.Log("Arma equipada: " + arma.GetType().Name);
                }

                if (i == casillaSeleccionada)
                {
                    SeleccionarCasilla(i);
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

        GameObject objeto = objetosEnInventario[casillaSeleccionada];
        if (objeto != null)
        {
            armaEquipada = objeto.GetComponent<IArma>();
            Debug.Log("Arma seleccionada: " + (armaEquipada != null ? armaEquipada.GetType().Name : "Ninguna"));
        }
        else
        {
        armaEquipada = null;
        }

        ActualizarHUD();
        ActualizarUIArma();
        ActualizarSpriteJugador();
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
            armaEquipada = null;
            ActualizarUIArma();
            // Cambia nuevamente el sprite del slot
            casillas[casillaSeleccionada].sprite = imagenPorDefecto;
            Debug.Log("Sprite restablecido a: " + imagenPorDefecto);
            casillas[casillaSeleccionada].enabled = true;

            ActualizarHUD();
            ActualizarSpriteJugador();
        }
    }

    void ActualizarUIArma()
    {
        if (armaEquipada != null)
        {
            Arma armaDistancia = armaEquipada as Arma;
            if (armaDistancia != null && textoMunicion != null)
            {
                textoMunicion.text = armaDistancia.municionActual + "/" + armaDistancia.municionMaxima;
                textoMunicion.enabled = true;
            }
            else
            {
                textoMunicion.enabled = false;
            }
        }
        else
        {
            if (textoMunicion != null)
            textoMunicion.enabled = false;
        }
    }

    public string[] ObtenerIDsObjetos()
    {
        string[] ids = new string[objetosEnInventario.Length];
        for (int i = 0; i < objetosEnInventario.Length; i++)
        {
            if (objetosEnInventario[i] != null)
            {
                ObjetoRecogible recogible = objetosEnInventario[i].GetComponent<ObjetoRecogible>();
                if (recogible != null)
                {
                    ids[i] = recogible.ID;
                }
            }
        }
        return ids;
    }
    
    public int ObtenerMunicionActual()
    {
        Arma arma = armaEquipada as Arma;
        return arma != null ? arma.municionActual : 0;
    }

    public void RestaurarInventario(string[] ids, int indiceSeleccionado, int municion)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            if (!string.IsNullOrEmpty(ids[i]))
            {
                GameObject prefab = GestorGuardado.ObtenerPrefabPorID(ids[i]);
                if (prefab != null)
                {
                    GameObject obj = Instantiate(prefab);
                    AgregarObjeto(obj); // Se coloca en el primer espacio libre
                }
            }
        }
        
        SeleccionarCasilla(indiceSeleccionado);
        
        Arma arma = armaEquipada as Arma;
        if (arma != null)
        {
            arma.municionActual = municion;
            ActualizarUIArma();
        }
    }

    public void VaciarInventario()
    {
        for (int i = 0; i < objetosEnInventario.Length; i++)
        {
            objetosEnInventario[i] = null;
            if (casillas != null && i < casillas.Length && casillas[i] != null)
            { 
                casillas[i].sprite = imagenPorDefecto;
                casillas[i].enabled = true;
                casillas[i].color = Color.white;
            }  
        }

            casillaSeleccionada = 0;
            armaEquipada = null;
            ActualizarUIArma();
            ActualizarHUD();
            ActualizarSpriteJugador();
            Debug.Log("Inventario vaciado.");
    }

    void ActualizarSpriteJugador()
    {
        if (animacionJugador == null) return;
        
        GameObject objeto = objetosEnInventario[casillaSeleccionada];
        if (objeto == null)
        {
            animacionJugador.DesequiparItem(); // Vuelve al sprite por defecto
            return;
        }
        
        ObjetoRecogible recogible = objeto.GetComponent<ObjetoRecogible>();
        if (recogible != null)
        {
            animacionJugador.EquiparItem(recogible.tipoItemEquipado); // Cambia sprite según tipo
        }
        else
        {
            animacionJugador.DesequiparItem(); // Si no tiene tipo definido, usa el default
        }
    }
}
