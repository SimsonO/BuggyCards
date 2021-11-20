using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private List<PlayerCard> deck;

    private CardDatabase cardDB;
    private int numberOfPlayerCardsInDB;
    private PlayfieldManager playfieldManager;

    public Deck(CardDatabase cardDB, PlayfieldManager playfieldManager)
    {
        deck = new List<PlayerCard>();
        this.cardDB = cardDB;
        numberOfPlayerCardsInDB = cardDB.PlayerCards.Count;
        this.playfieldManager = playfieldManager;
    }

    public void GenerateGameDeck(int deckSize)
    {
        for (int i = 0; i < deckSize; i++)
        {
            PlayerCard card = GetRandomCardFromDB();
            deck.Add(card);
        }
    }

    private PlayerCard GetRandomCardFromDB()
    {
        int cardId = Random.Range(0, numberOfPlayerCardsInDB);
        PlayerCard card = cardDB.PlayerCards[cardId];
        return card;
    }

    public void DrawNextXCards(int x)
    {
        for (int i = 0; i < x; i++)
        {
            playfieldManager.SpawnCardInHand(deck[0]);
            deck.RemoveAt(0);
        }
    }
}
