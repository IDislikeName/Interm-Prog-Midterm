using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public int hp;
    public float mana;
    [SerializeField]
    Image hpBar;
    [SerializeField]
    Image manaBar;
    public Transform playerTrans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.rectTransform.localScale = new Vector3(hp, 1, 1);
        manaBar.rectTransform.localScale = new Vector3(mana, 1, 1);
    }
    private void FixedUpdate()
    {
        mana += 0.1f;
        mana = Mathf.Min(100, mana);
    }
}
