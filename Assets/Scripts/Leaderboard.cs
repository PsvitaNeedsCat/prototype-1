using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] int playerNumber;

    [SerializeField] TextMeshProUGUI strokes;
    [SerializeField] TextMeshProUGUI totalScore;

    private void Awake()
    {
        uint playerStrokes = 0;

        switch (playerNumber)
        {
            case 1:
                {
                    playerStrokes = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>().strokes;
                    break;
                }
            
            case 2:
                {
                    playerStrokes = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>().strokes;
                    break;
                }

            default:
                break;
        }

        if (GameManager.Instance.winner == playerNumber)
        {
            totalScore.text = ((1 * 5) + playerStrokes).ToString();
        }
        else
        {
            totalScore.text = ((2 * 5) + playerStrokes).ToString();
        }

        strokes.text = "Strokes: " + playerStrokes.ToString();
    }
}
