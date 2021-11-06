using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class CardValueInPlayZone : MonoBehaviour
{
    private int sumOfCards;

    [SerializeField]
    private TextMeshProUGUI sumOfCardsDisplay;
    private CardDisplay[] cardsInPlayArea;

    private void Start()
    {
        DragAndDropCards.OnNewCardInPlayArea += UpdateSumOfCardValue;
    }
    private void UpdateSumOfCardValue()
    {
        sumOfCards = 0;
        cardsInPlayArea = GetComponentsInChildren<CardDisplay>();
        foreach (CardDisplay card in cardsInPlayArea)
        {
            sumOfCards += card.GetCardValue();
        }
        sumOfCardsDisplay.text = Convert.ToString(sumOfCards);
    }

    private void OnDestroy()
    {
        DragAndDropCards.OnNewCardInPlayArea -= UpdateSumOfCardValue;
    }
}
