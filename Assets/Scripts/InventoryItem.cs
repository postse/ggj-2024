using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private GameObject idle;

    [SerializeField]
    private GameObject projectile;

    // [SerializeField]
    // private int count;

    public GameObject GetIdle() {
        return idle;
    }

    public GameObject GetProjectile() {
        return projectile;
    }

    // public void SpendItem() {
    //     count--;
    // }

    // public int GetCount() {
    //     return count;
    // }
}
