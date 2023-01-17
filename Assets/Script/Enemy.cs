using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public GameObject player;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    bool isInv;

    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    public ObjectManager objectManager;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }
    private void OnEnable()
    {
        target = GameManager.instance.Player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;

    }
    private void OnDamaged(int dmg) 
    {
        if (health <= 0)
            return;
        health -= dmg;
        if (health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.killCount += 1;

            if (maxHealth == 10)
            {
                GameObject ExpS = objectManager.MakeObj("ExpS");
                ExpS.transform.position = transform.position;
                gameObject.SetActive(false);
            }
            if (maxHealth == 15)
            {
                GameObject ExpM = objectManager.MakeObj("ExpM");
                ExpM.transform.position = transform.position;
                gameObject.SetActive(false);
            }
            int ranCount = Random.Range(0, 10);
            if (ranCount < 5)
            {
                Debug.Log("NotItem");
            }
            else if (ranCount < 8)
            {
                GameObject ExpL = objectManager.MakeObj("ExpL");
                ExpL.transform.position = transform.position + Vector3.right * 0.1f;
            }
            else if (ranCount < 9)
            {
                GameObject HpPortion = objectManager.MakeObj("HpPortion");
                HpPortion.transform.position = transform.position + Vector3.right * 0.1f;
            }
            else if (ranCount < 10)
            {
                GameObject Magnet = objectManager.MakeObj("Magnet");
                Magnet.transform.position = transform.position + Vector3.right * 0.1f;
            }
        }
    }

    private void OnHit(Vector2 targetPos)
    {
        Debug.Log("Hit!");
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLive)
        {
            if (!isInv)
            {
                if (collision.gameObject.tag == "Weapon")
                {
                    isInv = true;
                    Invoke("InvCount", 1f);
                    Weapon weapon = collision.gameObject.GetComponent<Weapon>();
                    OnDamaged(weapon.dmg);
                    OnHit(collision.transform.position);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLive)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                if (!isInv)
                {
                    isInv = true;
                    Invoke("InvCount", 1f);
                    Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                    OnDamaged(bullet.dmg);
                    OnHit(collision.transform.position);

                }
                collision.gameObject.SetActive(false);
            }
        }
    }

    void InvCount()
    {
        isInv = false;
    }
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}
