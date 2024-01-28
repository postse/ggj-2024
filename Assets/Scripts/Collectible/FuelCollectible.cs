using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCollectible : Collectible
{
    [SerializeField]
    private float fuelAmt;

    override public void PickUp(GameObject player) {
        player.GetComponent<CarController>().AddFuel(fuelAmt);
        base.PickUp(player);
    }
}
