
public class ResultChecker
{

    private int numberOfCardsInHand;
    private int numberOfCardsInGameDeck;
    private int valueInPlayArea;
    private int valueActiveGameCard;

    //Event that will be broadcast whenever active Card is surpassed but Game is not won
    public delegate void ActiveCardSurpassed();
    public static event ActiveCardSurpassed OnActiveCardSurpassed;

    //Event that will be broadcast whenever active Card is surpassed and the Game is  won
    public delegate void GameWon();
    public static event GameWon OnGameWon;

    //Event that will be broadcast whenever active Card is not surpassed and Hand is empty -> game is lost
    public delegate void GameLost();
    public static event GameWon OnGameLost;

    public void UpdateNumberOfCardsInHand(int numberOfCardsInHand)
    {
        this.numberOfCardsInHand = numberOfCardsInHand;
    }

    public void UpdateNumberOfCardsInGameDeck(int numberOfCardsInGameDeck)
    {
        this.numberOfCardsInGameDeck = numberOfCardsInGameDeck;
    }
    public void UpdateValueInPlayArea(int valueInPlayArea)
    {
        this.valueInPlayArea = valueInPlayArea;
    }    

    public void UpdateValueActiveGameCard(int valueActiveGameCard)
    {
        this.valueActiveGameCard = valueActiveGameCard;
    }

    public void CheckForEndOfRound()
    {
        if(valueInPlayArea > valueActiveGameCard && numberOfCardsInGameDeck > 0)
        {
            OnActiveCardSurpassed?.Invoke();
        }
        else if(valueInPlayArea > valueActiveGameCard && numberOfCardsInGameDeck == 0)
        {
            OnGameWon?.Invoke();
        }
        else if (numberOfCardsInHand == 0)
        {
            OnGameLost?.Invoke();
        }
    }






}
