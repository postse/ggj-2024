using FishNet;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerMenu : NetworkBehaviour
{
    // store references to all the buttons
    // add an onclick listener to each button
    // when the buttons are clicked, add a menu item to the queue which is associated with that button
    // go to the last menu item in the queue and render it
    // when the back button is clicked, pop the last menu item from the queue, and then go to the last menu item

    // each menu item should have a reference to the things it wants to render
    // buttons, and the menu items that they will have,
    // 
    [SerializeField] Button hostGameButton;
    [SerializeField] Button joinGameButton;


    void Start()
    {
        hostGameButton.onClick.AddListener(() =>
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        });

        joinGameButton.onClick.AddListener(() =>
        {
            InstanceFinder.ClientManager.StartConnection();
        });
    }
}
