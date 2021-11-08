using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayfieldManager : MonoBehaviour
{
    private ResultChecker resultChecker;
    private GameStateManager gameStateManager;
    [SerializeField]
    private GameObject playerHand;
    [SerializeField]
    private GameObject gameDeck;
    [SerializeField]
    private GameObject playArea;
    [SerializeField]
    private GameObject activeGameCardArea;

    //remove
    [SerializeField]
    private TextMeshProUGUI activeCardValueDisplay;

    private void Awake()
    {
        resultChecker = new ResultChecker();
        gameStateManager = new GameStateManager();
    }
    private void Start()
    {
        DragAndDropCards.OnNewCardInPlayArea += HandleNewCardInPlayArea;
        ResultChecker.OnActiveCardSurpassed += StartNewRound;
        gameDeck.GetComponent<GameDeck>().GenerateGameDeck();
        StartFirstRound();//Move to gameStateManger;
    }    

    private void HandleNewCardInPlayArea()
    {
        UpdateSumOfCardValue();
        resultChecker.CheckForEndOfRound();
    }

    private void UpdateSumOfCardValue()
    {
        int valueInPlayArea = 0;
        PlayerCardDisplay[] cardsInPlayArea = playArea.GetComponentsInChildren<PlayerCardDisplay>();
        foreach (PlayerCardDisplay card in cardsInPlayArea)
        {
            valueInPlayArea += card.GetCardValue();
        }
        resultChecker.UpdateValueInPlayArea(valueInPlayArea);
        Debug.Log("sum in PlayArea: " + valueInPlayArea);
    } 
      
    private void StartNewRound()
    {
        Debug.Log("Start new round");
        DiscardActiveGameCard();
        DiscardPlayArea();
        GameObject newActiveCard = ReturnNextCardAndSetItAcitve();
        UpdateValueOfActiveGameCard(newActiveCard);
        UpdateNumberOfCardsInGameDeck();
        DealPlayerCard();
        UpdateNumberOfCardsInHand();
        Debug.Log("new round is started");

    }

    private void DiscardActiveGameCard()
    {
        Transform transformOfActiveCard = activeGameCardArea.transform.GetChild(0);
        Destroy(transformOfActiveCard.gameObject);
    }

    private void DiscardPlayArea() //TODO: Introduce Discardpile
    {
        while (playArea.transform.childCount > 0)
        {
            DestroyImmediate(playArea.transform.GetChild(0).gameObject);
        }
    }


    private GameObject ReturnNextCardAndSetItAcitve()
    {
        GameObject newActiveCard = gameDeck.GetComponent<GameDeck>().ReturnNextCardAndSetItAcitve();
        return newActiveCard;
    }
    private void UpdateValueOfActiveGameCard(GameObject newActiveCard)
    {
        GameCardDisplay cardDisplay = newActiveCard.GetComponent<GameCardDisplay>();
        int valueActiveGameCard = cardDisplay.GetCardValue();
        resultChecker.UpdateValueActiveGameCard(valueActiveGameCard);
        Debug.Log("activeCard: " + valueActiveGameCard);

        activeCardValueDisplay.text = Convert.ToString(valueActiveGameCard); //remove
    }
    private void DealPlayerCard()
    {
        //TODO: Introduce Player Deck First
    }

    private void UpdateNumberOfCardsInHand()
    {
        int numberOfCardsInHand = playerHand.transform.childCount;
        resultChecker.UpdateValueActiveGameCard(numberOfCardsInHand);
        Debug.Log("cards in Hand: " + numberOfCardsInHand);
    }

    private void UpdateNumberOfCardsInGameDeck()
    {
        int numberOfCardsInGameDeck = gameDeck.transform.childCount;
        resultChecker.UpdateNumberOfCardsInGameDeck(numberOfCardsInGameDeck);

        Debug.Log("cards in GameDech: " + numberOfCardsInGameDeck);
    }

    private void StartFirstRound()
    {
        GameObject firstCard = ReturnNextCardAndSetItAcitve();
        UpdateValueOfActiveGameCard(firstCard);
        UpdateNumberOfCardsInGameDeck();        
    }
    

    private void OnDestroy()
    {
        DragAndDropCards.OnNewCardInPlayArea -= HandleNewCardInPlayArea;
        ResultChecker.OnActiveCardSurpassed -= StartNewRound;
    }






}
