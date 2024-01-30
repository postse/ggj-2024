using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneActivator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    AudioSource bubblePop;

    bool scenePlaying = true;

    void ActivateMainMenu() {
        mainMenu.SetActive(true);
    }

    void PlayPop() {
        bubblePop.Play();
    }

    void Update() {
        if (Input.anyKey && scenePlaying) {
            GetComponent<Animator>().SetTrigger("End");
            scenePlaying = false;
            ActivateMainMenu();
        }
    }
}
