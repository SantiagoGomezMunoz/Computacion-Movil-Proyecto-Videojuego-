using UnityEngine;
using System.Collections;

public class ObjetoRecogible : MonoBehaviour
{
    public string ID;
    public Sprite iconoHUD;
    public TipoItemEquipado tipoItemEquipado = TipoItemEquipado.Ninguno;

    [Tooltip("Si true: marcado como persistente (no reaparece cuando se suelta).")]
    public bool persistente = false;

    [Header("Pick up")]
    [Tooltip("Tiempo que tarda en poder recogerse otra vez tras soltar.")]
    public float pickupCooldownOnDrop = 0.6f;

    private bool puedeRecogerse = true;
    public bool fueRecogido = false; // usado por inventario para saber si ya se tomó

    private GestorObjetivos gestorObjetivos;

    void Start()
    {
        gestorObjetivos = FindFirstObjectByType<GestorObjetivos>();

        // Si es persistente y ya fue recogido, se oculta al inicio
        if (persistente && fueRecogido)
        {
            gameObject.SetActive(false);
        }
    }

    // Intentar recoger en Enter
    void OnTriggerEnter(Collider other)
    {
        TryPickup(other, "OnTriggerEnter");
    }

    // También Intentar recoger en Stay (útil cuando el objeto aparece debajo del jugador)
    void OnTriggerStay(Collider other)
    {
        TryPickup(other, "OnTriggerStay");
    }

    private void TryPickup(Collider other, string source)
    {
        // LOG para depuración: si quieres verlo, activa en consola
        // Debug.Log($"ObjetoRecogible.TryPickup {name} via {source} puedeRecogerse={puedeRecogerse} fueRecogido={fueRecogido}");

        if (!puedeRecogerse) return;
        if (!other.CompareTag("Player")) return;

        InventarioJugador inventario = other.GetComponent<InventarioJugador>();
        if (inventario == null) return;

        if (persistente && fueRecogido) return;

        if (inventario.AgregarObjeto(gameObject))
        {
            fueRecogido = true;
            gameObject.SetActive(false);

            if (gestorObjetivos != null)
                gestorObjetivos.MarcarComoCumplido(ID);

            Debug.Log($"ObjetoRecogible: {name} recogido por player. (via {source})");
        }
    }

    // Llamable desde fuera para forzar un cooldown (si quieres)
    public void ActivarCooldown(float segundos = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(HabilitarRecoleccion(segundos));
    }

    IEnumerator HabilitarRecoleccion(float segundos)
    {
        puedeRecogerse = false;
        yield return new WaitForSeconds(segundos);
        puedeRecogerse = true;
        Debug.Log($"ObjetoRecogible: {name} puede ser recogido otra vez (cooldown {segundos}s).");
    }

    // Llamar cuando el jugador suelta el objeto: lo resetea (reactiva collider, Rigidbody, y aplica cooldown).
    public void ResetearEstadoAfterDrop(float cooldown = -1f)
    {
        if (persistente) return;

        // asegurar que el objeto está activo para que las corutinas funcionen
        if (!gameObject.activeSelf) gameObject.SetActive(true);

        // marcar que NO está recogido
        fueRecogido = false;

        // unparent para evitar estar dentro de un Content que se pueda desactivar
        transform.SetParent(null, true);

        // collider y rigidbody OK
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        float c = (cooldown > 0f) ? cooldown : pickupCooldownOnDrop;

        // bloquear recolección por un tiempo breve y luego permitirla
        StopAllCoroutines();
        StartCoroutine(ReactivarDespuesDeCooldown(c));
    }

    private IEnumerator ReactivarDespuesDeCooldown(float segundos)
    {
        puedeRecogerse = false;
        yield return new WaitForSeconds(segundos);
        puedeRecogerse = true;
        Debug.Log($"ObjetoRecogible: {name} reactivado tras cooldown {segundos}s");
    }
}