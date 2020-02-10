using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersJoined : MonoBehaviour
{
    public uint playerCount = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
