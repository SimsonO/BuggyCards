using System.Collections.Generic;
using UnityEngine;

public class GameDeck : MonoBehaviour
{
    [SerializeField]
    private GameObject ActiveGameCardArea;
    [SerializeField]
    private GameObject playfield;
    [SerializeField]
    private GameObject starpostionActivateGameCard;

    private List<GameObject> gameDeck;


    [SerializeField]
    private CardDatabase cardDB;
    private int numberOfGameCardsInDB;

    [SerializeField]
    private int maxDeckSum;

    //Event that will be broadcast whenever a new Card is set to beat
    public delegate void NewCardToBeat(Card card, int numberOfcardsInGameDeck);
    public static event NewCardToBeat OnNewCardToBeat;

    void Awake() 
    {
        numberOfGameCardsInDB = cardDB.GameCards.Count;
        gameDeck = new List<GameObject>();        
    }

    private void Start()
    {
        GameStateManager.OnInitiateDiscardPhase += DiscardActiveCard;
        GameStateManager.OnGameDidEnd += DiscardActiveCard;
        GameStateManager.OnGameDidEnd += DiscardGameDeck;
    }

    public void GenerateGameDeck(int deckSize)
    {
        List<GameCard> deck = new List<GameCard>();
        int deckSum = 0;
        for (int i = 0; i < deckSize; i++)
        {
            GameCard card =  GetRandomCardFromDB();
            deck.Add(card);
            deckSum += card.value;
        }
        if (deckSum < maxDeckSum)
        {
            SpawnGameDeck(deck);
        }
        else
        {
            GenerateGameDeck(deckSize);
        }
    }

    private GameCard GetRandomCardFromDB()
    {
        int cardId = Random.Range(0, numberOfGameCardsInDB);
        GameCard card = cardDB.GameCards[cardId];
        return card;
    }

    private void SpawnGameDeck(List<GameCard> deck)
    {
        gameDeck = new List<GameObject>();
        for (int i = 0; i < deck.Count; i++)
        {
            GameCard card = deck[i];
            GameObject gameCard = GenerateGameCardObject(card);
            gameDeck.Add(gameCard);
        }
    }

    private GameObject GenerateGameCardObject(GameCard card)
    {
        GameObject cardObject = Instantiate(card.prefab, Vector3.zero, Quaternion.identity);
        CardDisplay display = cardObject.GetComponent<CardDisplay>();
        display.SetCardInformation(card);
        cardObject.transform.SetParent(this.transform, false);
        return cardObject;
    }  

    private void DiscardActiveCard()
    {
        while (ActiveGameCardArea.transform.childCount > 0)
        {
            DestroyImmediate(ActiveGameCardArea.transform.GetChild(0).gameObject);
        }
    }

    public void ActivateNextCard()
    {
        GameObject cardObject = gameDeck[0];
        CardDisplay display = cardObject.GetComponent<CardDisplay>();
        Card card = display.GetCard();
        cardObject.transform.SetParent(playfield.transform, false);
        cardObject.transform.position = starpostionActivateGameCard.transform.position;
        cardObject.GetComponent<ActivateGameCardTween>().StartActivateCardTween();  
        gameDeck.RemoveAt(0);
        int numberOfCardInGameDeck = gameDeck.Count;
        OnNewCardToBeat?.Invoke(card, numberOfCardInGameDeck);
    }

    private void DiscardGameDeck()
    {
        for (int i = gameDeck.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(gameDeck[i].gameObject);
            gameDeck.RemoveAt(i);
        }
    }

    private void OnDestroy()
    {
        GameStateManager.OnInitiateDiscardPhase -= DiscardActiveCard;
        GameStateManager.OnGameDidEnd -= DiscardActiveCard;
        GameStateManager.OnGameDidEnd -= DiscardGameDeck;
    }
}
