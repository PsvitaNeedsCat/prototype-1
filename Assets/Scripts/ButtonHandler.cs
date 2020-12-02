using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    DontDestroyScript dontDestroy;

    EventSystem m_eventSystem;
    [SerializeField] private GameObject m_playButton;

    [SerializeField] GameObject[] playerHornGroups;

    private void Awake()
    {
        dontDestroy = FindObjectOfType<DontDestroyScript>();
        m_eventSystem = FindObjectOfType<EventSystem>();
    }

    public void Play()
    {
        // Load game scene
        SceneManager.LoadScene("TimScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangePlayerCount()
    {
        // Toggle player count integer
        dontDestroy.playerCount = (dontDestroy.playerCount >= dontDestroy.maxNumPlayers) ? 1 : dontDestroy.playerCount + 1;

        // Turn player 2 horn button on/off
        if (dontDestroy.playerCount >= 2)
        {
            playerHornGroups[1].SetActive(true);
        }
        else
        {
            playerHornGroups[1].SetActive(false);
        }

        m_eventSystem.SetSelectedGameObject(m_playButton);
    }

    public void ChangePlayerHorn(int _playerNum)
    {
        int index = _playerNum - 1;

        // Go to next horn sound
        dontDestroy.horns[index] = (HornScript.Sounds)((int)dontDestroy.horns[index] + 1);

        // If at the end of the list, reset
        if ((int)dontDestroy.horns[index] >= (int)HornScript.Sounds.COUNT) dontDestroy.horns[index] = (HornScript.Sounds)0;

        // Change button's text
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dontDestroy.horns[index].ToString();

        // Play the sound
        GetComponent<HornScript>().NextHorn();
        GetComponent<HornScript>().PlayHorn();
    }
}
