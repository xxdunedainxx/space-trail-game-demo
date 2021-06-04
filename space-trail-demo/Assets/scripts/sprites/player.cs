using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //private ILogger __logger;
    public float movementSpeed;
    public Rigidbody2D rb;

    public float jumpForce = 20f;
    public Transform feet;
    public Transform head;
    public LayerMask groundLayers;
    public LayerMask npcDialog;

    public float mx;
    public float my;

    public int coins;

    // Start is called before the first frame update
    void Start()
    {
        Debug.unityLogger.Log("Start new Player Object");
        // this.__logger.Log("Start new player object");
    }

    // Update is called once per frame
    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        /*if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            Jump();
        }*/

        if (Input.GetMouseButtonDown(0)) {
            Debug.unityLogger.Log("They clicked left");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                Debug.unityLogger.Log($"Hit {hit.transform.name}");
                if (hit.transform.name == "")
                {

                }
            }
            /*if(interact())
            {
                Debug.unityLogger.Log("They are trying to interact with something.");
            }*/
        }

    }

    private void FixedUpdate()
    {
        Vector2 movementX = new Vector2(mx * movementSpeed, my * movementSpeed);  //rb.velocity.y

        rb.velocity = movementX;


    }

    private void Jump() {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundChecking = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        if (groundChecking != null) {
            return true;
        }
        return false;
    }

    public bool interact()
    {
        Collider2D interactCheck = Physics2D.OverlapCircle(this.head.position, 0.5f, this.npcDialog);

        if (interactCheck)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.unityLogger.Log("Colided with objecty?");
        Debug.unityLogger.Log($"{other.gameObject.layer}");
        if (other.gameObject.layer == 7)
        {
            coins++;
            Destroy(other.gameObject);
            Debug.unityLogger.Log($"User now has {this.coins} coins");
        }
    }

    public void TriggerDialogue()
    {
        DialogManager mgr = DialogManager.getManager();

    }
}
