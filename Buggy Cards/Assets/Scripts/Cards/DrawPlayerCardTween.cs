using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DrawPlayerCardTween : MonoBehaviour
{
    private Sequence DrawCardTween;

    private GameObject handPosition;   
    private Camera mainCamera;

    private PlayfieldManager playfieldManager;

    private void Awake()
    {
        mainCamera = Camera.main;
        playfieldManager = FindObjectOfType<PlayfieldManager>();
        handPosition = GameObject.FindGameObjectWithTag("HandPositionOnPlayfield");
        
    }

    public void StartCardDrawTween()
    {
        DrawCardTween = DOTween.Sequence()
             .SetEase(Ease.OutSine)
             .OnComplete(AddCardTohand);
        DrawCardTween
            .Append(transform.DOMove(handPosition.transform.position, 3));
        //DrawCardTween
          //  .Insert(0, transform.DOLookAt(mainCamera.transform.position, 2));
    }

    private void AddCardTohand()
    {
        playfieldManager.AddCardToHand(this.gameObject);
    }


}
