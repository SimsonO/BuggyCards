using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    private List<GameObject> playerDeck;
    [SerializeField]
    private Vector3 postitionFirstCard;
    [SerializeField]
    private Vector3 offsetCards = new Vector3(40, -40, 0);

    [SerializeField]
    private CardDatabase cardDB;
    private int numberOfGameCardsInDB;
    [SerializeField]
    private int minDeckSum;

    //Event that will be broadcast whenever active Card is not surpassed and Hand is empty -> game is lost
    public delegate void GameLost();
    public static event GameLost OnGameLost;

    void Awake()
    {
        numberOfGameCardsInDB = cardDB.PlayerCards.Count;
        playerDeck = new List<GameObject>();
        postitionFirstCard = this.transform.position;
    }
    public void GeneratePlayerDeck(int deckSize)
    {
        List<PlayerCard> deck = new List<PlayerCard>();
        int deckSum = 0;
        for (int i = 0; i < deckSize; i++)
        {
            PlayerCard card = GetRandomCardFromDB();
            deck.Add(card);
            deckSum += card.value;           
        }
        if(deckSum > minDeckSum)
        {
            SpawnPlayerDeck(deck);
        }
        else
        {
            GeneratePlayerDeck(deckSize);
        }
    }
    private PlayerCard GetRandomCardFromDB()
    {
        int cardId = Random.Range(0, numberOfGameCardsInDB);
        PlayerCard card = cardDB.PlayerCards[cardId];
        return card;
    }

    private void SpawnPlayerDeck(List<PlayerCard> deck)
    {
        playerDeck = new List<GameObject>();
        for (int i = 0; i < deck.Count; i++)
        {
            PlayerCard card = deck[i];
            Vector3 spawnPosition = postitionFirstCard + i * offsetCards;
            GameObject playerCard = GeneratePlayerCardObject(card, spawnPosition);
            playerDeck.Add(playerCard);
        }
    }

    private GameObject GeneratePlayerCardObject(PlayerCard card, Vector3 spawnPosition)
    {
        GameObject cardObject = Instantiate(card.prefab, spawnPosition, Quaternion.identity);
        CardDisplay display = cardObject.GetComponent<CardDisplay>();
        display.SetCardInformation(card);                
        cardObject.transform.SetParent(this.transform, true);
        cardObject.transform.localScale = this.transform.localScale;
        return cardObject;
    }

    public void DrawNextXCards(int x)
    {
        int n = playerDeck.Count;
        if (n > 0)
        {
            for (int i = n - 1; i >= n - x; i--)
            {
                GameObject card = playerDeck[i];
                card.GetComponent<DrawPlayerCardTween>().StartCardDrawTween();
                playerDeck.Remove(card);
            }
        }
        else
        {
            OnGameLost?.Invoke();
        }
        
    }
    public int GetNumberOfCardsInPlayerDeck()
    {
        return playerDeck.Count;
    }

    public GameObject GetCardToBeEatenByBug()
    {
        int i = Random.Range(0, playerDeck.Count - 1);
        GameObject card = playerDeck[i];
        playerDeck.Remove(card);                
        return card; 
    }

    

}
