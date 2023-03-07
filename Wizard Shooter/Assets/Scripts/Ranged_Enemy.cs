using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Enemy : Enemy
{

    [SerializeField]
    float attackCD;
    float currentAtkCD;

    [SerializeField]
    int damage;

    [SerializeField]
    GameObject projectile;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Vector3.Distance(transform.position,GameManager.instance.playerTrans.position)<=15f)
        {
            chase = false;

            if (currentAtkCD > attackCD)
            {
                currentAtkCD = 0;
                Attack();
            }
        }
        else
        {
            chase = true;
        }
        base.Update();
        currentAtkCD += Time.deltaTime;
    }
    public override void Die()
    {
        base.Die();
    }
    public void Attack()
    {
        StartCoroutine(Atk());
    }
    public IEnumerator Atk()
    {
        GameObject p = Instantiate(projectile);
        p.transform.position = transform.position;
        p.transform.LookAt(GameManager.instance.playerTrans);
        p.GetComponent<Rigidbody>().velocity = p.transform.forward * 10f;
        p.GetComponent<Enemy_projectile>().damage = damage;
        yield return new WaitForSeconds(1f);
    }
}
