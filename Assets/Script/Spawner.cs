using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    public ObjectManager objectManager;
    public GameObject Player;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),spawnData.Length-1);
        if (timer > spawnData[level].spawnTime)
        {
            Debug.Log(level);
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
       GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.objectManager = objectManager;
        enemyLogic.player = Player;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}
[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}