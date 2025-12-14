using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;


    [Header ("Moving Parameters")]
    [SerializeField] private float speed;

    [Header("Enemy Behaviour")]
    [SerializeField] private Animator animator;
    [SerializeField] private float idleDuration;
    private float idleTimer;


    private Vector3 initScale;
    private bool movingLeft;

    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }

    private void Start()
    {
        initScale = enemy.localScale;
        
    }
    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }

    }

    private void DirectionChange()
    {
        animator.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;

    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        animator.SetBool("moving", true);
        // make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, 
            initScale.y, initScale.z);

        // enemy move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * speed * _direction, enemy.position.y, enemy.position.z);
    }


}
