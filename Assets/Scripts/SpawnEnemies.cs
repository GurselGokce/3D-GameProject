using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int enemyCount = 12;
    public int totaal = 15;
    bool coroutineStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Respawn());
    }
    void EnemyDrop()
    {
        for (int i = 0; i < 15; i++)
        {
            xPos = Random.Range(-20, 20);
            zPos = Random.Range(-3, 3);
            Instantiate(enemy, new Vector3(xPos, 3, zPos), Quaternion.identity);
            //yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
        coroutineStarted = false;
    }

    void Update()
    {
        if (!coroutineStarted)
            StartCoroutine(Respawn());
    }


    IEnumerator Respawn()
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(8f);
        EnemyDrop();
    }



}

