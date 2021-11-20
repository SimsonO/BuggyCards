using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    [SerializeField]
    private GameObject hand;

    private List<GameObject> playerDeck;
    [SerializeField]
    private Vector3 postitionFirstCard;
    [SerializeField]
    private Vector3 offsetCards = new Vector3(40, -40, 0);
    [SerializeField]
    int deckSize = 5;

    [SerializeField]
    private CardDatabase cardDB;
    private int numberOfGameCardsInDB;
    void Awake()
    {
        numberOfGameCardsInDB = cardDB.PlayerCards.Count;
        playerDeck = new List<GameObject>();
        postitionFirstCard = this.transform.position;
    }

    private void Start()
    {
        GeneratePlayerDeck(deckSize);
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
        //cardObject.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        cardObject.transform.SetParent(this.transform, true);
        return cardObject;
    }
}
