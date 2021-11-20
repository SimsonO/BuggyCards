using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropCards : MonoBehaviour
{
    private bool isDragging;
    private bool overPlayArea = false;
   
    private GameObject startParent;

    private GameObject playField;
    private GameObject playArea;

    //Event that will be broadcast whenever a card is placed in the Play Are
    public delegate void NewCardInPlayArea(GameObject card);
    public static event NewCardInPlayArea OnNewCardInPlayArea;
    private void Start()
    {
        playField = GameObject.FindGameObjectWithTag("Playfield");
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
        GameObject parent = this.transform.parent.gameObject;
        if (parent != playArea)
        {
            startParent = transform.parent.gameObject;
            transform.SetParent(playField.transform, true);
            isDragging = true;
        }
        
    }

    public void EndDrag()
    {
        if(overPlayArea && isDragging)
        {
            DropCardInPlayArea();
            OnNewCardInPlayArea?.Invoke(this.gameObject);
        }
        else if(isDragging)
        {
            MoveCardBackToHand();
        }
        isDragging = false;
    }  

    private void DropCardInPlayArea()
    {
        transform.SetParent(playArea.transform, false);
    }

    private void MoveCardBackToHand()
    {
        transform.SetParent(startParent.transform, false);        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {       
        if (other.gameObject.tag == "PlayArea")
        {
            overPlayArea = true;
          
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "PlayArea")
        {
            overPlayArea = false;
        }   
    }
}
