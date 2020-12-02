using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    public uint playerCount = 1;
    public uint maxNumPlayers = 2;

    public HornScript.Sounds[] horns = new HornScript.Sounds[2]
    {
        HornScript.Sounds.SQUEAK,
        HornScript.Sounds.SQUEAK,
    };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Reset()
    {
        playerCount = 1;
        horns[0] = HornScript.Sounds.SQUEAK;
        horns[1] = HornScript.Sounds.SQUEAK;
    }
}
