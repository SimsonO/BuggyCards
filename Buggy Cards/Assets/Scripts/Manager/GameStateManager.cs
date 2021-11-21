using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{    
    private PlayerDeck playerDeck;
    [SerializeField]
    private int deckSize = 6;
    private GameDeck gameDeck;
    [SerializeField]
    private int gameDeckSize = 4;

    [SerializeField]
    private int startHandSize = 3;

    [SerializeField]
    private CardDatabase cardDB;

    //Event that will be broadcast whenever Cards from last Round should be discarded
    public delegate void InitiateDiscardPhase();
    public static event InitiateDiscardPhase OnInitiateDiscardPhase;

    private void Awake()
    {   
        playerDeck = FindObjectOfType<PlayerDeck>();
        gameDeck = FindObjectOfType<GameDeck>();
    }

    private void Start()
    {
        ResultChecker.OnActiveCardSurpassed += StartNewRound;
        ResultChecker.OnGameLost += InitiateGameLoss;
        ResultChecker.OnGameWon += InitiateGameWin;
        StartTheGame();
    }

    private void StartTheGame()
    {
        playerDeck.GeneratePlayerDeck(deckSize);
        gameDeck.GenerateGameDeck(gameDeckSize);
        playerDeck.DrawNextXCards(startHandSize);
        gameDeck.ActivateNextCard();
    }

    private void StartNewRound()
    {
        OnInitiateDiscardPhase?.Invoke();
        gameDeck.DiscardActiveCard();
        gameDeck.ActivateNextCard();
        playerDeck.DrawNextXCards(1);
    }
    private void InitiateGameLoss()
    {
        Debug.Log("you lost the game");
        //TODO:
        //show loose screen
        //wrap up game
    }
    private void InitiateGameWin()
    {
        Debug.Log("You won the Game");
        //TODO:
        //show win screen
        //wrap up game
    }

    private void OnDestroy()
    {
        ResultChecker.OnActiveCardSurpassed -= StartNewRound;
        ResultChecker.OnGameLost -= InitiateGameLoss;
        ResultChecker.OnGameWon -= InitiateGameWin;
    }
}
