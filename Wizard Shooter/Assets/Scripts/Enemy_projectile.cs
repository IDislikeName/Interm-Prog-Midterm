using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_projectile : MonoBehaviour
{
    public int damage;
    public bool deflected = false;
    private void Start()
    {
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.hp -= damage;
            Destroy(gameObject);
        }
        if(deflected&& other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().currentHP -= damage;
            Destroy(gameObject);
        }
    }
}
