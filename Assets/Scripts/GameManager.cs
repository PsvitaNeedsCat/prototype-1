using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

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

    public GameObject[] countdownObjects;
    public AudioSource countdownSource;
    public GameObject gameMusic;

    [HideInInspector] public int numCheckpoints;
    public GameState gameState = GameState.preRace;
    public float preRaceDuration = 5.0f;
    public float raceCountdownDuration = 3.0f;
    public float postRaceDuration = 5.0f;
    [HideInInspector] public bool raceComplete = false;
    [HideInInspector] public int winner = 1;
    [HideInInspector] public int playersFinished = 0;
    [SerializeField] GameObject leaderboard;

    private List<Player> players = new List<Player>();
    private DontDestroyScript dontDestroy;
    public int[] playerPositions; // 1st place is first in array

    private void Awake()
    {
        dontDestroy = GameObject.FindObjectOfType<DontDestroyScript>();

        // Fill player positions array
        playerPositions = new int[dontDestroy.playerCount];
        for (int i = 0; i < playerPositions.Length; i++) playerPositions[i] = 0;

        if (_instance != null && _instance != this) { Destroy(this.gameObject); }
        else { _instance = this; }

        numCheckpoints = FindObjectsOfType<LapCheckpoint>().Length;

    }

    private void Start()
    {
        FindObjectOfType<SplitScreenManager>().ActivatePlayerTwo();

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

    private void SetPlayersBoostAllowed(bool boostAllowed)
    {
        foreach (Player player in players)
        {
            player.boostingAllowed = boostAllowed;
        }
    }

    private IEnumerator StartPreRace()
    {
        gameState = GameState.preRace;

        yield return new WaitForSeconds(preRaceDuration);

        SetPlayersInputControl(true);

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        gameState = GameState.countdown;

        FindObjectOfType<SplitScreenManager>().SplitScreens();

        StartCoroutine(StartCountdownSound());

        for (int i = 0; i < countdownObjects.Length; i++)
        {
            countdownObjects[i].SetActive(true);
        }

        yield return new WaitForSeconds(raceCountdownDuration);

        // Start timing
        foreach (Player _player in players)
        {
            _player.StartTiming();
        }

        StartCoroutine(StartRace());
    }

    private IEnumerator StartCountdownSound()
    {
        yield return new WaitForSeconds(0.5f);
        countdownSource.Play();
    }

    private IEnumerator StartRace()
    {
        gameState = GameState.inRace;

        SetPlayersBoostAllowed(true);

        gameMusic.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < countdownObjects.Length; i++)
        {
            countdownObjects[i].SetActive(false);
        }

        while (raceComplete == false)
        {
            yield return null;
        }

        StartCoroutine(EndRace());
    }

    private IEnumerator EndRace()
    {
        ActivateLeaderboard();
        GameObject.FindObjectOfType<SplitScreenManager>().SetHUD(false);

        gameState = GameState.postRace;

        SetPlayersInputControl(false);

        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        while (!kb.escapeKey.wasPressedThisFrame)
        {
            yield return null;
        }

        GameObject.FindObjectOfType<DontDestroyScript>().Reset();
        SceneManager.LoadScene("MenuScreen");

        //yield return new WaitForSeconds(postRaceDuration);

        //RestartScene();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerFinished(int playerNumber)
    {
        if (playersFinished == 0) winner = playerNumber;

        playerPositions[playersFinished] = playerNumber;

        ++playersFinished;

        if (playersFinished >= dontDestroy.playerCount)
        {
            raceComplete = true;
        }
    }

    private void ActivateLeaderboard()
    {
        // Activate leaderboard object which gets the backgrounds
        leaderboard.SetActive(true);

        if (dontDestroy.playerCount == 1)
        {
            Player soloPlayer = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
            leaderboard.GetComponent<LeaderboardActivation>().Activate(1, 1);
        }
        else
        {
            // Check each position
            for (int place = 0; place < dontDestroy.playerCount; place++)
            {
                int modifier = place * 2;

                switch (playerPositions[place])
                {
                    case 1:
                        {
                            leaderboard.GetComponent<LeaderboardActivation>().Activate(place + 1, 1);
                            break;
                        }
                    case 2:
                        {
                            leaderboard.GetComponent<LeaderboardActivation>().Activate(place + 1, 2);
                            break;
                        }

                    default:
                        break;
                }
            }
        }
    }
}
