using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHand;

    [SerializeField]
    private GameObject playArea;
    private int valueInPlayArea;

    [SerializeField]
    private GameObject activeGameCard;
    private int valueActiveGameCard;

    private void Start()
    {
        DragAndDropCards.OnNewCardInPlayArea += HandleNewCardInPlayArea;
    }

    private void HandleNewCardInPlayArea()
    {
        UpdateSumOfCardValue();
    }

    private void UpdateSumOfCardValue()
    {
        valueInPlayArea = 0;
        PlayerCardDisplay[] cardsInPlayArea = GetComponentsInChildren<PlayerCardDisplay>();
        foreach (PlayerCardDisplay card in cardsInPlayArea)
        {
            valueInPlayArea += card.GetCardValue();
        }
    }



}
