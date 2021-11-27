using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BugController : MonoBehaviour
{
    PlayerDeck deck;

    private Vector3 leavePosition;

    [SerializeField]
    private float timeToMoveToDeck;
    [SerializeField]
    private float timeToLeavePlayfield;
    [SerializeField]
    private float timeToMoveToCard;

    private AudioSource bugAudio;
    [SerializeField]
    private AudioClip bugCrawl;
    [SerializeField]
    private AudioClip bugEat;

    private void Start()
    {
        bugAudio = GetComponent<AudioSource>();
    }
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
        transform.DOMove(deck.transform.position, timeToMoveToDeck, false).OnComplete(ChooseCardAndEatIt);
    }

    private void ChooseCardAndEatIt()
    {
        if(deck.GetNumberOfCardsInPlayerDeck() >0)
        {
            GameObject card = deck.GetCardToBeEatenByBug();
            transform.DOMove(card.transform.position, timeToMoveToCard, false).OnComplete(() => EatCard(card));            
        }
        else
        {
            LeaveThePlayfield();
        }

    }    

    private void EatCard(GameObject card)
    {
        bugAudio.PlayOneShot(bugEat);
        //StartPArticles
        Destroy(card);
        LeaveThePlayfield();
    } 
    
    private void LeaveThePlayfield()
    {
        transform.DOMove(leavePosition, timeToLeavePlayfield, false).OnComplete(DestroyBug);
    }

    private void DestroyBug()
    {
        Destroy(this.gameObject);
    }
}