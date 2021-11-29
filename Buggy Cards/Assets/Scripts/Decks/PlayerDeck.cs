using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    private List<GameObject> playerDeck;
    [SerializeField]
    private Vector3 postitionFirstCard;
    [SerializeField]
    private Vector3 offsetCards = new Vector3(40, -40, 0);

    private AudioSource deckAudio;
    [SerializeField]
    private AudioClip cardDraw;
    [SerializeField]
    private AudioClip bugEat;

    [SerializeField]
    private CardDatabase cardDB;
    private int numberOfGameCardsInDB;
    
    private int minDeckSum;

    //Event that will be broadcast whenever no more Card can be Drawn
    public delegate void DeckEmtpy();
    public static event DeckEmtpy OnDeckEmtpy;

    void Awake()
    {
        numberOfGameCardsInDB = cardDB.PlayerCards.Count;
        playerDeck = new List<GameObject>();
        postitionFirstCard = this.transform.position;
    }
    private void Start()
    {
        GameStateManager.OnGameDidEnd += DiscardDeck;
        deckAudio = GetComponent<AudioSource>();
    }
    public void GeneratePlayerDeck(int deckSize, int minDeckSum)
    {
        this.minDeckSum = minDeckSum;
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
            GeneratePlayerDeck(deckSize, minDeckSum);
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
                deckAudio.PlayOneShot(cardDraw);
            }
        }
        else
        {
            OnDeckEmtpy?.Invoke();
        }
        
    }
    public bool IsEmpty()
    {

        if (playerDeck.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public GameObject GetCardToBeEatenByBug()
    {
        int i = Random.Range(0, playerDeck.Count - 1);
        GameObject card = playerDeck[i];
        playerDeck.Remove(card);
        deckAudio.PlayOneShot(bugEat);
        if (playerDeck.Count <= 0)
        {
            OnDeckEmtpy?.Invoke();
        }
        return card; 
    }

    private void DiscardDeck()
    {
        for (int i = playerDeck.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(playerDeck[i].gameObject);
            playerDeck.RemoveAt(i);
        }
    }

    private void OnDestroy()
    {
        GameStateManager.OnGameDidEnd -= DiscardDeck;
    }



}
