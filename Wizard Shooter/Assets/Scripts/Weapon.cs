using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Camera cam;
    public GameObject projectile;
    
    [SerializeField]
    protected float shootCD;
    protected float currentCD;
    [SerializeField]
    protected Transform launchPoint;
    [SerializeField]
    protected float range;

    [SerializeField]
    protected ProjectileStats proj;


    protected LineRenderer lr;

    public AudioClip fireSound;
    public enum WeaponType
    {
        Hitscan,
        Projectile,
    }
    public WeaponType type;
    [System.Serializable]
    public struct ProjectileStats
    {
        public int damage;
        public float speed;
        public ProjectileStats(int dmg,float spd)
        {
            damage = dmg;
            speed = spd;
        }
    }

    public virtual void Start()
    {
        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
    }

    public virtual void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (currentCD >= shootCD)
            {
                currentCD = 0;
                Shoot();

            }
        }
        currentCD += Time.deltaTime;
        lr.SetPosition(0, launchPoint.position);

    }
    public virtual void Shoot()
    {
        if (type == WeaponType.Hitscan)
        {
            
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, range))
            {
                lr.SetPosition(1,hit.point);
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<Enemy>().currentHP -= proj.damage;
                }
                
            }
            else
            {
                lr.SetPosition(1, ray.GetPoint(range));
            }
            StartCoroutine(Hitscan());
        }
        else
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 destination = Vector3.zero;
            if (Physics.Raycast(ray, out hit))
            {
                destination = hit.point;
            }
            else
            {
                destination = ray.GetPoint(range);
            }
            GameObject p = Instantiate(projectile);
            p.GetComponent<Rigidbody>().velocity = (destination - launchPoint.position).normalized * proj.speed;
            p.GetComponent<Projectile>().damage = proj.damage;
            p.transform.rotation = transform.rotation;
            p.transform.position = launchPoint.position;
        }
        
        
    }
    IEnumerator Hitscan()
    {
        lr.enabled = true;
        yield return new WaitForSeconds(0.3f);
        lr.enabled = false;
    }

}
