using System.Collections.Generic;
using UnityEngine;

public class GameDeck : MonoBehaviour
{
    [SerializeField]
    private GameObject gameCardPrefab;
    [SerializeField]
    private GameObject ActiveGameCardArea;

    [SerializeField]
    private int deckSize = 4;
    private List<GameObject> gameDeck;

    [SerializeField]
    private CardDatabase cardDB;
    private int numberOfGameCardsInDB;

    void Awake() //TODO: moveto start when generate Game is out of start in playfieldmanager
    {
        numberOfGameCardsInDB = cardDB.GameCards.Count;
        gameDeck = new List<GameObject>();        
    }

    public void GenerateGameDeck()
    {
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
        GameObject cardObject = Instantiate(gameCardPrefab, Vector3.zero, Quaternion.identity);
        GameCardDisplay display = cardObject.GetComponent<GameCardDisplay>();
        display.SetCardInformation(card);
        cardObject.transform.SetParent(this.transform, false);
        return cardObject;
    }

    public void DealCard()
    {
        gameDeck[0].transform.SetParent(ActiveGameCardArea.transform, false);
        gameDeck.RemoveAt(0);
    }

    public int GetNumberOfCardsInDeck()
    {       
        int numberOfGameCardsDeck = this.transform.childCount;
        return numberOfGameCardsDeck;
    }
}
