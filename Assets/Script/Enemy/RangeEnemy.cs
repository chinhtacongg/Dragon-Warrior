using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [Header("Melee Attack")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    private EnemyPatrol enemyPatrol;
    private float coolDownTimer = Mathf.Infinity;

    [Header("Melee Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private Animator anim;

    [Header("Sound")]
    [SerializeField] private AudioClip fireballSound;


    private void Awake()
    {
       
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime;
        if (PlayerInsight())
        {
            if (coolDownTimer > attackCoolDown)
            {
                coolDownTimer = 0;
                anim.SetTrigger("rangedAttack");
                SoundManager.instance.PlaySound(fireballSound);
            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInsight();
        }
    }

    private void RangedAttack()
    {
        coolDownTimer = 0;
        fireBalls[FindFireBalls()].transform.position = firePoint.position;
        fireBalls[FindFireBalls()].GetComponent<Enemy_Arrow>().ActivateArrow();

    }

    private int FindFireBalls()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * range * colliderDistance,
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                            0, Vector2.left, 0, playerLayer);
       
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * range * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

}
