using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialog : MonoBehaviour, IInteractable
{
    [Header("Configuración de diálogo")]
    [TextArea(3, 8)] public string textoInicial = "Solo he podido encontrar estos objetos para volver a construir mi hogar...";
    [TextArea(3, 8)] public string textoGracias = "Muchas gracias compañero, ya puedo construir mi nuevo hogar. Siga adelante y destruya a esos malditos robots.";

    [Header("Requerimientos")]
    public int reqMadera = 10;
    public int reqPiedra = 5;
    public int reqMetal = 2;

    [Header("Referencias (UI)")]
    public GameObject panelDialogo;
    public Image imagenNPC;
    public TMP_Text textoDialogo;
    public TMP_Text contadorMaderaTxt;
    public TMP_Text contadorPiedraTxt;
    public TMP_Text contadorMetalTxt;
    public Button darMaterialesBtn;
    public Button salirBtn;

    [Header("Referencias runtime")]
    public GameObject barricada;

    [Header("Interacción")]
    public float rangoInteraccion = 2f;
    public Transform puntoProximidad;

    private bool jugadorEnRango = false;
    private InventarioJugador jugadorInventario;
    private TMP_Text accionButtonText;

    void Start()
    {
        if (panelDialogo != null) panelDialogo.SetActive(false);

        if (darMaterialesBtn != null) darMaterialesBtn.onClick.AddListener(OnDarMaterialesClicked);
        if (salirBtn != null) salirBtn.onClick.AddListener(CerrarPanel);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) jugadorInventario = player.GetComponent<InventarioJugador>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        jugadorEnRango = true;
        InteractionManager.Instance?.SetCurrent(this);
        Debug.Log("Jugador entró en rango del NPC");
        InteractionManager.Instance?.SetCurrent(this);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        jugadorEnRango = false;
        Debug.Log("Jugador salió del rango del NPC");
        InteractionManager.Instance?.ClearCurrent(this);
    }

    // 🔹 Implementación de la interfaz IInteractable
    public void Interact()
    {
        Debug.Log("Interact() llamado en NPCDialog, jugadorEnRango = " + jugadorEnRango);
        if (!jugadorEnRango) return; // seguridad extra
        AbrirPanel();
    }

    public string GetActionName() => "Hablar";
    public bool IsAvailable() => jugadorEnRango;

    // 🔹 Métodos propios del NPC
    public void AbrirPanel()
    {
        if (panelDialogo == null) return;
        panelDialogo.SetActive(true);
        if (textoDialogo != null) textoDialogo.text = textoInicial;
        ActualizarContadoresEnPanel();
    }

    public void CerrarPanel()
    {
        if (panelDialogo == null) return;
        panelDialogo.SetActive(false);
    }

    void ActualizarContadoresEnPanel()
    {
        if (jugadorInventario == null) return;
        if (contadorMaderaTxt != null) contadorMaderaTxt.text = $"Madera: {jugadorInventario.madera} / {reqMadera}";
        if (contadorPiedraTxt != null) contadorPiedraTxt.text = $"Piedra: {jugadorInventario.piedra} / {reqPiedra}";
        if (contadorMetalTxt != null) contadorMetalTxt.text = $"Metal: {jugadorInventario.metal} / {reqMetal}";
    }

    void OnDarMaterialesClicked()
    {
        if (jugadorInventario == null) return;

        bool tiene = jugadorInventario.TieneMateriales(reqMadera, reqPiedra, reqMetal);
        if (!tiene)
        {
            if (textoDialogo != null)
                textoDialogo.text = "Te faltan materiales para completar la entrega.";
            ActualizarContadoresEnPanel();
            return;
        }

        bool ok = jugadorInventario.QuitarMateriales(reqMadera, reqPiedra, reqMetal);
        if (!ok)
        {
            if (textoDialogo != null) textoDialogo.text = "Error al quitar materiales.";
            return;
        }

        if (textoDialogo != null) textoDialogo.text = textoGracias;
        if (barricada != null) Destroy(barricada);
        ActualizarContadoresEnPanel();
        if (darMaterialesBtn != null) darMaterialesBtn.interactable = false;
    }

    void ActualizarTextoAccion(string texto)
    {
        if (accionButtonText == null)
        {
            GameObject btnObj = GameObject.FindWithTag("ActionButton");
            if (btnObj != null)
            {
                accionButtonText = btnObj.GetComponentInChildren<TMP_Text>();
            }
        }

        if (accionButtonText != null)
            accionButtonText.text = texto;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 pos = (puntoProximidad != null) ? puntoProximidad.position : transform.position;
        Gizmos.DrawWireSphere(pos, rangoInteraccion);
    }
}