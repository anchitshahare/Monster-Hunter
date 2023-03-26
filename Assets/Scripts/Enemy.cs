using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log("damage taken");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ground")) {
            GetComponent<Rigidbody2D>().gravityScale = 0f; 
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);
        }
    }

}
