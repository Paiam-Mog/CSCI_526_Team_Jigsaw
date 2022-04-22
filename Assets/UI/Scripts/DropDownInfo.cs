using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownInfo : MonoBehaviour
{
    public Button dropdownBtn;
    public GameObject Menu;

    [SerializeField]
    GameManagerScript gm;

    private bool toggleOnStart;

    private bool isToggled;

    void Awake()
    {

        if (toggleOnStart)
        {
            Menu.SetActive(true);
            isToggled = true;

            toggleOnStart = false;

            if (gm) gm.PauseGame();
        }
        else
        {
            Menu.SetActive(false);
            isToggled = false;
        }
    }

    void Update()
    {
        if (!gm) return;

        if (gm.GetLevelNumber() == 2)
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
        if (!isToggled)
        {
            Menu.SetActive(true);
            isToggled = true;

            if (gm) gm.PauseGame();
        }
        else if (isToggled)
        {
            Menu.SetActive(false);
            isToggled = false;

            if (gm) gm.ResumeGame();
        }

    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
        isToggled = false;

        if (gm) gm.ResumeGame();
    }
}
