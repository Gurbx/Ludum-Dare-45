﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rigidbody;
    private Vector2 moveVelocity = Vector2.zero;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        moveVelocity.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveVelocity.magnitude > 1) moveVelocity.Normalize();

        moveVelocity *= speed;

    }

    private void FixedUpdate()
    {
        rigidbody.velocity = moveVelocity;
    }
}