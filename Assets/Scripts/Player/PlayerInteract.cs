using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Responsável por tudo que o jogador pode interagir, seja seja com um item que pode ser coletado do ambiente (ItemObject) ou um trigger de dialogo ()DialogueTrigger
/// Aplicar nos objetos que o jogador pode interagir mas não são relacionados com o consumo de itens, e sim a acquisição de novos. 
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    public string interactableTag;
    public string itemTag;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandlePlayerMovementInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (!hit.transform) return;
            switch (hit.collider.tag)
            {
                case "Item":
                    ItemObject itemObject = hit.collider.gameObject.GetComponent<ItemObject>();
                    itemObject.HandlePickupItem();
                    break;
                case "Dialogue":
                    DialogueTrigger dialogueTrigger = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    dialogueTrigger.InteractTriggerDialogue();
                    break;
            }
        }
    }
}
