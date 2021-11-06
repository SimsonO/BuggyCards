using UnityEngine;
using TMPro;
using System;

public class PlayerCardDisplay : MonoBehaviour
{
    private PlayerCard card;

    [SerializeField]
    private TextMeshProUGUI valueText;

   public void SetCardInformation(PlayerCard baseCard)
   {
        this.card = baseCard;
        valueText.text = Convert.ToString(card.value);
   }

    public int GetCardValue()
    {
        return card.value;
    }
}
