using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;


public class PlayerBullet : MonoBehaviour
{

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem particles;
    [SerializeField] Light2D light;

    [SerializeField] GameObject hitEffectPrefab;

    private int damage;

    public void Initialize(Color color, int damage)
    {
        spriteRenderer.color = color;
        particles.startColor = color;
        light.color = color;
        this.damage = damage;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        }

        var effect = Instantiate(hitEffectPrefab, transform.position, transform.rotation);
      //  effect.GetComponent<ParticleSystem>().startColor = enemyHandler.enemyType.color;
        Destroy(effect, 2f);

        particles.transform.parent = null;
        particles.Stop();

        Destroy(particles, 1f);
        Destroy(gameObject);
    }
}
