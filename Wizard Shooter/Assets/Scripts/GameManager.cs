using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public int score;
    public GameObject loseScreen;
    public TMP_Text scoreText;
    public bool lost = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.rectTransform.localScale = new Vector3(hp, 1, 1);
        manaBar.rectTransform.localScale = new Vector3(mana, 1, 1);

        scoreText.text = "Score: " + score;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    private void FixedUpdate()
    {
        mana += 0.1f;
        mana = Mathf.Min(100, mana);
    }
    public void Lose()
    {
        lost = true;
        loseScreen.SetActive(true);
    }
}
