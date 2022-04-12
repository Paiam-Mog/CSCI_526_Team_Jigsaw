using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownColorWheel: MonoBehaviour
{
    public Button dropdownBtn;
    public GameObject Menu;
    public bool turnOffMenuAtAwake = true;

    [SerializeField] 
    GameManagerScript gm;

    private bool isToggled;

    void Awake()
    {
        if (turnOffMenuAtAwake)
        {
            CloseMenu();
        }
        else
        {
            isToggled = true;
        }
    }

    public void ToggleMenu()
    {
        if(!isToggled)
        {
            Menu.SetActive(true);
            isToggled = true;

            gm.PauseGame();
        }
        else if (isToggled)
        {
            Menu.SetActive(false);
            isToggled = false;

            gm.ResumeGame();
        }

    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
        isToggled = false;

        gm.ResumeGame();
    }
}
