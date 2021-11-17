using UnityEngine;
using TMPro;
using System;

public class CardDisplay : MonoBehaviour
{
    private Card card;

    [SerializeField]
    private TextMeshProUGUI valueText;

   public void SetCardInformation(Card baseCard)
   {
        this.card = baseCard;
        valueText.text = Convert.ToString(card.value);
   }  

    public Card GetCard()
    {
        return card;
    }
}
