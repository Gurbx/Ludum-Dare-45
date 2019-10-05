using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health;

    private EnemyHandler enemyHandler;

    [SerializeField] private GameObject hitEffectPrefab, deathEffectPrefab;

    void Start()
    {
        enemyHandler = gameObject.GetComponent<EnemyHandler>();
        health = enemyHandler.enemyType.health;
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            var effect = Instantiate(deathEffectPrefab, transform.position, transform.rotation);
            effect.GetComponent<ParticleSystem>().startColor = enemyHandler.enemyType.color;
            Destroy(effect, 5f);

            Destroy(gameObject);
        }
        else
        {
            var effect = Instantiate(hitEffectPrefab, transform.position, transform.rotation);
            effect.GetComponent<ParticleSystem>().startColor = enemyHandler.enemyType.color;
            Destroy(effect, 5f);
        }

    }
}
