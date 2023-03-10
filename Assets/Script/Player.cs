using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;
    public int speed;
    public float health;
    public int maxHealth;
    public int exp;
    public int killCount;


    public Vector2 inputVec;
    public Transform hp;

    SpriteRenderer sprite;
    public SpriteRenderer weapon;
    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp.localScale = new Vector3(health/100,1,1);
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized *speed *Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
            weapon.transform.localScale = new Vector3(1, 1, 1);
            if (inputVec.x < 0)
                weapon.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Exp":
                    exp = exp + item.exp;
                    CountLevel();
                    break;
                case "Health":
                    health = health + item.health;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;
                case "Mag":
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }

    void CountLevel()
    {
        if (exp > 5 * level)
        {
            level += 1;
            exp = 0;
        }
    }
}
