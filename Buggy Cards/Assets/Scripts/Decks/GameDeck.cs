using System.Collections.Generic;
using UnityEngine;

public class GameDeck : MonoBehaviour
{
    [SerializeField]
    private GameObject ActiveGameCardArea;

    private List<GameObject> gameDeck;

    [SerializeField]
    private CardDatabase cardDB;
    private int numberOfGameCardsInDB;

    //Event that will be broadcast whenever a new Card is set to beat
    public delegate void NewCardToBeat(Card card, int numberOfcardsInGameDeck);
    public static event NewCardToBeat OnNewCardToBeat;

    void Awake() 
    {
        numberOfGameCardsInDB = cardDB.GameCards.Count;
        gameDeck = new List<GameObject>();        
    }
       
    public void GenerateGameDeck(int deckSize)
    {
        gameDeck = new List<GameObject>();
        for (int i = 0; i < deckSize; i++)
        {
            GameCard card =  GetRandomCardFromDB();
            GameObject gameCard = GenerateGameCardObject(card);
            gameDeck.Add(gameCard);
        }
    }

    private GameCard GetRandomCardFromDB()
    {
        int cardId = Random.Range(0, numberOfGameCardsInDB);
        GameCard card = cardDB.GameCards[cardId];
        return card;
    }

    private GameObject GenerateGameCardObject(GameCard card)
    {
        GameObject cardObject = Instantiate(card.prefab, Vector3.zero, Quaternion.identity);
        CardDisplay display = cardObject.GetComponent<CardDisplay>();
        display.SetCardInformation(card);
        cardObject.transform.SetParent(this.transform, false);
        return cardObject;
    }  

    public void DiscardActiveCard()
    {
        while (ActiveGameCardArea.transform.childCount > 0)
        {
            DestroyImmediate(ActiveGameCardArea.transform.GetChild(0).gameObject);
        }
    }

    public void ActivateNextCard()
    {
        CardDisplay display = gameDeck[0].GetComponent<CardDisplay>();
        Card card = display.GetCard();
        gameDeck[0].transform.SetParent(ActiveGameCardArea.transform, false);        
        gameDeck.RemoveAt(0);
        int numberOfCardInGameDeck = gameDeck.Count;
        OnNewCardToBeat?.Invoke(card, numberOfCardInGameDeck);
    }    
}
