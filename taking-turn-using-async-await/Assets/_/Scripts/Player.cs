namespace GiantCroissant.MoYraq.Game
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class Player : MonoBehaviour
    {
        private void Pick()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Debug.Log($"mousePosition screen: {mousePosition} worldMousePosition: {worldMousePosition}");
            
            var hit = Physics2D.Raycast(worldMousePosition, Vector2.zero);
            if (hit.collider is not null)
            {
                // Debug.Log($"hit collider: {hit.collider.gameObject.name}");
                var interactToPick = hit.collider.GetComponent<InteractToPick>();
                if (interactToPick is not null)
                {
                    interactToPick.Interact();
                }
            }
        }
        
        public void HandlePick(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                Pick();
            }
            else if (callbackContext.canceled)
            {
                
            }
        }
    }
    
}
