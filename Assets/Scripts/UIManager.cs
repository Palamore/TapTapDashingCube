using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager instance;

    public static UIManager Instance()
    {
        if(instance == null)
        {
            instance = FindObjectOfType<UIManager>();
        }
        return instance;
    }

    LevelGenerator LG;

    private void Start()
    {
        LG = LevelGenerator.Instance();
    }

    public Text MarathonScoreText = null;
    public int MarathonScore = 0;
    private Animation marathonScoreFx = null;
    public GameObject MarathonProgressBar = null;
    private float progressBarValue = 100.0f;

    public float ProgressBarTension
    {
        get { return progressBarTension; }
        set { progressBarTension = value; }
    }


    public Text TimeAttackTimerText = null;
    private int timeAttackRemainTime = 90;


    public GameObject RhythmActionNodesBar;

    public Transform LeftEndNodePos;
    public Transform RightEndNodePos;


    public int NodeCount = 0;
    private int jumpCount = 0;

    private float progressBarTension = 19.0f;


    public void AddScore()
    {
        MarathonScore++;
        jumpCount++;
        if (MarathonScore % 20 == 0)
        { // 10점씩 얻을 때마다 Bar의 감소 속도가 빨라짐.
            if (progressBarTension < 30.0f)
                progressBarTension += 1.0f;
        }
        MarathonScoreText.text = MarathonScore.ToString();
        marathonScoreFx.Play("ScoreAnim");

        if (jumpCount == NodeCount - 10)
        {
            LG.MakeNextLevel();
        }
        else if (jumpCount == NodeCount)
        {
            LG.SendNodeCount();
            LG.SendRandValue();
            jumpCount = 0;
        }

        if (MarathonScore % 100 == 0)
        {
            if (LG.RandMaxValue >= 5) // 1~4
                LG.RandMaxValue--;
        }
    }

    IEnumerator marathonTimer()
    {
        while (progressBarValue > 0)
        {
            // 16 / 60   0.26 1초에 16.0f
            progressBarValue -= progressBarTension / 60.0f;
            MarathonProgressBar.transform.localScale = new Vector3(progressBarValue, 1.0f, 1.0f);
            yield return new WaitForSeconds(1.0f / 60.0f);
        }
        StopCoroutine(marathonTimer());
        /// TO DO : 게임 오버 연출
        Debug.Log("Game Over!");
    }


    public void InitScores(GameModeEnum gameMode)
    {
        if (MarathonScoreText != null)
        {
            MarathonScore = 0;
            MarathonScoreText.text = MarathonScore.ToString();
        }
        marathonScoreFx = MarathonScoreText.gameObject.GetComponent<Animation>();

        switch (gameMode)
        {
            case GameModeEnum.Marathon:
                MarathonProgressBar.SetActive(true);
                StartCoroutine(marathonTimer());
                break;
            case GameModeEnum.TimeAttack:
                TimeAttackTimerText.gameObject.SetActive(true);
                StartCoroutine(timeAttackTimer());
                break;
            case GameModeEnum.RhythmAction:
                RhythmActionNodesBar.SetActive(true);
                break;
            default: Debug.LogError("GameMode needs to be set as actual type");
                break;
        }

    }



    IEnumerator timeAttackTimer()
    {
        while(timeAttackRemainTime > 0)
        {
            TimeAttackTimerText.text = TimeFormatting(timeAttackRemainTime);
            timeAttackRemainTime--;
            yield return new WaitForSeconds(1.0f);
        }
        StopCoroutine(timeAttackTimer());
        /// TO DO : 게임 오버 연출
        Debug.Log("Game Over!");

    }


    public void GainPlayTime()
    {
        progressBarValue += 5.0f;
        if (progressBarValue >= 100.0f) progressBarValue = 100.0f;
        MarathonProgressBar.transform.localScale = new Vector3(progressBarValue, 1.0f, 1.0f);
    }


    private string TimeFormatting(int time)
    {
        int min = time / 60;
        int sec = time % 60;
        return min.ToString() + ":" + sec.ToString();
    }


}
