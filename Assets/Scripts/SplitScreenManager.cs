using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class SplitScreenManager : MonoBehaviour
{
    // Refs to objects
    [SerializeField] GameObject[] playerCameras;
    [SerializeField] GameObject[] playerCanvas;
    [SerializeField] GameObject[] players;

    // Height for charge bar to snap to if in split screen
    private const float chargeBarHeight = 612.0f; //84.0f;
    // Viewport rect Y value if in split screen
    private const float rectYVal = 0.5f;
    private const float player2LapsCounterHeight = -653.0f;

    private void Awake()
    {
        // Get player count. Default to 1
        uint playerCount = 1;
        if (GameObject.Find("DontDestroyObj"))
        {
            playerCount = GameObject.Find("DontDestroyObj").GetComponent<DontDestroyScript>().playerCount;
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

            // Move canvas stuff
            for (int index = 0; index < playerCanvas[0].transform.childCount; index++)
            {
                switch (playerCanvas[0].transform.GetChild(index).GetComponent<CanvasItem>().itemTag)
                {
                    case CanvasItem.CanvasItemTag.CHARGEBAR:
                        {
                            playerCanvas[0].transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, chargeBarHeight, 0.0f);
                            break;
                        }

                    default:
                        break;
                }
            }
        }
    }

    public void AddStroke(int playerNumber)
    {
        playerNumber -= 1;

        GameManager.Instance.playerStrokeCounters[playerNumber] += 1;
        
        for (int i = 0; i < playerCanvas[playerNumber].transform.childCount; i++)
        {
            // Get strokes counter
            if (playerCanvas[playerNumber].transform.GetChild(i).GetComponent<CanvasItem>().itemTag == CanvasItem.CanvasItemTag.STROKES)
            {
                playerCanvas[playerNumber].transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = "Strokes: " + GameManager.Instance.playerStrokeCounters[playerNumber];
            }
        }
    }
}
