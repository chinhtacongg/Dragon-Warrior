
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private AudioClip fireballSound;
    private Animator anim;
    private PlayerMoveMent playerMoveMent;
    private float coolDownTimer = Mathf.Infinity;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMoveMent = GetComponent<PlayerMoveMent>();
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && coolDownTimer > attackCoolDown && playerMoveMent.CanAttack())
        {
            Attack();
        }
        coolDownTimer += Time.deltaTime;

    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        coolDownTimer = 0;

        fireBalls[FineFireBall()].transform.position = firePoint.position;
        fireBalls[FineFireBall()].GetComponent<ProjectTile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FineFireBall()
    {
        for(int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

}
