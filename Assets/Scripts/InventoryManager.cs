using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public enum InventoryType {
        WaterBalloon = 0,
        BowlingPin = 1,
        JackInTheBox = 2,
    }

    [SerializeField]
    private InventoryItem[] items;

    // private List<InventoryItem> items;

    [SerializeField]
    private int[] counts;


    [SerializeField]
    private int active = 0;

    [SerializeField]
    private Launcher launcher;
    private InventoryPanel inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        LoadProjectile();
        inventoryPanel = FindObjectOfType<InventoryPanel>();
    }

    public void CycleProjectile() {
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

    public void AddItem(InventoryType inventoryType, int amt) {
        counts[(int)inventoryType] += amt;
        SetInventoryPanel();
    }

    // Manage inventory and load another projectile if there are more
    public void SpendItem() {
        if (counts[active] == 0) return;

        if (counts[active] > 0) {
            // Count is limited
            counts[active]--;

            if (counts[active] == 0) {
                // No more. Go back to default
                active = 0;
            }
            LoadProjectile();

        } else {
            // Count is negative, treat as infinity
            LoadProjectile();
        }

        SetInventoryPanel();
    }

    public int GetItemCount(InventoryType item) {
        return counts[(int)item];
    }

    public InventoryType GetActiveItem() {
        return (InventoryType)active;
    }

    public void SetInventoryPanel()
    {
        inventoryPanel.SetPlayerNameText(GetComponent<CarController>().name);
        inventoryPanel.SetJackInTheBoxText(GetItemCount(InventoryType.JackInTheBox));
        inventoryPanel.SetBowlingPinsText(GetItemCount(InventoryType.BowlingPin));
    }
}
