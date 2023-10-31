using System;
using System.Collections;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    public float speed = 3f;           // Movement speed of the player.
    public float jumpForce = 7f;           // Force applied when the player jumps.
    private Rigidbody2D rb;                // Reference to the player's Rigidbody2D component.
    private bool isGrounded;               // Check if the player is grounded.
    public Transform groundCheck;         // Reference to the ground check position.
    public LayerMask groundLayer;         // Layer mask for the ground objects.
    private bool isFacingRight = true;     // Flag to track the direction the player is facing.
    private bool IsJumping = false;
    public static bool[] isHitting = new bool[4];
    private GameManager gameManager;
    

    //sound effects
    [SerializeField] private AudioSource Jumpsound;
    [SerializeField] private AudioSource CollectCoin;
    [SerializeField] private AudioSource Walking;


    public Animator animator;              // Refernce for the character animations
    private float objectPosition;

    

    
   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();                  // Get the Rigidbody2D component of the player.
        groundCheck = transform.Find("Ground");       // Find the ground check position.
        groundLayer = LayerMask.GetMask("Ground");          // Set the ground layer (change to your ground layer name).
        rb.freezeRotation = true;                           // Lock the character's rotation.

        objectPosition = transform.position.x;       // Get the current local scale.
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    void Update()
    {
        // Check if the player is grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        
        // Player movement
        float moveDirection = Input.GetAxis("Horizontal"); // Get the horizontal input (left or right).
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y); // Apply horizontal velocity.
        

        // Control the animation based on whether the player is jumping or not
        if (IsJumping)
        {
            
            animator.SetBool("IsJumping", true); // Assuming you have a "isJumping" parameter in your Animator controller.
            Walking.Stop();
        }
        else
        {
            // Play walk animation + walk Sound
            animator.SetBool("IsJumping", false);
        
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x)); // Set the speed variable to the velocity of the player

        //Player Animation
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 6f;
            animator.SetFloat("Speed", 6);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            
            speed = 3f;
        }
        if (speed >= 0f)
        {
            Walking.Stop();
        }
        if (speed < 0f)
        {
            Walking.Stop();
        }

        // Flip the character's direction if necessary.
        if (moveDirection > 0 && !isFacingRight)
        {
            Walking.Play();
           
            FlipCharacter();
        }
        else if (moveDirection < 0 && isFacingRight)
        {
            Walking.Play();
            
            FlipCharacter();
        }


        // Player jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jumpsound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        if (isGrounded == true)
        {

            IsJumping = false; // Set isJumping to true when jumping.
        }
        else
        {
            IsJumping = true; // Set isJumping to false when grounded.
        }



       
    }

    // Function to flip the character's direction.
    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;             // Toggle the facing direction flag.


        Vector3 scale = transform.localScale;       // Get the current local scale.
        scale.x *= -1;                             // Invert the X scale to flip horizontally.
        transform.localScale = scale;               // Apply the new local scale to flip the character.

        new Vector2(transform.position.x, objectPosition);


    }

  

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            CollectCoin.Play();
            gameManager.coinCounter += 1;
            Destroy(other.gameObject);
            Debug.Log("Player has collected a coin!");
        }
    }
   


}

