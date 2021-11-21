using System.Collections.Generic;
using UnityEngine;

public class PlayfieldManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHandArea;
    [SerializeField] 
    private GameObject playArea;

    private List<Card> hand;
    private List<Card> cardsInPlayArea;

    private ResultChecker resultChecker;
    private void Awake()
    {
        hand = new List<Card>();
        cardsInPlayArea = new List<Card>();
        resultChecker = new ResultChecker(cardsInPlayArea);
    }

    private void Start()
    {
        DragAndDropCards.OnNewCardInPlayArea += HandleNewCardInPlayArea;
        GameStateManager.OnInitiateDiscardPhase += DiscardPlayArea;
    }
    private void HandleNewCardInPlayArea(GameObject cardObject)
    {
        Card card = cardObject.GetComponent<CardDisplay>().GetCard();

        hand.Remove(card);
        cardsInPlayArea.Add(card);
        int numberOfCardInHand = hand.Count;
        resultChecker.CheckForEndOfRound(numberOfCardInHand);
    }

    public void AddCardToHand(GameObject cardObject)
    {
        cardObject.transform.SetParent(playerHandArea.transform, false);
        Card card = cardObject.GetComponent<CardDisplay>().GetCard();
        hand.Add(card);
    }

   
    public void DiscardPlayArea()
    {
        cardsInPlayArea.Clear();
        while (playArea.transform.childCount > 0)
        {
            DestroyImmediate(playArea.transform.GetChild(0).gameObject);
        }
    }

    private void OnDestroy()
    {
        DragAndDropCards.OnNewCardInPlayArea -= HandleNewCardInPlayArea;
        GameStateManager.OnInitiateDiscardPhase -= DiscardPlayArea;
    }
}
