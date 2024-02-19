using FishNet;
using FishNet.Broadcast;
using FishNet.Transporting.Tugboat;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct PlayerData : IBroadcast
{
    public string playerName;
}

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager instance;

    private Tugboat _transport;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        if (TryGetComponent(out Tugboat tugboat)) _transport = tugboat;
        else Debug.LogError("Couldn't get tugboat", this);
    }

    public void StartConnection(bool asServer)
    {
        if (asServer) _transport.StartConnection(true);
        _transport.StartConnection(false);
    }

    public void StopConnection()
    {
        _transport.StopConnection(false);
        if (InstanceFinder.IsServer)
        {
            ClownNames.RestoreAllClownNames();
            _transport.StopConnection(true);
        }
    }
}
