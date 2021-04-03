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

    public Text MarathonScoreText = null;
    public int MarathonScore = 0;
    private Animation marathonScoreFx = null;


    public Text TimeAttackTimerText = null;
    private int timeAttackRemainTime = 90;


    public GameObject RhythmActionNodesBar;

    public Transform LeftEndNodePos;
    public Transform RightEndNodePos;

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

                break;
            case GameModeEnum.TimeAttack:
                TimeAttackTimerText.gameObject.SetActive(true);
                StartCoroutine(timer());
                break;
            case GameModeEnum.RhythmAction:
                RhythmActionNodesBar.SetActive(true);
                break;
            default: Debug.LogError("GameMode needs to be set as actual type");
                break;
        }

    }

    public void AddScore()
    {
        MarathonScore++;
        MarathonScoreText.text = MarathonScore.ToString();
        marathonScoreFx.Play("ScoreAnim");
    }


    IEnumerator timer()
    {
        while(timeAttackRemainTime != 0)
        {
            TimeAttackTimerText.text = TimeFormatting(timeAttackRemainTime);
            timeAttackRemainTime--;
            yield return new WaitForSeconds(1.0f);
        }
    }



    private string TimeFormatting(int time)
    {
        int min = time / 60;
        int sec = time % 60;
        return min.ToString() + ":" + sec.ToString();
    }


}
