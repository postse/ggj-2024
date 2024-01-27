using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollectible : Collectible
{
    [SerializeField]
    private int weaponType;

    [SerializeField]
    private int amt;

    public override void PickUp(GameObject player) {
        player.GetComponent<InventoryManager>().AddItem(weaponType, amt);
        base.PickUp(player);
    }
}
