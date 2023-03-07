using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy1;
    public GameObject enemy2;

    [SerializeField]
    float spawnCD;
    float currentCD;

    int enemySpawnNum;

    // Start is called before the first frame update
    void Start()
    {
        spawnCD = 7f;
        enemySpawnNum = 1;
        SpawnEnemy(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCD > spawnCD)
        {
            currentCD = 0;
            SpawnEnemy(enemySpawnNum);
        }
        else
        {
            currentCD += Time.deltaTime;
        }
        if (GameManager.instance.score >= 50)
        {
            enemySpawnNum = 5;
            spawnCD = 3f;
        }
        if (GameManager.instance.score >= 30)
        {
            enemySpawnNum = 3;
        }
        else if (GameManager.instance.score >= 20)
        {
            spawnCD = 4f;
        }
        else if (GameManager.instance.score >= 15)
        {
            spawnCD = 5f;
        }
        else if (GameManager.instance.score >= 5)
        {
            enemySpawnNum = 2;
        }
        
        

    }
    public void SpawnEnemy(int num)
    {
        for(int i = 0; i < num; i++)
        {
            GameObject e;
            if (Random.Range(0, 3) == 1)
            {
                e = Instantiate(enemy2);
            }
            else
            {
                e = Instantiate(enemy1);
            }
            e.transform.position = new Vector3(Random.Range(-13f, 35f), 2, Random.Range(-27f, 23f));
        }
               
    }
}
