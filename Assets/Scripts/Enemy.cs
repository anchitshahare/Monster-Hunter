using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isFacingRight = false;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float chaseDistance = 5f;
    [SerializeField] private bool shouldPatrol;
    [SerializeField] private LayerMask groundLayer;
    private bool isDead = false;
    private Vector2 patrolDirection = Vector2.right;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isDead) {
            RaycastHit2D groundHit = Physics2D.Raycast(transform.position, -transform.up, 2f, groundLayer);
        
            if(groundHit.collider != null && Vector2.Distance(transform.position, player.transform.position) < chaseDistance && !player.GetComponent<PlayerActions>().isDead) {
                if(Vector2.Distance(transform.position, player.transform.position) < attackDistance) {
                    rb.velocity = Vector2.zero;
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isAttacking", true);
                } else {
                    float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
                    rb.velocity = new Vector2(direction * speed * Time.deltaTime, 0f);

                    if(direction > 0 && !isFacingRight) {
                        Flip();
                    } else if (direction < 0 && isFacingRight) {
                        Flip();
                    }
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isAttacking", false);
                }
            } else {
                if(shouldPatrol) {
                    Patrol();
                } else {
                    rb.velocity = Vector2.zero;
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isAttacking", false);
                }
            }
        }
        
    }

    public void TakeDamage(int damage) {
        if(isDead) return;
        health -= damage;
        

        if(health <= 0) {
            Die();
        } else {
            animator.SetTrigger("hurt");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ground")) {
            isGrounded = true;
            GetComponent<Rigidbody2D>().gravityScale = 0f; 
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Ground")) {
            isGrounded = false;
        }
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        patrolDirection.x = -patrolDirection.x;

        Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }
    
    private void Die() {
        isDead = true;
        animator.SetBool("isAttacking", false);
        animator.SetTrigger("isDead");
    }

    public void Patrol() {
            animator.SetBool("isAttacking", false);
            RaycastHit2D groundHit = Physics2D.Raycast(transform.position, -transform.up, 2f, groundLayer);
            RaycastHit2D wallHit = Physics2D.Raycast(transform.position, transform.forward,1f, groundLayer);
            
            if(groundHit.collider == null) {
                Flip();  
            } 
            rb.velocity = new Vector2(patrolDirection.x * speed * Time.deltaTime, 0f);
            if(rb.velocity.magnitude > 0f) {
                animator.SetBool("isWalking", true);
            } else {
                animator.SetBool("isWalking", false);
            }
    }
}
