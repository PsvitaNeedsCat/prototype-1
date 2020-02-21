using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    // Refs to objects
    [SerializeField] GameObject[] playerCameras;
    [SerializeField] GameObject[] playerCanvas;
    [SerializeField] GameObject[] players;

    // Height for charge bar to snap to if in split screen
    private const float chargeBarHeight = 300.0f;
    private const float chargeBarScale = 0.5f;
    // Viewport rect Y value if in split screen
    private const float rectYVal = 0.5f;
    private const float player2LapsCounterHeight = -653.0f;

    private void Awake()
    {
        
    }

    public void SplitScreens()
    {
        // Get player count. Default to 1
        uint playerCount = 1;
        if (GameObject.FindObjectOfType<DontDestroyScript>())
        {
            playerCount = GameObject.FindObjectOfType<DontDestroyScript>().playerCount;
        }

        // Check for number of players
        if (playerCount == 1) return;

        // 2 Players
        if (playerCount == 2)
        {
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
                if (playerCanvas[0].transform.GetChild(i).tag == "ChargeBar")
                {
                    // Get game object
                    GameObject chargeBar = playerCanvas[0].transform.GetChild(i).gameObject;

                    // Change scale
                    Vector3 newScale = chargeBar.GetComponent<RectTransform>().localScale;
                    newScale.y = chargeBarScale;
                    // Change position
                    Vector3 newPos = chargeBar.GetComponent<RectTransform>().localPosition;
                    newPos.y = chargeBarHeight;

                    // Set scale
                    chargeBar.GetComponent<RectTransform>().localScale = newScale;
                    // Set height
                    chargeBar.GetComponent<RectTransform>().localPosition = newPos;
                }
            }
        }
    }

    public void ActivatePlayerTwo()
    {
        if (GameObject.FindObjectOfType<DontDestroyScript>().playerCount == 2)
        {
            players[1].SetActive(true);
        }
    }

    public void SetHUD(bool active)
    {
        for (int i = 0; i < playerCanvas.Length; i++)
        {
            playerCanvas[i].SetActive(active);
        }
    }
}
