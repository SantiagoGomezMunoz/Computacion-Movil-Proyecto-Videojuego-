using UnityEngine;

public interface IInteractable
{
    void Interact();
    string GetActionName();
    bool IsAvailable(); // agregado para que el InteractionManager sepa si puede interactuar
}