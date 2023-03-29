using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    static public BossFightManager instance;
    public Boss boss;
    public GameObject entry;
    public GameObject exit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.isDead) {
            exit.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            entry.SetActive(true);
            exit.SetActive(true);
            boss.isBossFight = true;
        }
    }
}
