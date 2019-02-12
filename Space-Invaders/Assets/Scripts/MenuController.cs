using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadSceneAsync("Level");
    }
    
    public void Quit() {
//        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
