using System;
// using System.Numerics;
using NUnit.Framework.Internal;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class RaccoonController : MonoBehaviour
{
    GameManager gm;
    private UIManager uI;
    private InputActionAsset actions;
    private InputAction xAxis;
    private float speed = 3f;
    private float jumpForce = 5f;
    private bool isJumping = false;
    private int numberOfJumps = 2;
    private bool isCrouching = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D coll;
    float tmpSpeed = 0 ;
    Vector3 startPos;
    void OnEnable()
    {
        actions.FindActionMap("Raccoon").Enable();
        actions.FindActionMap("Raccoon").FindAction("Jump").performed += OnJump;
        actions.FindActionMap("Raccoon").FindAction("Crouch").performed += OnCrouch;
        actions.FindActionMap("Raccoon").FindAction("Crouch").canceled += OnStandUp;
    }
    void OnDisable()
    {
        actions.FindActionMap("Raccoon").Disable();
        actions.FindActionMap("Raccoon").FindAction("Jump").performed -= OnJump;
        actions.FindActionMap("Raccoon").FindAction("Crouch").performed -= OnCrouch;
        actions.FindActionMap("Raccoon").FindAction("Crouch").canceled -= OnStandUp;
    }

    public void Initialize(GameManager gm, InputActionAsset actions, float playerSpeed, float jumpForce)
    {
        this.gm = gm;
        this.actions = actions;
        this.speed = playerSpeed;
        this.jumpForce = jumpForce;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        startPos = this.transform.position;
        xAxis = actions.FindActionMap("Raccoon").FindAction("MoveX");
    }
    public void Process()
    {
        MoveX();
        if (isJumping)
        {

            Vector3 origin = transform.position + Vector3.down * 0.9f;
            Vector3 direction = Vector3.down * 2f;
            RaycastHit2D belowHit = Physics2D.Raycast(origin, direction, .5f);
            Debug.DrawRay(origin, direction, Color.magenta);

            if (belowHit.collider != null)
            // if (rb.linearVelocityY < 0)
            {
                isJumping = false;
                animator.SetBool("on jump", false);
            }
        }
    }

    private void MoveX()
    {
        spriteRenderer.flipX = xAxis.ReadValue<float>() < 0;

        if (isCrouching) return;
        // Debug.Log(speed + tmpSpeed);
        // transform.Translate(xAxis.ReadValue<float>() * (speed + tmpSpeed) * Time.deltaTime, 0f, 0f);
        transform.position += Vector3.right * ((xAxis.ReadValue<float>() * speed) + tmpSpeed) * Time.deltaTime;
        animator.SetFloat("speed", Math.Abs(xAxis.ReadValue<float>()));
    }
    private void OnJump(InputAction.CallbackContext callbackContext)
    {
        animator.SetBool("on jump", true);
        isJumping = true;

        if (numberOfJumps <= 0) return;

        if (numberOfJumps > 1) rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        if (numberOfJumps <= 1) rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        numberOfJumps--;

    }
    private void OnCrouch(InputAction.CallbackContext callbackContext)
    {
        animator.SetBool("on crouch", true);
        isCrouching = true;
    }
    private void OnStandUp(InputAction.CallbackContext callbackContext)
    {
        animator.SetBool("on crouch", false);
        isCrouching = false;
    }

    public void Restart()
    {
        this.transform.position = startPos;
    }
    public void CaughtAChestnut()
    {
        gm.IncreaseCounter();
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plateform" || collision.gameObject.tag == "Moving plateform")
        {
            numberOfJumps = 2;
            // animator.SetBool("on fall", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
        {
            gm.AddOneFall();
            Restart();
        }
        if (collision.CompareTag("Finish"))
        {
            gm.Finish();
        }
        if (collision.CompareTag("Chestnut"))
        {
            CaughtAChestnut();
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Moving plateform"))
        {
            tmpSpeed = collision.GetComponentInParent<MovingPlateformBehavior>().GetSpeed();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Moving plateform"))
        {
            tmpSpeed = 0;
        }
    }

}
