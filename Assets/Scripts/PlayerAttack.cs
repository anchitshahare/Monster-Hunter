using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private float timebtwAttack;
    [SerializeField] private float startTimeBtwAttacks;

    [SerializeField] private LayerMask whatIsEnemies;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timebtwAttack <= 0) {
            // timebtwAttack = startTimeBtwAttacks;
        } else {
            timebtwAttack -= Time.deltaTime;
        }
    }

    public void Attack(InputAction.CallbackContext context) {
        if(context.performed && timebtwAttack <= 0) {
            playerAnimator.SetTrigger("attack");
            
            timebtwAttack = startTimeBtwAttacks;
        }
    }
}
