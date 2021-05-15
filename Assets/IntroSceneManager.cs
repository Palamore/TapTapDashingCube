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
        if(PlayerPrefs.HasKey("BestScore"))
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
