using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy : Enemy
{
    [SerializeField]
    float attackCD;
    float currentAtkCD;

    [SerializeField]
    GameObject hitBox;

    [SerializeField]
    int damage;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        hitBox.GetComponent<HitBox>().damage = 20;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        currentAtkCD += Time.deltaTime;
    }
    public override void Die()
    {
        base.Die();
    }
    private void OnTriggerStay(Collider other)
    {
        if (currentAtkCD > attackCD)
        {
            currentAtkCD = 0;
            Attack();
        }
    }
    public void Attack()
    {
        StartCoroutine(Atk());
    }
    public IEnumerator Atk()
    {
        hitBox.SetActive(true);
        chase = false;
        yield return new WaitForSeconds(1f);
        chase = true;
        hitBox.SetActive(false);
    }
}
