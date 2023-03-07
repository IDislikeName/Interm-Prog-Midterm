using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject deflectBox;
    public GameObject spell;

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
                    currentCD = coolDown;
                    StartCoroutine(Deflect());
                }
            }
            else
            {
                Launch();
            }
        }
        currentCD += Time.deltaTime;
        if (spell != null)
            spell.transform.position = transform.position + transform.forward * 2f;
    }
    IEnumerator Deflect()
    {
        deflectBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        deflectBox.SetActive(false);
    }
    public void Launch()
    {
        spell.transform.forward = transform.forward;
        spell.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
        spell.GetComponent<Enemy_projectile>().deflected = true;
        spell = null;
    }
}
