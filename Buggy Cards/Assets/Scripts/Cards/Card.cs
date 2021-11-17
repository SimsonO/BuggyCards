using UnityEngine;

public class Card : ScriptableObject
{
    public string cardName;
    public int value;

    public int GetCardValue()
    {
        return value;
    }
}

