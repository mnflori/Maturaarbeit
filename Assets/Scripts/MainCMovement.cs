using UnityEngine;
using Movement;
using TMPro;


// git add . && git commit -m "Meine �nderung" && git push




public class MainCMovement : MonoBehaviour
{
    //Movement Variables
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private MCMovement controls;
   

    //Interaction Variables
    public float interactRange = 1.5f;         
    public LayerMask interactableLayer;           
    private Vector2 lookDirection = Vector2.right;



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
        rb.linearVelocity = moveInput * speed;

        

    }

    void TryInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, interactRange, interactableLayer);
        
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
}
