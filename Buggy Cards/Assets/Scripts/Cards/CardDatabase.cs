using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "cardDB", menuName = "Cards/Database")]
public class CardDatabase : ScriptableObject
{
    [SerializeField]
    public List<PlayerCard> PlayerCards = new List<PlayerCard>();

    [SerializeField]
    public List<GameCard> GameCards = new List<GameCard>();

}
