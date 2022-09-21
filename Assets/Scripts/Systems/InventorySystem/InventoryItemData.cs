using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Consumable = 0,
    Durable = 1,
    IndependentDurable = 2,
}



[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
    public ObjectType type;
    public AudioClip pickupSound;
}
