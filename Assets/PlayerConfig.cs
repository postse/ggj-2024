using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{

    public int playerCount;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
