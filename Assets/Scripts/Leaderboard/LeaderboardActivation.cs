using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardActivation : MonoBehaviour
{
    [SerializeField] GameObject[] playerObjects;

    public void Activate(int place, int player)
    {
        player -= 1;
        place -= 1;

        int modifier = place * 2;
        playerObjects[player + modifier].SetActive(true);
    }
}
