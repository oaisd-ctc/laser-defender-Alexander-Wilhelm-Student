using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //lotsa spaghetti!
using TMPro; //no im not making a separate script for stage ui lol

public class GameManager : MonoBehaviour
{

    [SerializeField] List<StageSO> stageList;

    [SerializeField] float musicFadeFactor;

    [SerializeField] float stageStartDelay;
    [SerializeField] float stageEndDelay;
    [SerializeField] float fadeHold;
    [SerializeField] float fadeSpeed;
    [SerializeField] float gameOverFadeSpeed;
    [SerializeField] Image img;
 
    [SerializeField] TextMeshProUGUI stageStartText;
    [SerializeField] TextMeshProUGUI stageEndText;

    EnemySpawner enemySpawner;

    GameObject music;
    GameObject player;
    int maxhealth;
    AudioSource musicSrc;
    float defaultMusicVol;

  

    public bool stagePlaying;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        player = FindObjectOfType<Player>().gameObject;
        maxhealth = player.GetComponent<Health>().GetHealth();
        music = FindObjectOfType<MusicLoop>().gameObject;
        musicSrc = music.GetComponent<AudioSource>();
        defaultMusicVol = musicSrc.volume;
        StartCoroutine(HandleStages());
    }


    public IEnumerator HandleStages()
    {
        foreach (StageSO stage in stageList)
        {
            stageStartText.text = stage.GetStageName();
            StartCoroutine(FadeText(stageStartText, fadeSpeed));
            player.GetComponent<Health>().SetHealth(maxhealth);
            yield return new WaitForSeconds(stageStartDelay);
            NextStage(stage);
            stagePlaying = true;
            do { yield return null; }
            while (enemySpawner.spawningEnemies || GameObject.FindGameObjectsWithTag("Enemy").Length > 0);
            stagePlaying = false;
            StartCoroutine(FadeText(stageEndText, fadeSpeed/3));
            StartCoroutine(FadeMusic());
            yield return new WaitForSeconds(stageEndDelay);
            
        }

            yield return new WaitForSeconds(stageEndDelay);
            FadePanel();
            FindObjectOfType<LevelManager>().DelayLoadGameOver();

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

    public void ImpostorFadeMusic() { //lmfao
        StartCoroutine(FadeMusic()); 
    }
    public IEnumerator FadeMusic()
    {
        while (musicSrc.volume >= Mathf.Epsilon)
        {
            musicSrc.volume -= (Time.deltaTime * musicFadeFactor);
            yield return null;
        }
        musicSrc.Stop();
    }

    public IEnumerator FadeText(TextMeshProUGUI text, float fadeFactor) {
        
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        yield return new WaitForSeconds(fadeHold);
        Debug.Log($"{text.color.r} {text.color.g} {text.color.b} {text.color.a}");
        while (text.color.a >= Mathf.Epsilon)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, (text.color.a - (fadeFactor*Time.deltaTime))); //INSANE!!!! WHY AM I DOING THIS
            yield return null;
        }
        
    }

    public void FadePanel() {
        StartCoroutine(RealFadePanel());
    }
    IEnumerator RealFadePanel() {
        Debug.Log("ULTRACRINGE!!!");
        while(true) {
            img.color += new Color(0,0,0, gameOverFadeSpeed*Time.deltaTime);
            yield return null;
        }
        
    }

    public void StopGame() {
        StopCoroutine(HandleStages());
    }
}
