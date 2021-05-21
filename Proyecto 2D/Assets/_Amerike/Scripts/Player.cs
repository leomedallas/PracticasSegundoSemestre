using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int hitPoints;
    public int HitPoints
    {
        get
        {
            return hitPoints;
        }
        set
        {
            hitPoints = value;
            CheckForDeath();
        }
    }

    [Header("Components")]
    public Animator anima;
    public Rigidbody2D rbody;
    public SpriteRenderer sprRenderer;
    public CheckpointManager checkManager;
    [Header("Movement")]
    public float movementSpeed;
    float currentSpeed;
    [Header("Jump")]
    public Transform feet;
    public float jumpForce;
    public float raycastLenght;
    public LayerMask groundLayer;
    bool willJump;
    [Header("Attack")]
    public GameObject swordCollider;
    bool isAttacking;
    [Header("Dash")]
    public bool isDashing;
    public float dashingSpeed;
    public float dashingDuration;

    private void Start()
    {
        //UIManager.instance.slHitPoints.value = 0;
        hitPoints = 3;
        if (checkManager.GetActiveCheckpoint())
        {
            transform.position = checkManager.GetActiveCheckpoint().transform.position;
        }
    }

    void Update()//Corre cada frame del juego
    {
        if (!isAttacking && !isDashing)
        {
            Inputs();
        }

    }

    private void FixedUpdate()//Como el update pero no depende de las frames
    {
        Movement();
        Jump();
    }

    void Inputs()
    {
        #region Movement
        currentSpeed = Input.GetAxis("Horizontal");
        anima.SetFloat("WalkSpeed", currentSpeed);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (currentSpeed < 0)
            {
                sprRenderer.flipX = true;
            }
            else
            {
                sprRenderer.flipX = false;
            }
        }
        #endregion

        #region Jump
        RaycastHit2D hit = Physics2D.Raycast(feet.position, Vector2.down, raycastLenght, groundLayer);
        if (hit.collider)
        {
            //Si colisiono con algo
            anima.SetBool("IsJumping", false);
            if (Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
            {
                //Salto
                willJump = true;
            }
        }
        else
        {
            //No colisiono con algo
            anima.SetBool("IsJumping", true);
            willJump = false;
        }
        #endregion

        #region Attack
        if (Input.GetKeyDown(KeyCode.V))
        {
            Attack();
        }
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.C))
        {
            Dash();
        }
        #endregion
    }
    #region funciones
    void Movement()
    {
        if (!isDashing)
        {
            rbody.velocity = currentSpeed * Vector2.right * movementSpeed + rbody.velocity.y * Vector2.up;
        }
        else
        {
            float direction = 0;
            if (!sprRenderer.flipX)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            rbody.velocity = direction * Vector2.right * dashingSpeed; //+ rbody.velocity.y * Vector2.up;
        }
        //rbody.AddForce(Vector2.right, ForceMode2D.Force);

    }

    void Dash()
    {
        anima.SetBool("IsJumping", false);
        anima.SetBool("IsDashing", true);
        isDashing = true;
        Invoke("StopDashing", dashingDuration);
    }

    void StopDashing()
    {
        anima.SetBool("IsDashing", false);
        isDashing = false;
    }

    void Jump()
    {
        if (willJump)
        {
            rbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            willJump = false;
        }
    }

    void Attack()
    {
        anima.SetBool("IsJumping", false);
        anima.SetTrigger("Attack");
        isAttacking = true;
        currentSpeed = 0;
        anima.SetFloat("WalkSpeed", currentSpeed);

        if (sprRenderer.flipX)
        {
            swordCollider.transform.localScale = new Vector3(-1, 1, -1);
        }
        else
        {
            swordCollider.transform.localScale = Vector3.one;
        }
    }

    void CheckForDeath()
    {
        if (HitPoints <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void EnableSwordCollider()
    {
        swordCollider.SetActive(true);
    }

    public void DisableSwordCollider()
    {
        swordCollider.SetActive(false);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fireball")) //Para bajarle vida por medio del proyectil al jugador y desaparezca
        {
            HitPoints--;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Potion")) //Para hacer desaparecer a la pocima y sume o reste vida al jugador
        {
            if (collision.GetComponent<Potion>())
            {
                HitPoints += collision.GetComponent<Potion>().hpRecovered;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.CompareTag("Hazard"))
        {
            HitPoints--;
            rbody.velocity = Vector2.right * rbody.velocity.x;
            rbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anima.SetBool("IsJumping", true);
        }
        else if (collision.CompareTag("Enemy"))
        {
            HitPoints--;
            rbody.velocity = Vector2.right * rbody.velocity.x;
            rbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anima.SetBool("IsJumping", true);
        }
        else if (collision.CompareTag("Finish"))
        {
            checkManager.CurrentCheckpoint = -1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    #endregion
}
