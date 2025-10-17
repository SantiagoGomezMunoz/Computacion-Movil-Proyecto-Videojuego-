using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventarioJugador : MonoBehaviour
{
    public Image[] casillas; // Se a asigna en el Inspector
    public GameObject[] objetosEnInventario = new GameObject[4];
    public int casillaSeleccionada = 0;
    public int madera = 0;
    public int piedra = 0;
    public int metal = 0;
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
        if (Input.GetKeyDown(KeyCode.Alpha4)) SeleccionarCasilla(3);

        if (Input.GetKeyDown(KeyCode.Q)) SoltarObjeto();

        //if (Input.GetMouseButtonDown(0)) // Click izquierdo
        //{
        //if (armaEquipada != null)
        //{
        //armaEquipada.Usar();
        //ActualizarUIArma();
        //}
        //else if (armaEquipada == null)
        //{
        //Debug.Log("No tienes un arma equipada.");
        //}
        //}

        if (Input.GetKeyDown(KeyCode.E))
        {
            UsarObjetoCurativo();
        }
    }

    [Header("Textos HUD de materiales")]
    public TMP_Text maderaTexto;
    public TMP_Text piedraTexto;
    public TMP_Text metalTexto;

[Header("Slots visuales de materiales (icono + cantidad)")]
    public Image slotMaderaIcon;
    public TMP_Text slotCantidadMadera;

    public Image slotPiedraIcon;
    public TMP_Text slotCantidadPiedra;

    public Image slotMetalIcon;
    public TMP_Text slotCantidadMetal;

    [Header("Sprites de los recursos")]
    public Sprite iconoMadera;
    public Sprite iconoPiedra;
    public Sprite iconoMetal;

    private void ActualizarInventarioHUD(){
        Debug.Log($"Materiales -> Madera: {madera}, Piedra: {piedra}, Metal: {metal}");
        // Textos HUD simples
        if (maderaTexto != null)
            maderaTexto.text = madera.ToString();
        if (piedraTexto != null)
            piedraTexto.text = piedra.ToString();
        if (metalTexto != null)
            metalTexto.text = metal.ToString();

        // MADERA
        if (slotMaderaIcon != null)
        {
            if (iconoMadera != null) slotMaderaIcon.sprite = iconoMadera;
            slotMaderaIcon.gameObject.SetActive(madera > 0);
        }
        if (slotCantidadMadera != null)
            slotCantidadMadera.text = (madera > 0) ? madera.ToString() : "";

        // PIEDRA
        if (slotPiedraIcon != null)
        {
            if (iconoPiedra != null) slotPiedraIcon.sprite = iconoPiedra;
            slotPiedraIcon.gameObject.SetActive(piedra > 0);
        }
        if (slotCantidadPiedra != null)
            slotCantidadPiedra.text = (piedra > 0) ? piedra.ToString() : "";

        // METAL
        if (slotMetalIcon != null)
        {
            if (iconoMetal != null) slotMetalIcon.sprite = iconoMetal;
            slotMetalIcon.gameObject.SetActive(metal > 0);
        }
        if (slotCantidadMetal != null)
            slotCantidadMetal.text = (metal > 0) ? metal.ToString() : "";
    }
    public void AgregarMaterial(TipoMaterial tipo, int cantidad)
    {
        switch (tipo)
        {
            case TipoMaterial.Madera:
                madera += cantidad;
                Debug.Log($"+{cantidad} madera (Total: {madera})");
            break;
            case TipoMaterial.Piedra:
            piedra += cantidad;
            Debug.Log($"+{cantidad} piedra (Total: {piedra})");
                break;
            case TipoMaterial.Metal:
                metal += cantidad;
                Debug.Log($"+{cantidad} metal (Total: {metal})");
                break;
            }
        ActualizarInventarioHUD();
    }
    
     // Devuelve true si el jugador tiene al menos esas cantidades
    public bool TieneMateriales(int maderaReq, int piedraReq, int metalReq)
    {
        return madera >= maderaReq && piedra >= piedraReq && metal >= metalReq;
    }
    // Intenta quitar las cantidades; devuelve true si se quitaron correctamente.
    // Llama a ActualizarInventarioHUD() internamente si se modifican valores.
    public bool QuitarMateriales(int maderaQuitar, int piedraQuitar, int metalQuitar)
    {
        if (!TieneMateriales(maderaQuitar, piedraQuitar, metalQuitar)) return false;
        madera -= maderaQuitar;
        piedra -= piedraQuitar;
        metal -= metalQuitar;
        ActualizarInventarioHUD();
        
        Debug.Log($"Se entregaron: Madera {maderaQuitar}, Piedra {piedraQuitar}, Metal {metalQuitar}");
        return true;
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
        for (int j = 0; j < objetosEnInventario.Length; j++)
        {
            if (objetosEnInventario[j] == objeto)
            {
                Debug.Log($"El objeto {objeto.name} ya está en el inventario.");
                return false;
            }
        }
        for (int i = 0; i < objetosEnInventario.Length; i++)
        {
            if (objetosEnInventario[i] == null) // Si la casilla está vacía
            {
                objetosEnInventario[i] = objeto;
                objeto.SetActive(false); // ocultamos la instancia real en escena

                ObjetoRecogible data = objeto.GetComponent<ObjetoRecogible>();
                if (data != null && data.iconoHUD != null && casillas[i] != null)
                {
                    casillas[i].sprite = data.iconoHUD;
                    casillas[i].enabled = true;
                }

                if (i == casillaSeleccionada)
                    SeleccionarCasilla(i);

                ActualizarHUD();
                return true;
            }
        }
        Debug.Log("Inventario lleno");
        return false;
    }

    public void SeleccionarCasilla(int indice)
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

    public void SeleccionarCasillaUI(int indice)
    {
        SeleccionarCasilla(indice);
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
            Vector3 basePos = (puntoDeSoltar != null) ? puntoDeSoltar.position : transform.position + transform.forward;
            Vector3 posicionSoltar = basePos + transform.forward * 0.6f;
            objeto.transform.position = posicionSoltar;
            objeto.transform.rotation = Quaternion.identity;
            objeto.transform.SetParent(null, true);
            objeto.SetActive(true);
            Rigidbody rb = objeto.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            ObjetoRecogible data = objeto.GetComponent<ObjetoRecogible>();
            if (data != null)
                data.ResetearEstadoAfterDrop(0.8f);
            objetosEnInventario[casillaSeleccionada] = null;
            armaEquipada = null;
            ActualizarUIArma();
            casillas[casillaSeleccionada].sprite = imagenPorDefecto;
            casillas[casillaSeleccionada].enabled = true;
            ActualizarHUD();
            ActualizarSpriteJugador();
        }
    }

    public void ActualizarUIArma()
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