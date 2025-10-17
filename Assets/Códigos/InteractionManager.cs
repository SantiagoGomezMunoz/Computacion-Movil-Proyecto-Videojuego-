using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;

    [Header("Referencias UI")]
    public Button actionButton;
    public TMP_Text actionButtonText;

    [Header("Referencias del jugador")]
    public GameObject jugador; // Solo referencia al GameObject del jugador, no a un script específico

    private IInteractable currentInteractable;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (actionButton != null)
            actionButton.onClick.AddListener(OnActionButtonPressed);
    }

    void OnActionButtonPressed()
    {
        // 🔹 Si hay un NPC u objeto interactuable cerca
        if (currentInteractable != null && currentInteractable.IsAvailable())
        {
            currentInteractable.Interact();
        }
        else
        {
            // 🔹 Si no hay NPC cerca, ejecuta la acción normal del jugador
            EjecutarAccionNormal();
        }
    }

    private void EjecutarAccionNormal()
    {
        // Aquí decidimos qué hacer cuando el jugador no está interactuando con nadie.
        // Llamaremos un método genérico en alguno de los scripts del jugador, si existe.

        // Ejemplo: buscar si el jugador tiene un script con método "RealizarAccionPrincipal"
        var scripts = jugador.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            var metodo = script.GetType().GetMethod("RealizarAccionPrincipal");
            if (metodo != null)
            {
                metodo.Invoke(script, null);
                return;
            }
        }

        Debug.LogWarning("⚠️ Ningún script del jugador tiene RealizarAccionPrincipal().");
    }

    public void SetCurrent(IInteractable interactable)
    {
        currentInteractable = interactable;
        ActualizarTextoAccion(interactable.GetActionName());
    }

    public void ClearCurrent(IInteractable interactable)
    {
        if (currentInteractable == interactable)
        {
            currentInteractable = null;
            ActualizarTextoAccion("Acción");
        }
    }

    public void ActualizarTextoAccion(string texto)
    {
        if (actionButtonText != null)
            actionButtonText.text = texto;
    }
}