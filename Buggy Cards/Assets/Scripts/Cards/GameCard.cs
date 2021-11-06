using UnityEngine;

[CreateAssetMenu(fileName = "New GameCard", menuName = "Cards/New Game Card")]
public class GameCard : ScriptableObject
{

    public string cardName;
    public int value;

    public GameObject cardObject;

}
