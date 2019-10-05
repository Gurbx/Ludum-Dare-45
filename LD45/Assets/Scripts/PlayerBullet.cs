using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;


public class PlayerBullet : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem particles;
    [SerializeField] Light2D light;

    public void Initialize(Color color)
    {
        spriteRenderer.color = color;
        particles.startColor = color;
        light.color = color;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        particles.transform.parent = null;
        Destroy(gameObject);
    }
}
