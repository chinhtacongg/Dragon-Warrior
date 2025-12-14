using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Arrow : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private Animator anim;
    private float lifeTime;
    private bool hit;
    private BoxCollider2D coll;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hit) return; 
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(moveSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if (lifeTime > resetTime) 
        {
            gameObject.SetActive(false);
        }

    }

    public void ActivateArrow()
    {
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision); // giong super().parentMethod()
        coll.enabled = false;

        if (anim != null)
            anim.SetTrigger("explode"); // when the object is a fireball explode it
        else
            gameObject.SetActive(false); // when this hits any object deactivate arrow
    }

    private void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
