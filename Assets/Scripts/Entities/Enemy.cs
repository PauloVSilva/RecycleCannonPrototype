using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Target {wall, collector}

public class Enemy : Entity, IPooledObjects
{
    private Inventory inventory;

    public TrashType trashType;
    public Target target;
    public float movementSpeed;

    public float distanceToCollector;
    private GameObject collector;
    public float distanceToWall;
    private GameObject wall;

    public bool canAttack;
    public float attackCooldown;
    public float attackRechargingTime;

    public float dropItemCooldown;
    public float dropItemRechargingTime;

    public bool instakill = false;
    
    protected override void Start()
    {
        base.Start();
        target = Target.wall;

        inventory = GetComponent<Inventory>();

        //collector = GameObject.FindWithTag("Player");
        collector = GameManager.instance.collector.gameObject;
        if(collector == null)
        {
            Debug.LogWarning("No active trash collector was found");
        }

        wall = GameManager.instance.wall.gameObject;
        if (wall == null)
        {
            Debug.LogWarning("No active wall was found");
        }


        attackRechargingTime = attackCooldown;
        dropItemRechargingTime = dropItemCooldown;
    }

    public void OnObjectSpawn()
    {
        target = Target.wall;
        isPooled = true;
        attackRechargingTime = attackCooldown;
        dropItemRechargingTime = dropItemCooldown;
    }

    protected void FixedUpdate()
    {
        TargetSetter();
        AttackBehaviour();
        MovementBehaviour();

        if (instakill)
        {
            Die();
        }
    }

    private void MovementBehaviour()
    {
        if (canAttack)
        {
            if (target == Target.wall)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                //transform.position += transform.forward * movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, wall.transform.position, movementSpeed * Time.deltaTime);
            }
            else if (target == Target.collector)
            {
                transform.position = Vector3.MoveTowards(transform.position, collector.transform.position, movementSpeed * Time.deltaTime);
            }

            dropItemRechargingTime = Math.Max(dropItemRechargingTime -= Time.deltaTime, 0);
            if(dropItemRechargingTime <= 0)
            {
                inventory.DropItem(0, this.transform);
                dropItemRechargingTime = dropItemCooldown;
            }
        }
    }

    private void TargetSetter()
    {
        distanceToCollector = Vector3.Distance(this.transform.position, collector.transform.position);
        distanceToWall = Vector3.Distance(this.transform.position, wall.transform.position);

        if (distanceToCollector <= 5)
        {
            target = Target.collector;
        }        
        else if(distanceToCollector > 5)
        {
            target = Target.wall;
        }
    }

    private void AttackBehaviour()
    {
        attackRechargingTime = Math.Max(attackRechargingTime -= Time.deltaTime, 0);

        if(attackRechargingTime <= 0)
        {
            canAttack = true;
        }

        if (canAttack && distanceToCollector < 1.5)
        {
            collector.GetComponent<Collector>().TakeDamage();

            attackRechargingTime = attackCooldown;

            canAttack = false;
        }

        if (canAttack && distanceToWall < 2)
        {
            wall.GetComponent<Wall>().TakeDamage();

            attackRechargingTime = attackCooldown;

            canAttack = false;
        }
    }

    protected override void Die()
    {
        inventory.DropItem(0, this.transform);
        base.Despawn();
    }


}
