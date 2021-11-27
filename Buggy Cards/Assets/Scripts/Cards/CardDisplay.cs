using UnityEngine;
using TMPro;
using System;

public class CardDisplay : MonoBehaviour
{
    private Card card;

    [SerializeField]
    private TextMeshProUGUI valueText1;
    [SerializeField]
    private TextMeshProUGUI valueText2;

    public void SetCardInformation(Card baseCard)
   {
        this.card = baseCard;
        valueText1.text = Convert.ToString(card.value);
        valueText2.text = Convert.ToString(card.value);
    }  

    public Card GetCard()
    {
        return card;
    }
}
