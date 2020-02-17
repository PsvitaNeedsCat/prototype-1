using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    preRace,
    countdown,
    inRace,
    postRace
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public int numLaps = 3;

    [HideInInspector] public int numCheckpoints;
    public GameState gameState = GameState.preRace;
    public float preRaceDuration = 5.0f;
    public float raceCountdownDuration = 3.0f;
    public float postRaceDuration = 5.0f;
    [HideInInspector] public bool raceComplete = false;
    [HideInInspector] public int winner = 1;
    [HideInInspector] public int[] playerStrokeCounters = new int[2] { 0, 0 };

    private List<Player> players = new List<Player>();

    private void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(this.gameObject); }
        else { _instance = this; }

        numCheckpoints = FindObjectsOfType<LapCheckpoint>().Length;

    }

    private void Start()
    {
        Player[] allPlayers = GameObject.FindObjectsOfType<Player>();
        for (int i = 0; i < allPlayers.Length; i++)
        {
            players.Add(allPlayers[i]);
        }

        SetPlayersInputControl(false);

        StartCoroutine(StartPreRace());
    }

    private void SetPlayersInputControl(bool canInput)
    {
        foreach (Player player in players)
        {
            player.SetInputControl(canInput);
        }
    }

    private IEnumerator StartPreRace()
    {
        gameState = GameState.preRace;

        yield return new WaitForSeconds(preRaceDuration);

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        gameState = GameState.countdown;

        yield return new WaitForSeconds(raceCountdownDuration);

        StartCoroutine(StartRace());
    }

    private IEnumerator StartRace()
    {
        gameState = GameState.inRace;

        SetPlayersInputControl(true);

        while (raceComplete == false)
        {
            yield return null;
        }

        StartCoroutine(EndRace());
    }

    private IEnumerator EndRace()
    {
        // Single player
        if (GameObject.Find("DontDestroyObj").GetComponent<DontDestroyScript>().playerCount == 1)
        {
            GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().text = "Victory!";
        }
        // Mulitplayer
        else
        {
            GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().text = "Player " + winner + " won!";
        }

        gameState = GameState.postRace;

        SetPlayersInputControl(false);

        yield return new WaitForSeconds(postRaceDuration);

        RestartScene();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
