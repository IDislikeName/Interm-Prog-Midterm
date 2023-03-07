using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchProjectile : MonoBehaviour
{
    [SerializeField]
    Hand hand;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            hand.spell = other.gameObject;
            other.GetComponent<Enemy_projectile>().StopAllCoroutines();
        }
    }
}
