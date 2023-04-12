using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // big chungus
    [SerializeField] int health = 50;
    [SerializeField] float flashLength = 0.025f;

    [SerializeField] GameObject deathEffect;

    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    float flashTimer;
    Material flash;

    CameraShake shake;

    [SerializeField] float hitShakeAmount;
    [SerializeField] float deathShakeAmount;
    [SerializeField] float ShakeDecay;
    void Start()
    {
        flash = GetComponentInChildren<SpriteRenderer>().material;
        shake = FindObjectOfType<CameraShake>();
    }

    void Update()
    {
        if (flashTimer > 0) flash.SetFloat("_FlashAmount", 1);
        else flash.SetFloat("_FlashAmount", 0);

        flashTimer -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }

    }

    void DeathStuff()
    {
        if (deathEffect != null) Instantiate(deathEffect, transform.position, Quaternion.identity);
        if (deathSound != null) AudioSource.PlayClipAtPoint(deathSound, transform.position);
        shake.setShake(deathShakeAmount, ShakeDecay);
        Destroy(gameObject);
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        flashTimer = flashLength;
        if (hitSound != null) AudioSource.PlayClipAtPoint(hitSound, transform.position);
        shake.setShake(hitShakeAmount, ShakeDecay);
        if (health <= 0)
        {
            DeathStuff();
        }
    }



}
