using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private float timebtwAttack;
    [SerializeField] private float startTimeBtwAttacks;

    [SerializeField] Transform attackPos;
    [SerializeField] private LayerMask whatIsEnemies;
    [SerializeField] float attackRange;
    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timebtwAttack <= 0) {
            timebtwAttack = startTimeBtwAttacks;
        } else {
            timebtwAttack -= Time.deltaTime;
        }
    }

    public void Attack(InputAction.CallbackContext context) {
        if(context.performed) {
            playerAnimator.SetTrigger("attack");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for(int i=0; i < enemiesToDamage.Length; i++) {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
