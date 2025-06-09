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

    //Inventory Variables
    [Header("Scripts")]
    [SerializeField] private InventoryPreviewUI inventoryPreviewUI;
    [SerializeField] private InventoryManager inventoryManager;
    private float holdThreshold = 0.25f; //Damit das Inventar nicht direkt gezeigt wird, sondern mann gedigen das Item mit einmaligem Klicken ändern kann
    private bool tabHeld = false;
    private float tabPressedTime;


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

        controls.UI.InventoryPreview.started += ctx => //Hier wird, sobald Tab gedrückt wird, die Zeit gezählt.
        {
            tabHeld = true;
            tabPressedTime = Time.time; 
            
        };

        controls.UI.InventoryPreview.canceled += ctx =>
        {
            tabHeld = false;

            float heldDuration = Time.time - tabPressedTime;

            if (heldDuration < holdThreshold) // Sollte hier die heldDuration, also die Zeit, seit dem Halten von Tab, kleiner sein als der holdThreshold, wird das Item einfach gewechselt. Wird gemacht, sobald Tab losgelasst wird.
            {
                switchToNextItem();
            }
            else
            {
                inventoryPreviewUI.HideMiniInventory(); //Sollte die heldDuration länger sein, wird beim loslassen von Tab einfach die UI ausgeblendet.
            }
        };

    }


    private void OnEnable()
    {
        controls.Player.Enable();
        controls.UI.Enable();

    }

    private void OnDisable()
    {
        
        controls.Player.Disable();
        controls.UI.Disable();
    }

    private void Update()
    {
        if (tabHeld && Time.time - tabPressedTime > holdThreshold && !inventoryPreviewUI.isVisible()) //Hier wird geschaut, ob der Threshold schon erreicht wurde und ob Tab immer noch gedrückt wird. Wenn beides der Fall ist, wird das Inventar gezeigt.
        {
            inventoryPreviewUI.ShowMiniInventory();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    private void FixedUpdate()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive) //Dies ist, um das Bewegen wärend einer Interaktion mit einem NPC zu verhindern.
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (!isDashing)
            rb.linearVelocity = moveInput * speed; 
    }


    void TryInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, interactRange, interactableLayerNPC); //Hier wird geschaut, ob der Strich, der vom MC weggeht, beim Drücken von "E", ein GameObject mit dem Layer "Interactable" trifft.
        if (hit.collider != null)
        {
            

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {

                if (hit.collider.TryGetComponent<IItem>(out IItem item))
                {
                    
                    string itemName = item.getItemName();
                    Debug.Log(itemName);
                    inventoryManager.addItem(itemName);
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    
                    interactable.Interact(); //Wenn ja, wird das Skript aufgerufen, dass dazuführt, dass Text angezeigt wird.
                }
                
            }
        }

       

    }

    private void switchToNextItem()
    {
        inventoryManager.SelectNextItem(); //Dies zeigt an, welches Item ausgewählt ist.
    }

    void OnDrawGizmos() //Dies ist dafür da, um den "Interaction-Strich" zu sehen im Scene View.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)lookDirection * interactRange); 
    }


    void TryDash()
    {
        if (!canDash || isDashing)
            return;

        StartCoroutine(DashRoutine()); //Dies ist dafür da, um das spammen von Dash zu verhindern.
    }

    private IEnumerator DashRoutine() //Dies zählt die Zeit ab, wenn man wieder Dash benutzen kann.
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
