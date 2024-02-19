using FishNet.Object;
using FishNet.Object.Synchronizing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : NetworkBehaviour
{
    [NonSerialized]
    [SyncVar(OnChange = nameof(OnPlayerNameChange))]
    private string _playerName;

    VerticalLayoutGroup _playerList;

    public override void OnStartClient()
    {
        base.OnStartClient();

        _playerList = GameObject.Find("PlayerList").GetComponent<VerticalLayoutGroup>();
        transform.SetParent(_playerList.transform);
    }

    public void SetPlayerName(string name)
    {
        _playerName = name;
    }

    void OnPlayerNameChange(string oldValue, string newValue, bool asServer)
    {
        GetComponentInChildren<TMP_Text>().text = _playerName;
    }
}
