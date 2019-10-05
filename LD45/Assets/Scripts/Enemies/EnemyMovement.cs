using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class EnemyMovement : MonoBehaviour
{
    private EnemyHandler enemyHandler;

    private GameObject player;
    private Rigidbody2D rigidbody;
    private Vector2 moveVelocity = Vector2.zero;

    [SerializeField] private Light2D light;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyHandler = GetComponent<EnemyHandler>();

        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody2D>();

        light.color = enemyHandler.enemyType.color;
        particles.startColor = enemyHandler.enemyType.color;
        spriteRenderer.color = enemyHandler.enemyType.color;
    }

    // Update is called once per frame
    void Update()
    {
        moveVelocity = player.transform.position - transform.position;
        moveVelocity.Normalize();

        moveVelocity *= enemyHandler.enemyType.movementSpeed;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = moveVelocity;
    }
}
