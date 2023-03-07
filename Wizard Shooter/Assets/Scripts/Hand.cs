using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject deflectBox;
    public GameObject spell;

    [SerializeField]
    Transform hand;
    bool moving;
    [SerializeField]
    float coolDown;
    float currentCD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (spell == null)
            {
                if(currentCD >= coolDown)
                {
                    currentCD = 0;
                    StartCoroutine(Deflect());
                }
            }
            else
            {
                moving = false;
                Launch();
            }
        }
        currentCD += Time.deltaTime;
        if (spell != null)
        {
            spell.transform.position = transform.position + transform.forward * 0.5f;
            moving = true;
        }
            

        if (moving)
        {
            hand.localPosition = Vector3.MoveTowards(hand.localPosition, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
        }
        else
        {
            hand.localPosition = Vector3.MoveTowards(hand.localPosition,new Vector3(-0.3f,0f,0f),Time.deltaTime*10f);
        }
    }
    IEnumerator Deflect()
    {
        moving = true;
        deflectBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        deflectBox.SetActive(false);
        moving = false;
    }
    public void Launch()
    {
        spell.transform.forward = transform.forward;
        spell.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
        spell.GetComponent<Enemy_projectile>().deflected = true;
        spell = null;
    }
}
