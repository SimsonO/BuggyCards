using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDropCards : MonoBehaviour
{
    private bool isDragging;
    private bool overPlayArea = false;

    private Plane playfieldPlane;
   
    private GameObject startParent;    

    private GameObject playField;
    private GameObject playArea;
    private GameObject handArea;

    //Event that will be broadcast whenever a card is placed in the Play Are
    public delegate void NewCardInPlayArea(GameObject card);
    public static event NewCardInPlayArea OnNewCardInPlayArea;
    private void Start()
    {
        playField = GameObject.FindGameObjectWithTag("Playfield");
        playArea = GameObject.FindGameObjectWithTag("PlayArea");
        handArea = GameObject.FindGameObjectWithTag("HandArea");
        playfieldPlane = new Plane(Vector3.forward, Vector3.zero);


    }
    private void Update()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayLenght;
            playfieldPlane.Raycast(ray, out rayLenght);

            Vector3 mouseOnPlane = ray.GetPoint(rayLenght);            
            transform.position = mouseOnPlane;
        }
    }  

    public void BeginDrag()
    {
        GameObject parent = this.transform.parent.gameObject;
        if (parent == handArea)
        {
            startParent = transform.parent.gameObject;
            transform.SetParent(playField.transform, false);
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
