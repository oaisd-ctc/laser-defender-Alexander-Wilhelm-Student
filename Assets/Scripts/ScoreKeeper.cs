using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int score;
    static ScoreKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int x) {
        score += x;
        if (score > 9999999) score = 9999999; //prevent score ui from looking funky
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore() {
        score = 0;
    }

}
