using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    // Refs to objects
    [SerializeField] GameObject[] playerCameras;
    [SerializeField] GameObject[] playerCanvas;
    [SerializeField] GameObject[] players;
    [SerializeField] GameObject lapsCounter;

    // Height for charge bar to snap to if in split screen
    private const float chargeBarHeight = 612.0f; //84.0f;
    // Viewport rect Y value if in split screen
    private const float rectYVal = 0.5f;
    private const float player2LapsCounterHeight = -653.0f;

    private void Awake()
    {
        // Get player count. Default to 1
        uint playerCount = 1;
        if (GameObject.Find("PlayersJoined"))
        {
            playerCount = GameObject.Find("PlayersJoined").GetComponent<PlayersJoined>().playerCount;
        }

        // Check for number of players
        if (playerCount == 1) return;

        // 2 Players
        if (playerCount == 2)
        {
            // Set Player 2 to active
            players[1].SetActive(true);
            // Set player 2 camera to active
            playerCameras[1].SetActive(true);
            // Set player 1 camera's viewport
            Rect currentP1Rect = playerCameras[0].GetComponent<Camera>().rect;
            playerCameras[0].GetComponent<Camera>().rect = new Rect(new Vector2(currentP1Rect.x, 0.5f), currentP1Rect.size);
            // Set player 2 canvas to active
            playerCanvas[1].SetActive(true);
            // Set player 1's charge bar height
            for (int i = 0; i < playerCanvas[0].transform.childCount; i++)
            {
                if (playerCanvas[0].transform.GetChild(i).tag != "LapsCounter")
                {
                    playerCanvas[0].transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, chargeBarHeight, 0.0f);
                }
            }

            // Move player 2 laps counter
            for (int i = 0; i < playerCanvas[1].transform.childCount; i++)
            {
                if (playerCanvas[1].transform.GetChild(i).tag == "LapsCounter")
                {
                    Vector3 curPos = playerCanvas[1].transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
                    playerCanvas[1].transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector3(curPos.x, player2LapsCounterHeight, curPos.z);
                }
            }
        }
    }
}
