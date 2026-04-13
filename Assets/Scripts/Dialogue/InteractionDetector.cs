using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
 /* InteractionDetector Script - To be placed on Player
 * Description: Script that handles Interaction Detection, meaning if the player's collider intersects with another, this will trigger and allow an interaction to happen (if player presses E) 
 * April 9th SHREYA (sr3745):
 * (When script was created)
 */

    private IInteractable interactableInRange = null;
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) interactableInRange?.Interact();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
           
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
           
        }
    }
}

