using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rbody;
    public Vector3 movementSpeed;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rbody.velocity += movementSpeed.y * Vector3.up;
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            rbody.velocity = transform.forward * movementSpeed.z * Input.GetAxis("Vertical") + transform.right * movementSpeed.x * Input.GetAxis("Horizontal") + Vector3.up * rbody.velocity.y;
            animator.SetBool("Moving", true);
        }
        else
        {
            rbody.velocity = Vector3.up * rbody.velocity.y;
            animator.SetBool("Moving", false);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("Punch", true);
        }
        else
        {
            animator.SetBool("Punch", false);
        }
    }

    private void LateUpdate()
    {
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        }
    }
}
