using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] int playerNumber;

    [SerializeField] TextMeshProUGUI strokes;
    [SerializeField] TextMeshProUGUI timer;

    private void Awake()
    {
        uint playerStrokes = 0;
        float timeTaken = 0;

        switch (playerNumber)
        {
            case 1:
                {
                    playerStrokes = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>().strokes;
                    timeTaken = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>().timer;
                    break;
                }
            
            case 2:
                {
                    playerStrokes = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>().strokes;
                    timeTaken = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>().timer;
                    break;
                }

            default:
                break;
        }

        strokes.text = "Strokes: " + playerStrokes.ToString();
        int minutes = Mathf.FloorToInt(timeTaken / 60);
        int seconds = Mathf.FloorToInt((timeTaken % 60.0f));
        string secondsTxt = seconds.ToString();

        if (seconds < 10) secondsTxt = "0" + secondsTxt;
        timer.text = "Time: " + minutes + ":" + secondsTxt;
    }
}
