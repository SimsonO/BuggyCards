using UnityEngine;

public class DrawCard : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private GameObject handArea;
    [SerializeField]
    private CardDatabase cardDB;
    private int numberOfcardsInDB;

    private void Start()
    {
        numberOfcardsInDB = cardDB.cards.Count;
    }

    public void SpawnACardInHandArea()
    {
        int cardID = Random.Range(0, numberOfcardsInDB);        
        GameObject playerCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        CardDisplay display = playerCard.GetComponent<CardDisplay>();
        display.SetCardInformation(cardDB.cards[cardID]);
        playerCard.transform.SetParent(handArea.transform,false);
    }

}