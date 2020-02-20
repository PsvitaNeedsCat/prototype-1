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
    [SerializeField] Leaderboard[] playerInfo;

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

        StartCoroutine(StartCountdownSound());

        for (int i = 0; i < countdownObjects.Length; i++)
        {
            countdownObjects[i].SetActive(true);
        }

        yield return new WaitForSeconds(raceCountdownDuration);

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

        SetPlayersInputControl(true);

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
        // Single player
        if (GameObject.Find("DontDestroyObj").GetComponent<DontDestroyScript>().playerCount == 1)
        {
            GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().text = "Victory!";
        }
        // Mulitplayer
        else
        {
            ActivateLeaderboard();
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

        // Get players for strokes
        Player[] players = new Player[2] { GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>(), GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>() };

        int[] scores = new int[dontDestroy.playerCount];
        // Change positions
        for (int i = 0; i < dontDestroy.playerCount; i++)
        {
            // Starts with first place
            switch (playerPositions[i])
            {
                // Player 1 is in this position
                case 1:
                    {
                        scores[0] = (int)(((i + 1) * 5) + players[0].strokes);
                        break;
                    }
                // Player 2 is in this position
                case 2:
                    {
                        scores[1] = (int)(((i + 1) * 5) + players[1].strokes);
                        break;
                    }

                default:
                    break;
            }
        }

        int[] newPositions = playerPositions;

        // Player 1 has higher score
        if (scores[0] < scores[1]) { newPositions[0] = 1; newPositions[1] = 2; }
        else if (scores[1] < scores[0]) { newPositions[0] = 2; newPositions[1] = 1; }

        // Check each position
        for (int place = 0; place < dontDestroy.playerCount; place++)
        {
            int modifier = place * 2;

            switch (newPositions[place])
            {
                case 1:
                    {
                        playerInfo[0 + modifier].gameObject.SetActive(true);
                        break;
                    }
                case 2:
                    {
                        playerInfo[1 + modifier].gameObject.SetActive(true);
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
