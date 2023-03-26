using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private BoxCollider2D _groundCheckPoint;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] private GameObject playerHand;
    [SerializeField] private float playerHandPosition = 0.4f;
    private float _horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        playerHandPosition = playerHand.transform.localPosition.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // _rb.MovePosition(_rb.position + _horizontalInput * Time.fixedDeltaTime * _speed);
        _rb.velocity = new Vector2(_horizontalInput * _speed  * Time.deltaTime, _rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context) {
        _horizontalInput = context.ReadValue<Vector2>().x;

        if(_horizontalInput > 0) {
            playerHand.transform.localPosition = new Vector2(playerHandPosition, playerHand.transform.localPosition.y);
        } else if(_horizontalInput < 0) {
            playerHand.transform.localPosition = new Vector2(-playerHandPosition, playerHand.transform.localPosition.y);
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        if(context.started && isGrounded()) {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }

    private bool isGrounded() {
        return Physics2D.BoxCast(_groundCheckPoint.bounds.center, _groundCheckPoint.bounds.size, 0, Vector2.down, 0.1f, _groundLayer);
    }
    
    
}
