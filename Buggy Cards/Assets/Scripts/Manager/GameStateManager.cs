using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private PlayfieldManager playfieldManager;
    
    private Deck deck;
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
        playfieldManager = GetComponent<PlayfieldManager>();
        deck = new Deck(cardDB, playfieldManager);
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
        deck.GenerateGameDeck(deckSize);
        gameDeck.GenerateGameDeck(gameDeckSize);
        deck.DrawNextXCards(startHandSize);
        gameDeck.ActivateNextCard();
    }

    private void StartNewRound()
    {
        OnInitiateDiscardPhase?.Invoke();
        gameDeck.DiscardActiveCard();
        gameDeck.ActivateNextCard();
        deck.DrawNextXCards(1);
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
