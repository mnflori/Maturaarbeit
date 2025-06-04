using UnityEngine;
using Movement;
using TMPro;
using System.Collections;






public class MainCMovement : MonoBehaviour
{
    //Movement Variables
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private MCMovement controls;
   

    //Interaction Variables
    public float interactRange = 1.5f;         
    public LayerMask interactableLayerNPC;
    private Vector2 lookDirection = Vector2.right;


    //Dash Variables
    [Header("Dash Settings")]
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private bool canDash = true;




    private void Awake()
    {
        controls = new MCMovement();

        controls.Player.Move.performed += ctx =>
        {
            moveInput = ctx.ReadValue<Vector2>();
            if (moveInput != Vector2.zero)
                lookDirection = moveInput.normalized;
        };

        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Interact.performed += ctx => TryInteract();

        controls.Player.Dash.performed += ctx => TryDash();



    }


    private void OnEnable()
    {
        controls.Player.Enable();
        

    }

    private void OnDisable()
    {
        
        controls.Player.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    private void FixedUpdate()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (!isDashing)
            rb.linearVelocity = moveInput * speed;
    }


    void TryInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, interactRange, interactableLayerNPC);
        
        if (hit.collider != null)
        {
            Debug.Log("Hallo");

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
       
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)lookDirection * interactRange);
    }


    void TryDash()
    {
        if (!canDash || isDashing)
            return;

        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        canDash = false;

        Vector2 dashDirection = lookDirection.normalized;
        rb.linearVelocity = dashDirection * dashForce;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
