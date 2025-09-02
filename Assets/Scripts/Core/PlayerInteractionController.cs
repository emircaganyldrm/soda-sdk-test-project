using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{


    //---------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
            interactable.InteractionAction();
    }
}
