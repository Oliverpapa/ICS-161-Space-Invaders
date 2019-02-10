using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public Animator UIController;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }

    public void Pause() {
        Time.timeScale = 0f;
        UIController.SetTrigger("pause");
    }

    public void Resume() {
        Time.timeScale = 1f;
        UIController.SetTrigger("resume");
    }

    public void Quit() {
        Application.Quit();
    }
}
