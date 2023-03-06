using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_laser : Weapon
{
    [SerializeField]
    float altShootCD;
    float altCurrentCD;
    [SerializeField]
    float altFireHoldTime;
    [SerializeField]
    int altDamage;
    float timeHeld = 0f;
    bool charging = false;

    [SerializeField]
    MeshRenderer rend;
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    Color normalColor;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        gradient = new Gradient();
        colorKey = new GradientColorKey[2];
        normalColor = rend.material.color;
        colorKey[0].color = normalColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;
        gradient.SetKeys(colorKey,alphaKey);
    }

    // Update is called once per frame
    public override void Update()
    {
        lr.SetPosition(0, launchPoint.position);
        
        
        
        if (Input.GetMouseButton(1))
        {
            if(altCurrentCD >= altShootCD)
            {
                charging = true;
                timeHeld += Time.deltaTime;
                rend.material.color = gradient.Evaluate(timeHeld / altFireHoldTime);
            }
            
            
        }
        if (Input.GetMouseButtonDown(0)&&!charging)
        {
            if (currentCD >= shootCD)
            {
                currentCD = 0;
                Shoot();

            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            rend.material.color = normalColor;
            charging = false;   
            if(timeHeld > altFireHoldTime && altCurrentCD >= altShootCD)
            {
                altShoot();
                altCurrentCD = 0;
            }
            timeHeld = 0;
        }
        currentCD += Time.deltaTime;
        altCurrentCD += Time.deltaTime;
        if (timeHeld > altFireHoldTime)
        {

        }
    }

    public override void Shoot()
    {
        base.Shoot();
    }
    public void altShoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            lr.SetPosition(1, hit.point);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().currentHP -= altDamage;
            }

        }
        else
        {
            lr.SetPosition(1, ray.GetPoint(range));
        }
        StartCoroutine(ChargeShot());
    }
    public IEnumerator ChargeShot()
    {
        Color baseColor = lr.startColor;
        lr.enabled = true;
        lr.startWidth = 0.3f;
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        yield return new WaitForSeconds(0.3f);
        lr.startWidth = 0.1f;
        lr.startColor = baseColor;
        lr.endColor = baseColor;
        lr.enabled = false;
    }
}
