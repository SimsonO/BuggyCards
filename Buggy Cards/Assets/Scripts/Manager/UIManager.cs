using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject startMenu;

    public void deactivateStartMenu()
    {
        startMenu.SetActive(false);
    }

    public void activateStartMenu()
    {
        startMenu.SetActive(true);
    }
}
