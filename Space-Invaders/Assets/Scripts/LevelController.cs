using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public Animator UIAnimator;
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public static int levelNum = 1;
    public GameObject live1;
    public GameObject live2;
    public GameObject live3;

    private bool paused = false;

    public static LevelController instance;

    private void Awake() {
        instance = this;
    }

    private void Start() {
//        Enemy.instance.getKill.AddListener(UpdateScore);
        Debug.Log(levelNum);
//        EnemyCol.instance.levelNum = levelNum;
        Time.timeScale = 1f;
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
    
    public void Win() {
        Time.timeScale = 0f;
        UIAnimator.SetTrigger("win");
    }

    public void End() {
        Time.timeScale = 0f;
        UIAnimator.SetTrigger("end");
    }

    public void Restart() {
        levelNum = 1;
        score = 0;
        SceneManager.LoadSceneAsync("Level");
    }

    public void NextLevel() {
        ++levelNum;
        Debug.Log(levelNum);
        SceneManager.LoadScene("Level");
    }

    public void Quit() {
//        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void UpdateScore(int newScore) {
        score += newScore;
        scoreText.SetText(String.Format("Score: {0}", score));
    }

    public void UpdateLives() {
        int lives = Player.instance.life;
        switch (lives) {
            case 2: 
                live3.SetActive(false);
                break;
            case 1:
                live3.SetActive(false);
                live2.SetActive(false);
                break;
            case 0:
                live3.SetActive(false);
                live2.SetActive(false);
                live1.SetActive(false);
                End();
                break;
            default:
                Debug.LogError("Live num is wrong.");
                break;
        }
    }
}