using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int dmg;
    public float maxShootDelay;
    public float curShootDelay;

    public ObjectManager objectManager;
    public Transform firePos;
    public GameObject enemy;

    public float shortDis;

    public List<GameObject> FoundObjects;
    void Start()
    {

    }

    private void Update()
    {
        if(gameObject.name == "Gun")
        {
            FindEnemy();
            Fire();
            Reroad();
        }
    }
    void Fire()
    {
        if (curShootDelay < maxShootDelay)
            return;
        GameObject bullet = objectManager.MakeObj("BulletS");
        bullet.transform.position = firePos.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        if (enemy)
        {
            Vector3 dirVec = enemy.transform.position - firePos.position;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else
            rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);

        curShootDelay = 0;
    }

    private void Reroad()
    {
        curShootDelay += Time.deltaTime;
    }
    void FindEnemy()
    {
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        if (FoundObjects.Count == 0)
            return;
        enemy = FoundObjects[0];
        shortDis = Vector3.Distance(gameObject.transform.position, enemy.transform.position);

        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                enemy = found;
                shortDis = Distance;
            }
        }
        Debug.Log(enemy);

    }
}
