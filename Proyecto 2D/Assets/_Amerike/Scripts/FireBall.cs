using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Components")]
    public Animator animac;
    public Rigidbody2D rbody;
    public SpriteRenderer sprRenderer;
    [Header("Movement")]
    public float speed;

    void Update()
    {
        Movement();
    }
    void Movement()
    {
        rbody.velocity = Vector2.right * speed;
        if (rbody.velocity.x < 0)
        {
            sprRenderer.flipX = true;
        }
        else
        {
            sprRenderer.flipX = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }
}
