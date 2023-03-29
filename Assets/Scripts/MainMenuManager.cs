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
                newCard.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                newCard.transform.SetParent(worldCanvasTrans, true);
                
                // UICards[i].transform. = new Vector2(firstPosition.localPosition.x + space, firstPosition.localPosition.y);
                newCard.GetComponent<Animator>().enabled = false;
                newCard.layer = LayerMask.NameToLayer("UI");

                foreach(Transform child in newCard.transform) {
                    child.gameObject.layer = LayerMask.NameToLayer("UI");
                }
                space += 1f;
                break;
            }
        }
    }
}
