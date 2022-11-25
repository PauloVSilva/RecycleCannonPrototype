using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MenuController
{
    #region BUTTONS
    public void Play()
    {
        //CanvasManager.instance.OpenMenu(Menu.LevelSelectionMenu);
        CanvasManager.instance.CloseMenu();
        LevelLoader.instance.LoadLevel("GameScene");
    }

    public void Store()
    {
        CanvasManager.instance.OpenMenu(Menu.StoreMenu);
    }

    public void Settings()
    {
        CanvasManager.instance.OpenMenu(Menu.SettingsMenu);
    }
    #endregion BUTTONS
}
