using UnityEngine;
using TMPro;
using System;

public class GameCardDisplay : MonoBehaviour
{
    private GameCard card;

    [SerializeField]
    private TextMeshProUGUI valueText;

    public void SetCardInformation(GameCard baseCard)
    {
        this.card = baseCard;
        valueText.text = Convert.ToString(card.value);
    }

    public int GetCardValue()
    {
        return card.value;
    }
}
