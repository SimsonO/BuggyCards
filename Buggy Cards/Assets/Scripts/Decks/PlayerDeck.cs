using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    [SerializeField]
    private GameObject hand;
    private PlayfieldManager playfieldManager;

    private List<GameObject> playerDeck;
    [SerializeField]
    private Vector3 postitionFirstCard;
    [SerializeField]
    private Vector3 offsetCards = new Vector3(40, -40, 0);

    [SerializeField]
    private CardDatabase cardDB;
    private int numberOfGameCardsInDB;
    void Awake()
    {
        playfieldManager = FindObjectOfType<PlayfieldManager>();
        numberOfGameCardsInDB = cardDB.PlayerCards.Count;
        playerDeck = new List<GameObject>();
        postitionFirstCard = this.transform.position;
    }
    public void GeneratePlayerDeck(int deckSize)
    {
        playerDeck = new List<GameObject>();
        for (int i = 0; i < deckSize; i++)
        {
            PlayerCard card = GetRandomCardFromDB();
            Vector3 spawnPosition = postitionFirstCard + i * offsetCards;
            GameObject playerCard = GeneratePlayerCardObject(card, spawnPosition);
            playerDeck.Add(playerCard);
        }
    }
    private PlayerCard GetRandomCardFromDB()
    {
        int cardId = Random.Range(0, numberOfGameCardsInDB);
        PlayerCard card = cardDB.PlayerCards[cardId];
        return card;
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
        for (int i = n-1; i >= n - x ; i--)
        {
            GameObject card = playerDeck[i];
            //MoveCard            
            playerDeck.Remove(card);
            playfieldManager.AddCardToHand(card);
        }
    }
}
