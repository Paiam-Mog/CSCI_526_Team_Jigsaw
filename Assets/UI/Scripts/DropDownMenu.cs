using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenu: MonoBehaviour
{
    public Button dropdownBtn;
    public GameObject Menu;
    public TopBarScript topBarScript;

    [SerializeField]
    private bool isToggled;

    void Awake()
    {
        isToggled = false;
        Menu.SetActive(false);
    }

    public void ToggleMenu()
    {
        if(!isToggled)
        {
            Menu.SetActive(true);
            isToggled = true;

            topBarScript.PauseGame();
        }
        else if (isToggled)
        {
            Menu.SetActive(false);
            isToggled = false;

            topBarScript.ResumeGame();
        }

    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
        isToggled = false;

        topBarScript.ResumeGame();
    }
}
