using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public Animator UIAnimator;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public static int levelNum = 1;
    public GameObject live1;
    public GameObject live2;
    public GameObject live3;

    private bool paused = false;

    private void Start() {
//        Enemy.instance.getKill.AddListener(UpdateScore);
        Debug.Log(levelNum);
        levelText.SetText(String.Format("Level {0}", levelNum));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (paused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Pause() {
        paused = true;
        Time.timeScale = 0f;
        UIAnimator.SetTrigger("pause");
    }

    public void Resume() {
        paused = false;
        Time.timeScale = 1f;
        UIAnimator.SetTrigger("resume");
    }

    public void Restart() {
        levelNum = 1;
        SceneManager.LoadSceneAsync("Level");
    }

    public void NextLevel() {
        ++levelNum;
        Debug.Log(levelNum);
        SceneManager.LoadScene("Level");
    }

    public void Quit() {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void UpdateScore(int newScore) {
        scoreText.SetText(String.Format("Score: {0}", newScore));
    }

    private void UpdateLives() {
        
    }
}