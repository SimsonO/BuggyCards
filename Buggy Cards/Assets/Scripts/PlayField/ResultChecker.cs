using System.Collections.Generic;
public class ResultChecker
{
    private int numberOfCardsInGameDeck;
    private int valueInPlayArea;
    private int valueCardToBeat;
    private bool deckEmpty;

    private List<Card> cardsInPlayArea;
    private Card cardTobeat;

    public ResultChecker(List<Card> cardsInPlayArea)
    {
        this.cardsInPlayArea = cardsInPlayArea;
        GameDeck.OnNewCardToBeat += UpdateGameValues;
        PlayerDeck.OnDeckEmtpy += SetDeckEmptyTrue;
        GameStateManager.OnGameDidEnd += SetDeckEmptyFalse;

    }

    //Event that will be broadcast whenever active Card is surpassed but Game is not won
    public delegate void ActiveCardSurpassed();
    public static event ActiveCardSurpassed OnActiveCardSurpassed;

    //Event that will be broadcast whenever active Card is surpassed and the Game is  won
    public delegate void GameWon();
    public static event GameWon OnGameWon;

    //Event that will be broadcast whenever active Card is not surpassed and Hand is empty -> game is lost
    public delegate void GameLost();
    public static event GameLost OnGameLost;

    private void UpdateGameValues(Card card, int numberOfCardsInGameDeck)
    {
        this.numberOfCardsInGameDeck = numberOfCardsInGameDeck;
        cardTobeat = card;
    }

    private void SetDeckEmptyTrue()
    {
        deckEmpty = true;
    }

    private void SetDeckEmptyFalse()
    {
        deckEmpty = false;
    }
    public void CheckForEndOfRound(int numberOfCardsInHand)
    {
        valueInPlayArea = 0;
        for (int i = 0;i < cardsInPlayArea.Count ;i++)
        {
            valueInPlayArea += cardsInPlayArea[i].value;
        }
        valueCardToBeat = cardTobeat.value;


        if (valueInPlayArea > valueCardToBeat && numberOfCardsInGameDeck > 0)
        {
            if(numberOfCardsInHand >0 || !deckEmpty)
            {
                OnActiveCardSurpassed?.Invoke();
            }
            else
            {
                OnGameLost?.Invoke();
            }
           
        }
        else if(valueInPlayArea > valueCardToBeat && numberOfCardsInGameDeck == 0)
        {
            OnGameWon?.Invoke();
        }
        else if (numberOfCardsInHand == 0)
        {
            OnGameLost?.Invoke();
        }
    }








}
