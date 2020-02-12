using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    PlayersJoined playerCount;

    private void Awake()
    {
        playerCount = GameObject.Find("PlayersJoined").GetComponent<PlayersJoined>();
    }

    public void Play()
    {
        // Load game scene
        SceneManager.LoadScene("TestDrive");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangePlayerCount()
    {
        playerCount.playerCount = (playerCount.playerCount >= playerCount.maxNumPlayers) ? 1 : playerCount.playerCount + 1;

        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Players: " + playerCount.playerCount;
    }
}
