using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }


    public void LoadGame() {
        if (scoreKeeper != null) scoreKeeper.ResetScore();
        SceneManager.LoadScene("swag");
    }

    public void LoadMenu() {
        SceneManager.LoadScene("title");
    }

    public void LoadGameOver() {
        SceneManager.LoadScene("gameover");
    }

    public void QuitGame() {
        Debug.Log("HAHA FUCKIN RAGEQUIT LOSER");
        Application.Quit();
    }

    
    public void DelayLoadGameOver() {
        StartCoroutine(RealGameOverDelay());
    }

    IEnumerator RealGameOverDelay() {
        Debug.Log("SUPER CRINGE!!!");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("gameover");
    }
}
