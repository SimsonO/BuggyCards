using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BugController : MonoBehaviour
{
    PlayerDeck deck;

    private Vector3 leavePosition;
    
    public void SetDeck(PlayerDeck deck)
    {
        this.deck = deck;
    }
    public void SetLeavePosition(Vector3 leavePosition)
    {
        this.leavePosition = leavePosition;
    }
    public void MoveToDeck()
    {
        transform.DOMove(deck.transform.position, 6, false).OnComplete(ChooseCardAndEatIt);
    }

    private void ChooseCardAndEatIt()
    {
        if(deck.GetNumberOfCardsInPlayerDeck() >0)
        {
            GameObject card = deck.GetCardToBeEatenByBug();
            transform.DOMove(card.transform.position, 0.5f, false).OnComplete(() => EatCard(card));            
        }
        else
        {
            LeaveThePlayfield();
        }

    }    

    private void EatCard(GameObject card)
    {
        //StartPArticles
        Destroy(card);
        LeaveThePlayfield();
    } 
    
    private void LeaveThePlayfield()
    {
        transform.DOMove(leavePosition, 4, false).OnComplete(DestroyBug);
    }

    private void DestroyBug()
    {
        Destroy(this.gameObject);
    }
}
