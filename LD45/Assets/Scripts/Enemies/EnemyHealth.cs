using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int energyDropValue;
    [SerializeField] private int health;

    [SerializeField] private GameObject itemDropPrefab;

    private EnemyHandler enemyHandler;

    [SerializeField] private GameObject hitEffectPrefab, deathEffectPrefab;

    void Start()
    {
        enemyHandler = gameObject.GetComponent<EnemyHandler>();
        //health = enemyHandler.enemyType.health;
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

            //Drop item
            var item = Instantiate(itemDropPrefab, transform.position, transform.rotation);
            item.GetComponent<ItemPickup>().value = energyDropValue;

            GameHandler.AddSceenShake(8, 8, 0.2f);

            Destroy(gameObject);
        }
        else
        {
            GameHandler.AddSceenShake(4, 4, 0.2f);

            var effect = Instantiate(hitEffectPrefab, transform.position, transform.rotation);
            effect.GetComponent<ParticleSystem>().startColor = enemyHandler.enemyType.color;
            Destroy(effect, 5f);
        }

    }
}
