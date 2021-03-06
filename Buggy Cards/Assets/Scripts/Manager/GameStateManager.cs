using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{    
    private PlayerDeck playerDeck;
    [SerializeField]
    private int deckSize = 6;
    [SerializeField]
    private int minDeckSum;
    private GameDeck gameDeck;
    [SerializeField]
    private int gameDeckSize = 4;
    [SerializeField]
    private int maxDeckSum;

    [SerializeField]
    private int startHandSize = 3;

    [SerializeField]
    private float timeUntilNextBug;

    [SerializeField]
    private GameObject bugPrefab;
    [SerializeField]
    private GameObject startPositionBug;
    [SerializeField]
    private GameObject endPositionBug;

    private GameObject playfield;

    private int roundCounter;

    private bool gameActive = false;


    //Event that will be broadcast whenever Cards from last Round should be discarded
    public delegate void InitiateDiscardPhase();
    public static event InitiateDiscardPhase OnInitiateDiscardPhase;

    //Event that will be broadcast whenever a Game did end
    public delegate void GameDidEnd();
    public static event GameDidEnd OnGameDidEnd;

    private void Awake()
    {   
        playerDeck = FindObjectOfType<PlayerDeck>();
        gameDeck = FindObjectOfType<GameDeck>();
        playfield = GameObject.FindGameObjectWithTag("Playfield");
    }

    private void Start()
    {
        ResultChecker.OnActiveCardSurpassed += StartNewRound;
        ResultChecker.OnGameLost += InitiateGameLoss;
        ResultChecker.OnGameWon += InitiateGameWin;
    }

    public void StartTheGame()
    {
        playerDeck.GeneratePlayerDeck(deckSize, minDeckSum);
        gameDeck.GenerateGameDeck(gameDeckSize, maxDeckSum);
        playerDeck.DrawNextXCards(startHandSize);
        gameDeck.ActivateNextCard();
        gameActive = true;
        roundCounter = 0;
    }

    private void StartNewRound()
    {
        OnInitiateDiscardPhase?.Invoke();
        gameDeck.ActivateNextCard();
        playerDeck.DrawNextXCards(1);
        roundCounter++;
        if(roundCounter == 1)
        {
            StartCoroutine(LetTheBugsOut());
        }
    }     

    IEnumerator LetTheBugsOut()
    {
        while (gameActive)
        {
            LetOneBugOut();
            yield return new WaitForSeconds(timeUntilNextBug);
        }
    }

    private void LetOneBugOut()
    {
        GameObject bug = Instantiate(bugPrefab, startPositionBug.transform.position, Quaternion.Euler(0,0,180));
        bug.transform.SetParent(playfield.transform, false);
        BugController bugController = bug.GetComponent<BugController>();
        bugController.SetDeck(playerDeck);
        bugController.SetLeavePosition(endPositionBug.transform.position);
        bugController.MoveToDeck();
    }
    private void InitiateGameLoss()
    {
        gameActive = false;
        Debug.Log("you lost the game");
        StopAllCoroutines();
        OnGameDidEnd?.Invoke();

        //TODO:
        //show loose screen
        //wrap up game
    }
    private void InitiateGameWin()
    {
        gameActive = false;
        Debug.Log("You won the Game");
        StopAllCoroutines();
        OnGameDidEnd?.Invoke();
        //TODO:
        //show win screen
        //wrap up game
    }

    public void LoadPlayfield()
    {
        SceneManager.LoadScene("Playfield");
    }

    private void OnDestroy()
    {
        ResultChecker.OnActiveCardSurpassed -= StartNewRound;
        ResultChecker.OnGameLost -= InitiateGameLoss;
        ResultChecker.OnGameWon -= InitiateGameWin;
    }
}
