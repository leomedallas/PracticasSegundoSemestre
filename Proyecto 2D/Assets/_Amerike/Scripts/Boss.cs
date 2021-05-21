using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Stats")]
    public int hitPoints;
    public float speed;
    public float life;
    [Header("Components")]
    public Rigidbody2D rbody;
    public SpriteRenderer sprRenderer;
    [Header("Raycast")]
    public float offset;
    public float raycastLenght;
    public LayerMask groundLayer;

    void Update()
    {
        CheckForLimits();
    }

    void FixedUpdate()
    {
        rbody.velocity = Vector2.right * speed;
    }

    void CheckForLimits()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right * offset, Vector2.down, raycastLenght, groundLayer);
        if (hit.collider == null)
        {
            //No colisiono
            offset *= -1;
            speed *= -1;
            sprRenderer.flipX = !sprRenderer.flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            life = life - 1;
            if (life == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}