using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private ParticleSystem particles;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerShootingHandler>().DecreasePowerLevel(damage, true);
            GameHandler.AddSceenShake(7, 7, 0.35f);
        }


        particles.transform.parent = null;
        particles.Stop();

        Destroy(particles, 1f);

        var effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 3f);

        Destroy(gameObject);
    }
}
