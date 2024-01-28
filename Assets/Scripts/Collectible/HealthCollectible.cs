using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : Collectible
{
    [SerializeField]
    private float healAmt = 25;

    override public void PickUp(GameObject player) {
        player.GetComponent<CarController>().Heal(healAmt);
        base.PickUp(player);
    }
}
