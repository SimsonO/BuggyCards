using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject victoryScreen;
    [SerializeField]
    private GameObject defeatScreen;

    private void Awake()
    {
        ResultChecker.OnGameWon += ActivateVictoryScreen;
        ResultChecker.OnGameLost += ActivateDefeatScreen;
    }

    public void DeactivateStartMenu()
    {
        startMenu.SetActive(false);
    }

    private void ActivateVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }

    public void DeActivateVictoryScreen()
    {
        victoryScreen.SetActive(false);
    }
    private void ActivateDefeatScreen()
    {
        defeatScreen.SetActive(true);
    }

    public void DeActivateDefeatScreen()
    {
        defeatScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        ResultChecker.OnGameWon -= ActivateVictoryScreen;
        ResultChecker.OnGameLost -= ActivateDefeatScreen;
    }
}
