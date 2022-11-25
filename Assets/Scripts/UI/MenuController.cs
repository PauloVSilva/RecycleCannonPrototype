using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuController : MonoBehaviour
{
    public Menu menu;
    [SerializeField] private GameObject menuContainer;


    public void Open()
    {
        menuContainer.SetActive(true);
        GainControl();
    }

    public void Close()
    {
        menuContainer.SetActive(false);
        //UnsubscribeFromInputActions();
    }

    public void GainControl()
    {
        //firstSelected.Select();
    }
}
