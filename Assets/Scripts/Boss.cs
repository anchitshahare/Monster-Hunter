using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isFacingRight = false;
    [SerializeField] private float attackDistance = 2f;
    public bool isDead = false;
    public bool isBossFight = false;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead && isBossFight) {
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
        }
    }

    private void Flip() {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ground")) {
            GetComponent<Rigidbody2D>().gravityScale = 0f; 
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);
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

    private void Die() {
        isDead = true;
        animator.SetBool("isAttacking", false);
        animator.SetTrigger("isDead");
        
    }
}
