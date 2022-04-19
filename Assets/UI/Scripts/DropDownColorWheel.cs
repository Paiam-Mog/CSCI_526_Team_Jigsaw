using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownColorWheel: MonoBehaviour
{
    public Button dropdownBtn;
    public GameObject Menu;

    [SerializeField] 
    GameManagerScript gm;

    private bool isToggled;

    private bool toggleOnStart;

    void Awake()
    {
        if (toggleOnStart)
        {
            Menu.SetActive(true);
            isToggled = true;

            toggleOnStart = false;

            gm.PauseGame();
        }
        else
        {
            Menu.SetActive(false);
            isToggled = false;
        }

    }

    void Update()
    {
        if (gm.GetLevelNumber() == 3)
        {
            toggleOnStart = true;
        }
        else
        {
            toggleOnStart = false;
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
