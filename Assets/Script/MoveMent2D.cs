using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent2D : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 8.0f;
    private Rigidbody2D rigid2D;
    [HideInInspector]
    public bool isLongJump = false;

    [SerializeField]
    private LayerMask groundLayer;
    private CapsuleCollider2D capsuleCollider2D;
    private bool isGrounded;
    private Vector3 footPosition;

    [SerializeField]
    private int maxJumpCount = 2;
    private int currentJumpCount = 0;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        Bounds bounds =capsuleCollider2D.bounds;
        footPosition = new Vector3(bounds.center.x, bounds.min.y);
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        if(isGrounded==true&& rigid2D.velocity.y<=0)
        {
            currentJumpCount = maxJumpCount;
        }

        if (isLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = 1.0f;
        }
        else
        {
            rigid2D.gravityScale = 2.5f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
       
    }


public void Move(float x)
{
    rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
}

public void Jump()
{
        if (currentJumpCount>0)
        {
            rigid2D.velocity = Vector2.up * jumpForce;
            currentJumpCount--;
        }
}
}
