using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private GameObject handArea;


    public void SpawnACardInHandArea()
    {
        GameObject playerCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        playerCard.transform.SetParent(handArea.transform,false);
    }

}