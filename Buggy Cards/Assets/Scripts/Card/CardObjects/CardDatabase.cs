using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "cardDB", menuName = "Cards/Database")]
public class CardDatabase : ScriptableObject
{
    [SerializeField]
    public List<Card> cards = new List<Card>();
  
}
