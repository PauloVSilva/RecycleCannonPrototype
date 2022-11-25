using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    private Inventory inventory;
    private InputHandler inputHandler;

    private CharacterController controller;
    [SerializeField]
    private Vector3 move;

    public float movementSpeed;

    public Transform castPoint;
    
    public ProjectileScriptableObject projectileToCast;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
        inputHandler = GetComponent<InputHandler>();

        inputHandler.OnCannonShoot += OnShoot;
        inputHandler.OnCannonTurn += OnTurn;

        projectileToCast = (ProjectileScriptableObject)inventory.GetItemOnSlot(0);
    }

    public void SelectAmmoType(int index)
    {
        projectileToCast = (ProjectileScriptableObject)inventory.GetItemOnSlot(index);
    }

    public void Fire()
    {
        if (ObjectPooler.instance.SpawnFromPool(projectileToCast.itemModel, castPoint.position, castPoint.rotation) == null)
        {
            Debug.LogWarning("Something went wrong. Object Pooler couldn't Spawn " + projectileToCast.itemModel);
        }
    }

    private void FixedUpdate()
    {
        if (move != Vector3.zero)
        {
            //gameObject.transform.forward = move;
        }
        controller.Move(move * Time.deltaTime * movementSpeed); //player input - horizontal movement
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Fire();
        }
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, 0);
    }


}
