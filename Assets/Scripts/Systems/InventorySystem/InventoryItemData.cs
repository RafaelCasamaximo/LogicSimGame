using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Consumable = 0,
    Durable = 1,
    IndependentDurable = 2,
}


/// <summary>
/// Item que o jogador pode ter
/// Possui as categorias Consumable (consumível que some após interagir com algo), Durable (que não é consumido após interagir com algo) e IndependentDurable (Que não precisa de um interactee para funcionar e não é consumível)
/// </summary>
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
