using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PlayerShootingHandler shootingHandler;

    public bool canMove;

    private Rigidbody2D rigidbody;
    private Vector2 moveVelocity = Vector2.zero;

    void Start()
    {
        canMove = false;
        rigidbody = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        if (!canMove) return;
        moveVelocity.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveVelocity.magnitude > 1) moveVelocity.Normalize();

        moveVelocity *= speed * (1-shootingHandler.GetPowerPercentage()) + 3;

    }

    private void FixedUpdate()
    {
        rigidbody.velocity = moveVelocity;
    }
}
