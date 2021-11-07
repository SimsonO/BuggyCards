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
        numberOfcardsInDB = cardDB.PlayerCards.Count;
    }

    public void SpawnACardInHandArea()
    {
        int cardId = Random.Range(0, numberOfcardsInDB);        
        GameObject playerCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        PlayerCardDisplay display = playerCard.GetComponent<PlayerCardDisplay>();
        display.SetCardInformation(cardDB.PlayerCards[cardId]);
        playerCard.transform.SetParent(handArea.transform,false);
    }

}