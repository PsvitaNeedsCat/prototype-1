using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersJoined : MonoBehaviour
{
    public uint playerCount = 1;
    public uint maxNumPlayers = 2;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
