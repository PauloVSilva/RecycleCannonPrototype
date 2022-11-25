using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collector : Entity
{
    private Inventory inventory;
    private InputHandler inputHandler;

    private CharacterController controller;
    [SerializeField]
    private Vector3 move;

    public float movementSpeed;

    public float invencibilityTime;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
        inputHandler = GetComponent<InputHandler>();

        inputHandler.OnCollectorMove += OnMove;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Item>() != null && other.gameObject.GetComponent<Item>().CanBePickedUp)
        {
            if (inventory.AddToInventory(other.gameObject.GetComponent<Item>().item))
            {
                other.gameObject.GetComponent<Item>().Despawn();
            }
        }
    }

    public override void TakeDamage(int _damage)
    {
        if(invencibilityTime <= 0)
        {
            currentHealth = Math.Max(currentHealth -= _damage, 0);
            base.InvokeOnDamaged();
            invencibilityTime = 1;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if(invencibilityTime > 0)
        {
            invencibilityTime = Math.Max(invencibilityTime -= Time.deltaTime, 0);
        }
    }

    private void FixedUpdate()
    {

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        controller.Move(move * Time.deltaTime * movementSpeed); //player input - horizontal movement
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
    }
}
