using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Menu { MainMenu, ControlsMenu, SettingsMenu, PauseMenu, LevelSelectionMenu, CharacterSelectionMenu, StoreMenu, GameOverMenu }

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance = null;

    [SerializeField] private List<MenuController> menuControllersList;
    [SerializeField] private List<MenuController> allActiveMenus = new List<MenuController>();
    [SerializeField] public MenuController currentMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        menuControllersList = GetComponentsInChildren<MenuController>().ToList();
        //menuControllersList.ForEach(x => x.gameObject.SetActive(false)); //x is menuController
        SwitchMenu(Menu.MainMenu); //this line is only used if I want a specific menu to pop up as soon as the scene opens
    }

    public void OpenMenu(int menuIndex)
    {
        OpenMenu((Menu)menuIndex);
    }

    public void SwitchMenu(int menuIndex)
    {
        SwitchMenu((Menu)menuIndex);
    }

    public void OpenMenu(Menu _menu)
    {
        MenuController desiredMenu = menuControllersList.Find(x => x.menu == _menu);
        if (desiredMenu != null)
        {
            currentMenu = desiredMenu;
            allActiveMenus.Add(currentMenu);
            currentMenu.Open();
        }
        else
        {
            Debug.LogWarning("Desired menu was not found D:");
        }
    }

    public void SwitchMenu(Menu _menu)
    {
        CloseMenu();
        OpenMenu(_menu);
    }

    public void CloseMenu()
    {
        if (currentMenu != null)
        {
            allActiveMenus.Remove(currentMenu);
            currentMenu.Close();
        }
        if (allActiveMenus.Count() > 0)
        {
            currentMenu = allActiveMenus[allActiveMenus.Count() - 1];
            currentMenu.GainControl();
        }
        else
        {
            currentMenu = null;
            //PauseMenu.instance.UnfreezeGame();
        }
    }
}
