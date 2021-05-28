using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eBackGround
{
    RANDOM = 0,
    CFLOW1,
    CFLOW2,
    CFLOW3,
    CFLOW4,
    RFLOW1,
    RFLOW2,
    RFLOW3,
    RFLOW4
}

public class DataContainer : MonoBehaviour
{
    public static eBackGround theme = eBackGround.RANDOM;

    public Image[] ActivateImages = new Image[8];

    public GameObject TrophyPopup;

    private bool activeFlag = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    private void CheckPlayerPrefs()
    {
        string prefsKey = "BG";
        for (int i = 0; i < 8; i++)
        {
            prefsKey = "BG" + i.ToString();
            if(PlayerPrefs.GetInt(prefsKey) == 1)
            {
                ActivateImages[i].gameObject.SetActive(true);
            }
            else
            {
                ActivateImages[i].gameObject.SetActive(false);
            }
        }
        switch(DataContainer.theme)
        {
            case eBackGround.CFLOW1:
                ActivateImages[0].enabled = true;
                break;
            case eBackGround.CFLOW2:
                ActivateImages[1].enabled = true;
                break;
            case eBackGround.CFLOW3:
                ActivateImages[2].enabled = true;
                break;
            case eBackGround.CFLOW4:
                ActivateImages[3].enabled = true;
                break;
            case eBackGround.RFLOW1:
                ActivateImages[4].enabled = true;
                break;
            case eBackGround.RFLOW2:
                ActivateImages[5].enabled = true;
                break;
            case eBackGround.RFLOW3:
                ActivateImages[6].enabled = true;
                break;
            case eBackGround.RFLOW4:
                ActivateImages[7].enabled = true;
                break;
        }

    }

    public void OnClickTrophy()
    {
        CheckPlayerPrefs();
        TrophyPopup.SetActive(true);
    }

    public void OnCloseTrophy()
    {
        TrophyPopup.SetActive(false);
    }

    public void SetTheme(int numb)
    {
        for(int i = 0; i < 8; i++)
        {
            ActivateImages[i].enabled = false;
        }
        switch(numb)
        {
            case 0: 
                if(theme == eBackGround.CFLOW1)
                    activeFlag = true;
                theme = eBackGround.CFLOW1;
                break;
            case 1:
                if (theme == eBackGround.CFLOW2)
                    activeFlag = true;
                theme = eBackGround.CFLOW2;
                break;
            case 2:
                if (theme == eBackGround.CFLOW3)
                    activeFlag = true;
                theme = eBackGround.CFLOW3;
                break;
            case 3:
                if (theme == eBackGround.CFLOW4)
                    activeFlag = true;
                theme = eBackGround.CFLOW4;
                break;
            case 4:
                if (theme == eBackGround.RFLOW1)
                    activeFlag = true;
                theme = eBackGround.RFLOW1;
                break;
            case 5:
                if (theme == eBackGround.RFLOW2)
                    activeFlag = true;
                theme = eBackGround.RFLOW2;
                break;
            case 6:
                if (theme == eBackGround.RFLOW3)
                    activeFlag = true;
                theme = eBackGround.RFLOW3;
                break;
            case 7:
                if (theme == eBackGround.RFLOW4)
                    activeFlag = true;
                theme = eBackGround.RFLOW4;
                break;
        }
        if (!activeFlag)
            ActivateImages[numb].enabled = true;
        else
        {
            activeFlag = false;
            theme = eBackGround.RANDOM;
        }
    }

}
