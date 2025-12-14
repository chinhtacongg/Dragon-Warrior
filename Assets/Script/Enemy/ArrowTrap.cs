using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireArrows;
    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;


     private float coolDownTimer;

    private void Attack()
    {
        SoundManager.instance.PlaySound(arrowSound);
        coolDownTimer = 0;
        fireArrows[FindArrow()].transform.position = firePoint.position;
        fireArrows[FindArrow()].GetComponent<Enemy_Arrow>().ActivateArrow();
    }

    private int FindArrow()
    {
        for (int i = 0; i < fireArrows.Length; i++)
        {
            if (!fireArrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }



    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime;
        if (coolDownTimer >= attackCoolDown)
            Attack();
    }
}
