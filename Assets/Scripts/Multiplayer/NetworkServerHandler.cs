using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Managing;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Transporting;

public class NetworkServerHandler : MonoBehaviour
{
    [SerializeField] NetworkObject playerCardPrefab;

    private NetworkManager _networkManager;

    private void Start()
    {
        _networkManager = InstanceFinder.NetworkManager;

        if (_networkManager == null)
        {
            Debug.LogWarning($"NetworkServerHandler on {gameObject.name} because NetworkManager wasn't found on this object or within parent objects");
            return;
        }

        _networkManager.SceneManager.OnClientLoadedStartScenes += SceneManager_OnClientLoadedStartScenes;
        _networkManager.ServerManager.OnRemoteConnectionState += ServerManager_OnRemoteConnectionState;
    }

    // SERVER
    private void SceneManager_OnClientLoadedStartScenes(NetworkConnection conn, bool asServer)
    {
        // we call 'SpawnPlayerCard' here because the 'OnClientLoadedStartScenes' event handler takes a connection, and an important boolean
        // the connection can provide us with 'CustomData' which the user may have provided after authentication, but before loading a scene
        // we also do not want clients to spawn networked game objects, as only objects spawned by the server are automatically networked

        if (!asServer) return;

        Debug.Log(ClownNames.Count);
        
        SpawnPlayerCard(conn);
    }

    private void ServerManager_OnRemoteConnectionState(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        Debug.Log("REMOTE CONNECTION STATE CHANGE");
        Debug.Log(args.ConnectionState);

        if (args.ConnectionState != RemoteConnectionState.Stopped) return;

        // if host is disconnecting, then the server will be disconnected shortly thereafter
        // host must restore all clown names so that if they start a new game they have access to the full list


        if (conn.IsHost)
        {
            ClownNames.RestoreAllClownNames();
            return;
        }

        // if a remote client leaves, then they can just turn in their name and server can resuse it for a new player.
        ClownNames.RestoreClownName(((PlayerData)conn.CustomData).playerName);
    }

    private void SpawnPlayerCard(NetworkConnection conn)
    {
        // game objects must be instantiated before they can be spawned
        NetworkObject nob = _networkManager.GetPooledInstantiated(playerCardPrefab, true);

        // here we can set custom data to the game object before it gets spawned
        string name = ClownNames.PickClownName();
        conn.CustomData = new PlayerData() { playerName = name };

        nob.GetComponent<PlayerCard>().SetPlayerName(name);

        // all networked objects need to be spawned on the server, otherwise they will not sync to each client
        _networkManager.ServerManager.Spawn(nob, conn);
        _networkManager.SceneManager.AddOwnerToDefaultScene(nob);
    }

    private void OnDestroy()
    {
        if (_networkManager == null) return;
        _networkManager.SceneManager.OnClientLoadedStartScenes -= SceneManager_OnClientLoadedStartScenes;
    } 
}
