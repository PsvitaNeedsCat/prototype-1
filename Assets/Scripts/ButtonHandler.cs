using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    PlayersJoined numPlayers;

    [SerializeField] GameObject[] playerButtons;
    [SerializeField] GameObject[] buttonText;

    private void Awake()
    {
        numPlayers = GameObject.Find("PlayersJoined").GetComponent<PlayersJoined>();
    }

    public void Play()
    {
        // Load game scene
        SceneManager.LoadScene("CarTest");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void AddPlayer()
    {
        uint playersJoined = numPlayers.playerCount;

        switch (playersJoined)
        {
            case 0:
                {
                    // Display player 2 button, then add to player count
                    if (playerButtons[1])
                    {
                        playerButtons[1].SetActive(true);
                    }

                    buttonText[0].GetComponent<TextMesh>().text = "Joined";

                    break;
                }

            case 1:
                {
                    // Add a player
                    break;
                }

            case 2:
                {
                    // Max number of players, do nothing
                    return;
                }

            default:
                break;
        }

        numPlayers.playerCount++;
    }
}
