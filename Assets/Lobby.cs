using FishNet;
using FishNet.Managing.Scened;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static FishNet.Component.Transforming.NetworkTransform;

public class Lobby : NetworkBehaviour
{
    [SerializeField] Button LeaveGameButton;
    [SerializeField] Button StartGameButton;

    // list of clown names
    // must randomly pick a name and assign it to a character
    // must remove that clown name from potentials once it is picked
    // when a player disconnects, clownnames must be reset so that 

    public override void OnStartClient()
    {
        base.OnStartClient();

        LeaveGameButton.onClick.AddListener(() => ConnectionManager.instance.StopConnection());

        if (IsHost)
        {
            StartGameButton.gameObject.SetActive(true);
            StartGameButton.onClick.AddListener(() =>
            {
                SwitchScenes();
            });
        }
    }

    void SwitchScenes()
    {
        SceneLookupData gameSceneLookupData = new SceneLookupData("NetworkedGame");
        SceneLookupData lobbySceneLookupData = new SceneLookupData("Lobby");

        SceneLoadData gameSceneLoadData = new SceneLoadData(gameSceneLookupData);
        SceneUnloadData lobbySceneUnoadData = new SceneUnloadData(lobbySceneLookupData);

        SceneManager.UnloadGlobalScenes(lobbySceneUnoadData);
        SceneManager.LoadGlobalScenes(gameSceneLoadData);
    }

    private void OnDestroy()
    {
        LeaveGameButton.onClick.RemoveAllListeners();
        StartGameButton.onClick.RemoveAllListeners();
    }
}
