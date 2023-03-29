using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    public GameObject deathScreen;
    public GameObject[] UICards;
    public string collectedCardName;
    public Transform firstPosition;
    public Transform lastPosition;
    public Transform worldCanvasTrans;
    public float space = 0f;

    private void Awake() {
        worldCanvasTrans = GameObject.Find("Canvas").transform;
    }

    private void Start() {
        instance = this;
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void DisplayCard() {
        for(int i = 0; i < UICards.Length; i++) {
            if(UICards[i].name == collectedCardName) {
                GameObject newCard = Instantiate(UICards[i], new Vector3(firstPosition.localPosition.x + space, firstPosition.localPosition.y, 0f), Quaternion.identity);
                newCard.transform.SetParent(worldCanvasTrans, true);
                newCard.transform.localPosition = new Vector3(firstPosition.localPosition.x + space, firstPosition.localPosition.y, 0f);
                newCard.transform.localScale = new Vector3(50.0f, 50.0f, 0f);
                
                
                newCard.GetComponent<Animator>().enabled = false;
                newCard.layer = LayerMask.NameToLayer("UI");

                foreach(Transform child in newCard.transform) {
                    child.gameObject.layer = LayerMask.NameToLayer("UI");
                }
                space += 100f;
                break;
            }
        }
    }
}
