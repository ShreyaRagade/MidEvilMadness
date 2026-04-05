using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;

    private void Start()
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) interactableInRange?.Interact();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
