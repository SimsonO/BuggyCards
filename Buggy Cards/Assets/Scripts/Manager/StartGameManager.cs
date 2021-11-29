using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    
    void Start()
    {
        GameStateManager gameStateManager = GetComponent<GameStateManager>();
        gameStateManager.StartTheGame();
    }

}
