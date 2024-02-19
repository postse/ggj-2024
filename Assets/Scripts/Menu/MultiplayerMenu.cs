using FishNet;
using FishNet.Connection;
using FishNet.Managing.Scened;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerMenu : NetworkBehaviour
{
    [SerializeField] Button hostGameButton;
    [SerializeField] Button joinGameButton;

    private void Start()
    {
        hostGameButton.onClick.AddListener(() => ConnectionManager.instance.StartConnection(true));
        joinGameButton.onClick.AddListener(() => ConnectionManager.instance.StartConnection(false));
    }

    private void OnDestroy()
    {
        hostGameButton.onClick.RemoveAllListeners();
        joinGameButton.onClick.RemoveAllListeners();
    }
}
