using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MenuController
{
    #region BUTTONS
    public void ReturnToMainMenu()
    {
        LevelLoader.instance.LoadLevel("MainScene");
        CanvasManager.instance.SwitchMenu(Menu.MainMenu);
    }
    #endregion BUTTONS
}
