using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerShootingHandler>().DecreasePowerLevel(damage, true);
        }


        var effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 3f);

        Destroy(gameObject);
    }
}
