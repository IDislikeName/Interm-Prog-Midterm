using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected NavMeshAgent nav;
    [SerializeField]
    protected float checkTime;
    protected float currentTime = 0;

    protected bool chase;

    [SerializeField]
    protected int maxHP;
    public int currentHP;
    protected bool dead;


    // Start is called before the first frame update
    public virtual void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        currentHP = maxHP;
        chase = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (currentTime > checkTime&&chase)
        {
            currentTime = 0;
            nav.SetDestination(GameManager.instance.playerTrans.position);
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        if (currentHP <= 0)
        {
            Die();
        }
        
    }
    public virtual void Die()
    {
        if (!dead)
        {
            dead = true;
            GameManager.instance.score++;
            Destroy(gameObject);
        }
        
    }
}
