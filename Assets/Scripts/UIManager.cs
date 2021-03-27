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
    private Animation scoreFx = null;


    void Awake()
    {
        InitScores();
        
    }

    private void InitScores()
    {
        if(MarathonScoreText != null)
        {
            MarathonScore = 0;
            MarathonScoreText.text = MarathonScore.ToString();
        }
        scoreFx = GetComponent<Animation>();
    }

    public void AddScore()
    {
        MarathonScore++;
        MarathonScoreText.text = MarathonScore.ToString();
    }


}
