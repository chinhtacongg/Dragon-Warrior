using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [Header ("Melee Attack")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    private EnemyPatrol enemyPatrol;
    private float coolDownTimer = Mathf.Infinity;

    [Header ("Melee Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private Animator anim;
    private Health playerHealth;

    [SerializeField] private AudioClip hitSound;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime;
        if (PlayerInsight())
        {
            if (coolDownTimer > attackCoolDown && playerHealth.currentHealth > 0)
            {
                coolDownTimer = 0;
                anim.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(hitSound);
            }
        }
        if (enemyPatrol != null) 
        {
            enemyPatrol.enabled = !PlayerInsight();
        }
    }
    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * range * colliderDistance, 
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                            0, Vector2.left, 0, playerLayer);
        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * range * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)); 
    }

    private void DamagePlayer()
    {
        if (PlayerInsight())
        {
            playerHealth.TakeDamage(damage);
        }
    }




}
