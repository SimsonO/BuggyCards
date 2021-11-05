using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropCards : MonoBehaviour
{
    private bool isDragging;
   
    private GameObject startParent;

    private GameObject canvas;
    private GameObject playArea;


    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        playArea = GameObject.FindGameObjectWithTag("PlayArea");

    }
    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.position = mousePosition;
        }
    }

    public void BeginDrag()
    {
        startParent = transform.parent.gameObject;
        transform.SetParent(canvas.transform, true);
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
        if(PlayAreaIsSelected())
        {
            DropCardInPlayArea();
        }
        else
        {
            MoveCardBackToHand();
        }
    }
    private bool PlayAreaIsSelected()
    {
        return false;
    }

    private void DropCardInPlayArea()
    {
        transform.SetParent(playArea.transform, false);
    }

    private void MoveCardBackToHand()
    {
        transform.SetParent(startParent.transform, false);        
    }
}
