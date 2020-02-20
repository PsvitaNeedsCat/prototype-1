using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    public uint playerCount = 1;
    public uint maxNumPlayers = 2;

    public HornScript.Sounds p1SelectedHorn = HornScript.Sounds.SQUEAK;
    public HornScript.Sounds p2SelectedHorn = HornScript.Sounds.SQUEAK;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Reset()
    {
        playerCount = 1;
        p1SelectedHorn = HornScript.Sounds.SQUEAK;
        p2SelectedHorn = HornScript.Sounds.SQUEAK;
    }
}
