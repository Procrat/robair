﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float feetWidth;

    public GameObject feet;
    public LayerMask whatIsGround;

    private Rigidbody2D body;
    private PlayerInput playerInput;

    void Start ()
    {
        body = GetComponent<Rigidbody2D> ();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove () {
        MoveLeft();
        print(playerInput);
    }

    void FixedUpdate ()
    {
        // HandleInput ();
    }

    private void HandleInput ()
    {
        if (Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.Q)) {
            Debug.Log ("Quitting.");
            Application.Quit ();
        }

        if (Input.GetKey (KeyCode.RightArrow)) {
            MoveRight ();
        } else if (Input.GetKey (KeyCode.LeftArrow)) {
            MoveLeft ();
        }

        if (Input.GetKey (KeyCode.UpArrow)) {
            Jump ();
        }
    }

    private void MoveLeft ()
    {
        Move (Vector2.left);
    }

    private void MoveRight ()
    {
        Move (Vector2.right);
    }

    private void Move (Vector2 translationVector)
    {
        transform.Translate (moveSpeed * translationVector);
    }

    private void Jump ()
    {
        if (isGrounded ()) {
            body.velocity = jumpSpeed * Vector2.up;
        }
    }

    private bool isGrounded ()
    {
        var feetRect = new Rect (feet.transform.position.x, feet.transform.position.y, feetWidth, 0.1f);
        return Physics2D.OverlapArea (feetRect.min, feetRect.max, whatIsGround);
    }
}
