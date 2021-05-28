using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{

    public Text BestScoreTxt;



    private void Awake()
    {
        Application.targetFrameRate = 90;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1280, 720, true);

        if (PlayerPrefs.HasKey("BestScore"))
        {
            BestScoreTxt.text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            BestScoreTxt.text = "0";
            PlayerPrefs.SetInt("BestScore", 0);
            PlayerPrefs.Save();
        }
    }


    public void MoveToGameScene()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Load Scene");
    }

}
