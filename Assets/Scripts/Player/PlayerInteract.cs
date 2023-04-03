using System;
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
    private bool shouldCheck;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + (transform.forward / 10), transform.forward, Color.magenta);
    }

    private void FixedUpdate()
    {
        if (shouldCheck)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, 3f);
            
            foreach (RaycastHit hit in hits)
            {
                if (!hit.transform) return;
                Debug.Log(hit);
                switch (hit.collider.tag)
                {
                    case "Item":
                        Debug.Log("É um item");
                        ItemObject itemObject = hit.collider.gameObject.GetComponent<ItemObject>();
                        itemObject.HandlePickupItem();
                        return;
                    case "Dialogue":
                        Debug.Log("É um dialogo");
                        DialogueTrigger dialogueTrigger = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                        dialogueTrigger.InteractTriggerDialogue();
                        break;
                    default:
                        Debug.Log("Outro");
                        break;

                }
            }
            
            shouldCheck = false;
        }
    }

    public void HandlePlayerMovementInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        shouldCheck = true;
    }

}
