using System;
// using System.Numerics;
using NUnit.Framework.Internal;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class RaccoonController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    private InputAction xAxis;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 8f;
    private bool isJumping = false;
    private bool isCrouching = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D coll;
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
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        xAxis = actions.FindActionMap("Raccoon").FindAction("MoveX");

    }
    void Update()
    {
        MoveX();
        if (isJumping)
        {
            // changer cette partie et l'adapter à partir d'un ray casting pour arrêter l'animation du saut demanière plus fluide
        
            Vector3 origin = transform.position + Vector3.down * 0.9f;
            Vector3 direction = Vector3.down * 2f;
            RaycastHit2D belowHit = Physics2D.Raycast(origin, direction, 1.5f);
            Debug.DrawRay(origin, direction, Color.magenta);

            if (belowHit.collider != null) 
            // if (rb.linearVelocityY < 0)
            {
                isJumping = false;
                animator.SetBool("on jump", false);
            }
        }
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Enemy")
    //     {
    //         Debug.Log("You died");
    //     }
    // }


    private void MoveX()
    {
        spriteRenderer.flipX = xAxis.ReadValue<float>() < 0;

        if (isCrouching) return;

        transform.Translate(xAxis.ReadValue<float>() * speed * Time.deltaTime, 0f, 0f);
        animator.SetFloat("speed", Math.Abs(xAxis.ReadValue<float>()));
    }
    private void OnJump(InputAction.CallbackContext callbackContext)
    {
        animator.SetBool("on jump", true);
        isJumping = true;
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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

}
