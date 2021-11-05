using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropCards : MonoBehaviour
{
    private bool isDragging;
    private bool overPlayArea = false;
   
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
        if(overPlayArea)
        {
            DropCardInPlayArea();
        }
        else
        {
            MoveCardBackToHand();
        }
    }  

    private void DropCardInPlayArea()
    {
        transform.SetParent(playArea.transform, false);
    }

    private void MoveCardBackToHand()
    {
        transform.SetParent(startParent.transform, false);        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("over PlayArea");
        if (collider.gameObject.tag == "PlayArea")
        {
            overPlayArea = true;
          
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayArea")
        {
            overPlayArea = false;

            Debug.Log("not over PlayArea");
        }
    }
}
