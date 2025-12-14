using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float value;
    [Header("SFX")]
    [SerializeField] private AudioClip healthCollect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(healthCollect);
            collision.GetComponent<Health>().AddHealth(value);
            gameObject.SetActive(false);
        }
    }
}
