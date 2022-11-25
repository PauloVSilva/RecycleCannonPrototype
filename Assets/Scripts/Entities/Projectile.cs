using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity, IPooledObjects
{
    public ItemScriptableObject item;

    public TrashType trashType;

    [SerializeField]
    private SphereCollider myCollider;
    [SerializeField]
    private Rigidbody myRigidbody;


    // Start is called before the first frame update
    protected override void Start()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myRigidbody = GetComponent<Rigidbody>();
        age = 0;
        //isPooled = false;
    }

    public void OnObjectSpawn()
    {
        Debug.LogWarning("OnObjectSpawn()");
        age = 0;
        isPooled = true;

        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;
        myRigidbody.AddForce(transform.forward * 30, ForceMode.Impulse);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            if(trashType == TrashType.organic && other.gameObject.GetComponent<Enemy>().trashType != TrashType.organic)
            {
                other.GetComponent<Enemy>().TakeDamage(5);
            }

            if(trashType != TrashType.organic && other.gameObject.GetComponent<Enemy>().trashType == TrashType.organic)
            {
                other.GetComponent<Enemy>().TakeDamage(5);
            }

            Despawn();
        }
    }
}
