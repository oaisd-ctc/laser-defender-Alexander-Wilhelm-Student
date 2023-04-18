using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] List<StageSO> stageList;

    [SerializeField] float musicFadeFactor;

    [SerializeField] float stageDelay;

    EnemySpawner enemySpawner;

    GameObject music;
    AudioSource musicSrc;
    float defaultMusicVol;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        music = FindObjectOfType<MusicLoop>().gameObject;
        musicSrc = music.GetComponent<AudioSource>();
        defaultMusicVol = musicSrc.volume;
        StartCoroutine(HandleStages());
    }


    public IEnumerator HandleStages()
    {
        foreach (StageSO stage in stageList)
        {
            NextStage(stage);
            do { yield return null; }
            while (enemySpawner.spawningEnemies || GameObject.FindGameObjectsWithTag("Enemy").Length > 0);
            StartCoroutine(FadeMusic());
            yield return new WaitForSeconds(stageDelay);
        }

    }

    void NextStage(StageSO stage)
    {
        enemySpawner.waveList = stage.GetWaves();
        StartCoroutine(enemySpawner.SpawnWaves());
        musicSrc.clip = stage.GetIntroMusic();
        music.GetComponent<MusicLoop>().LoopMusic = stage.GetLoopMusic();
        musicSrc.loop = false;
        musicSrc.volume = defaultMusicVol;
        musicSrc.Play();

    }

    public IEnumerator FadeMusic()
    {
        while (musicSrc.volume >= Mathf.Epsilon)
        {
            musicSrc.volume -= (Time.deltaTime * musicFadeFactor);
            yield return null;
        }
        musicSrc.Stop();
        StopCoroutine(FadeMusic());
    }
}
