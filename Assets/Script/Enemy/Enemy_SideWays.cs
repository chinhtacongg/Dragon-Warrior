using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SideWays : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    bool movingRight;


    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }


    private void Update()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightBound)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if(transform.position.x <= leftBound)
            {
                movingRight =true;
            }
        }



    }


}
