using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    DontDestroyScript dontDestroy;

    [SerializeField] GameObject[] playerHornGroups;

    private void Awake()
    {
        dontDestroy = GameObject.Find("DontDestroyObj").GetComponent<DontDestroyScript>();
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
        // Toggle player count integer
        dontDestroy.playerCount = (dontDestroy.playerCount >= dontDestroy.maxNumPlayers) ? 1 : dontDestroy.playerCount + 1;

        // Change button's text in menu
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Players: " + dontDestroy.playerCount;

        // Turn player 2 horn button on/off
        if (dontDestroy.playerCount >= 2) playerHornGroups[1].SetActive(true);
        else playerHornGroups[1].SetActive(false);
    }

    public void ChangePlayerHorn(int _playerNum)
    {
        switch (_playerNum)
        {
            case 1:
                {
                    // Go to next horn sound
                    dontDestroy.p1SelectedHorn = (HornScript.Sounds)((int)dontDestroy.p1SelectedHorn + 1);

                    // If at the end of the list, reset
                    if ((int)dontDestroy.p1SelectedHorn >= (int)HornScript.Sounds.COUNT) dontDestroy.p1SelectedHorn = (HornScript.Sounds)0;

                    // Change button's text
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dontDestroy.p1SelectedHorn.ToString();

                    // Play the sound
                    GetComponent<HornScript>().NextHorn();
                    GetComponent<HornScript>().PlayHorn();

                    break;
                }

            case 2:
                {
                    // Go to next horn sound
                    dontDestroy.p2SelectedHorn = (HornScript.Sounds)((int)dontDestroy.p2SelectedHorn + 1);

                    // If at the end of the list, reset
                    if ((int)dontDestroy.p2SelectedHorn >= (int)HornScript.Sounds.COUNT) dontDestroy.p2SelectedHorn = (HornScript.Sounds)0;

                    // Change button's text
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dontDestroy.p2SelectedHorn.ToString();

                    // Play the sound
                    GetComponent<HornScript>().NextHorn();
                    GetComponent<HornScript>().PlayHorn();

                    break;
                }

            default:
                break;
        }
    }
}
