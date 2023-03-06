using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().currentHP -= damage;
        }
    }
    IEnumerator Destruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
