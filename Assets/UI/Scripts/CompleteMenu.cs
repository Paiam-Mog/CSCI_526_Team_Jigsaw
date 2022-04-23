using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteMenu : MonoBehaviour
{
    public GameObject Menu;

    [SerializeField]
    GameManagerScript gm;

    private bool isToggled;

    [SerializeField] public Button menuButton;
    [SerializeField] public Button colorButton;
    [SerializeField] public Button infoButton;

    void Awake()
    {
        CloseMenu();
    }

    public void ToggleMenu()
    {
        if (!isToggled)
        {
            Menu.SetActive(true);
            isToggled = true;

            gm.PauseGame();

            menuButton.interactable = false;
            colorButton.interactable = false;
            infoButton.interactable = false;


        }
        else if (isToggled)
        {
            Menu.SetActive(false);
            isToggled = false;

            gm.ResumeGame();

            menuButton.interactable = true;
            colorButton.interactable = true;
            infoButton.interactable = true;
        }

    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
        isToggled = false;

        gm.ResumeGame();
    }
}
