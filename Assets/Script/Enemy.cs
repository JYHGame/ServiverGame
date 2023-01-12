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
    private void OnHit(int dmg) 
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
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isLive)
        {
            if (!isInv)
            {
                if (collision.gameObject.tag == "Weapon")
                {
                    isInv = true;
                    Invoke("InvCount", 3f);
                    Debug.Log("¸ÂÀ½!");
                    Weapon weapon = collision.gameObject.GetComponent<Weapon>();
                    OnHit(weapon.dmg);
                }
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
