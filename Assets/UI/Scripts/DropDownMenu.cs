using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenu: MonoBehaviour
{
    public Button dropdownBtn;
    public GameObject Menu;

    [SerializeField] 
    GameManagerScript gm;

    [SerializeField]
    private bool isToggled;

    void Awake()
    {
        isToggled = false;
        Menu.SetActive(false);
        gm.GetComponent<GameManagerScript>();
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
