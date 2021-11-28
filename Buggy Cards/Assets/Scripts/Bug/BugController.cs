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

    private Sequence moveToDeck;
    private Sequence moveToCard;

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
        transform.rotation = Quaternion.Euler(0, 0, 150);
        moveToDeck =  DOTween.Sequence();
        moveToDeck
            .Append(transform.DOMove(deck.transform.position, timeToMoveToDeck, false))
            //.Insert(0,transform.DOLookAt(deck.transform.position, 1,AxisConstraint.W,Vector3.forward))
            .OnComplete(ChooseCardAndEatIt);
    }

    private void ChooseCardAndEatIt()
    {
        if(!deck.IsEmpty())
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
        transform.rotation = Quaternion.Euler(0, 0, 180);
        transform.DOMove(leavePosition, timeToLeavePlayfield, false).OnComplete(DestroyBug);
    }
    private void DestroyBug()
    {
        Destroy(this.gameObject);
    }

}
