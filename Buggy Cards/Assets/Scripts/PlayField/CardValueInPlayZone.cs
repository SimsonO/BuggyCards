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
    private PlayerCardDisplay[] cardsInPlayArea;

    private void Start()
    {
        DragAndDropCards.OnNewCardInPlayArea += UpdateSumOfCardValue;
    }
    private void UpdateSumOfCardValue()
    {
        sumOfCards = 0;
        cardsInPlayArea = GetComponentsInChildren<PlayerCardDisplay>();
        foreach (PlayerCardDisplay card in cardsInPlayArea)
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
