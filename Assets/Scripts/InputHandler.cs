using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    public event System.Action<InputAction.CallbackContext> OnCollectorMove;

    public event System.Action<InputAction.CallbackContext> OnCannonShoot;
    public event System.Action<InputAction.CallbackContext> OnCannonTurn;


    public void OnMove(InputAction.CallbackContext context)
    {
        OnCollectorMove?.Invoke(context);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        OnCannonShoot?.Invoke(context);
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        OnCannonTurn?.Invoke(context);
    }

}
