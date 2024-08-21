using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensorTrigger : MonoBehaviour
{
   //[SerializeField] private Animator door;
   //public InventoryItemData referenceItem; //teste para ver se abre de volta
   //private string _estado;
   
   /*public void ChangeDoorState(string novoest) //definir método de mudança de estado em outro script NÃO influencia
                                                 //em estados manipulados externamente  
   {
      if (_estado == novoest) return; //impede uma animação de interromper a si mesma
        
      door.Play(novoest, -1, 0);

      _estado = novoest;
   }*/
   
   public void OnTriggerExit(Collider other)
   {
      GameObject sensor = GameObject.Find("AutoDoorCloseTrigger");
      HubDoorInterectee.ChangeDoorState("door-close"); //deve-se chamar o método utilizado previamente para animar a porta;
                                                               //supondo que chaves apenas abram portas,
                                                               //as mesmas devem ser fechadas dentro do escopo de chaves,
                                                               //para impedir conflito de animação.
      //InventorySystem.Instance.Add(referenceItem); //chave de teste para abrir novamente
      Destroy(sensor, 1);
   }
}
