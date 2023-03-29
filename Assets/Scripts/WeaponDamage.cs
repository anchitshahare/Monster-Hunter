using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float resetDamageTimer;
    private bool canDamage = true;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy") && canDamage) {
            other.GetComponent<Enemy>().TakeDamage(damage);
            canDamage = false;
            StartCoroutine(ResetDamage());
        }

        if(other.CompareTag("Boss") && canDamage) {
            other.GetComponent<Boss>().TakeDamage(damage);
            canDamage = false;
            StartCoroutine(ResetDamage());
        }
            
        if(other.CompareTag("Player") && canDamage) {
            other.GetComponent<PlayerActions>().TakeDamage(damage);
            canDamage = false;
            StartCoroutine(ResetDamage());
        }    
    }

    IEnumerator ResetDamage() {
        yield return new WaitForSeconds(resetDamageTimer);
        canDamage = true;
    }
}
