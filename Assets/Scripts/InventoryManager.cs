using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField]
    private InventoryItem[] items;

    // private List<InventoryItem> items;

    [SerializeField]
    private int[] counts;


    [SerializeField]
    private int active = 0;

    [SerializeField]
    private Launcher launcher;

    // Start is called before the first frame update
    void Start()
    {
        // foreach (InventoryItem prefab in itemPrefabs) {
        //     InventoryItem item = Instantiate(prefab);
        //     items.Add(item);
        // }
        LoadProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CycleProjectile")) {
            CycleProjectile();
        }
    }

    private void CycleProjectile() {
        // active++;
        // if (active >= items.Length) active = 0;
        active = active == items.Length - 1 ? 0 : active + 1;
        while (counts[active] == 0) {
            active = active == items.Length - 1 ? 0 : active + 1;
        }

        LoadProjectile();
    }

    // Load Projectile in Launcher
    private void LoadProjectile() {
        launcher.SetIdleSprite(items[active].GetIdle());
        launcher.SetProjectilePrefab(items[active].GetProjectile());
    }

    // Manage inventory and load another projectile if there are more
    public void SpendItem() {
        if (counts[active] == 0) return;

        if (counts[active] > 0) {
            // Count is limited
            counts[active]--;

            if (counts[active] > 0) {
                // Are there more?
                LoadProjectile();
            } else {
                // No more :(
                launcher.SetProjectilePrefab(null);
            }

        } else {
            // Count is negative, treat as infinity
            LoadProjectile();
        }
    }
}