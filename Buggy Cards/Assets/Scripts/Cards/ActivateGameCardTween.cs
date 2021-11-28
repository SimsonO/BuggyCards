using UnityEngine;
using DG.Tweening;

public class ActivateGameCardTween : MonoBehaviour
{
    private Sequence DrawCardTween;

    private GameObject activeGameCardArea;

    private PlayfieldManager playfieldManager;

    private void Awake()
    {       
        playfieldManager = FindObjectOfType<PlayfieldManager>();
        activeGameCardArea = GameObject.FindGameObjectWithTag("ActiveCardArea");
    }

    public void StartActivateCardTween()
    {
        DrawCardTween = DOTween.Sequence()
             .SetEase(Ease.OutSine)
             .OnComplete(SetParentToactiveGameCardArea);
        DrawCardTween
            .Append(transform.DOMove(activeGameCardArea.transform.position, 1));
        //DrawCardTween
        //  .Insert(0, transform.DOLookAt(mainCamera.transform.position, 2));
    }

    private void SetParentToactiveGameCardArea()
    {
        transform.SetParent(activeGameCardArea.transform, false);
    }


}

