using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public Animator UIAnimator;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }

    public void Pause() {
        Time.timeScale = 0f;
        UIAnimator.SetTrigger("pause");
    }

    public void Resume() {
        Time.timeScale = 1f;
        UIAnimator.SetTrigger("resume");
    }

    public void Quit() {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}