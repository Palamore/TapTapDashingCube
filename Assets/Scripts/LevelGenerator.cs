using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelGenerator : MonoBehaviour
{
    private static LevelGenerator instance;
    public static LevelGenerator Instance()
    {
        if(instance == null)
        {
            instance = FindObjectOfType<LevelGenerator>();
        }
        return instance;
    }

    UIManager UM;
    CubeHandler CH;
    GameManager GM;

    public GameObject Dummy;
    public GameObject LastNode;
    public GameObject NodePrefab;
    public GameObject GameOverPopup = null;
    public GameObject GimmickBall;
    public int RandMaxValue = 0;
    public int RandMinValue = 0; // 2~6 개 사이의 블럭이 랜덤으로 생성.

    public const int generateIteration = 10;
    /// <summary>
    /// private member vars
    /// </summary>
    private Vector3 nextRightPos = new Vector3(1.0f, 0.0f, 1.0f);
    private Vector3 nextLeftPos = new Vector3(-1.0f, 0.0f, 1.0f);
    private int lvIndex = 0;
    private int blockNodeSum = 0;
    private int[] randValues = new int[generateIteration];

    public int NodeCount = 0;
    public int WholeNodeCount = 0;

    private Queue<GameObject> nodesContainer = new Queue<GameObject>();
    public Queue<GameObject> gimmickBallContainer = new Queue<GameObject>();

    public Text ScoreText;
    public Text BestScoreText;
    public Text TrophyText;

    private void Awake()
    {
        UM = UIManager.Instance();
        CH = CubeHandler.Instance();
        GM = GameManager.Instance();

        RandMinValue = 1;
        RandMaxValue = 7;
    }

    LevelGenerator()
    {

    }

    void Start()
    {

        nodesContainer.Enqueue(Dummy);
        MakeNextLevel();
        SendRandValue();
        SendNodeCount();
    }

    public void DestroyNodeWithEffect()
    {
        if (nodesContainer.Count == 0) return;
        GameObject node = nodesContainer.Dequeue();
        node.GetComponent<Node>().MakeEffect();
        Destroy(node);
    }

    public void DestroyNode()
    {
        if (nodesContainer.Count == 0) return;
        Destroy(nodesContainer.Dequeue());
    }


    public void MakeNextLevel()
    {
        setRandValue();
        generateNextNodes();
    }

    private void setRandValue()
    {
        for (int i = 0; i < generateIteration; i++)
        {
            randValues[i] = Random.Range(RandMinValue, RandMaxValue);
        }
    }

    public void SendRandValue()
    {
        for(int i = 0; i < generateIteration; i++)
        {
            CH.DirValidationValue[i] = randValues[i];
        }
        CH.DirValidationIndex = 0;
    }

    private void generateNextNodes()
    {
        NodeCount = 0;
        int randInd = 0;
        int nextVal = randValues[randInd];
        bool dirFlag = true;
        while(randInd < generateIteration)
        {
            nextVal = randValues[randInd];
            for (int i = 0; i < nextVal; i++)
            {
                generateSingleNode(dirFlag);
            }

            dirFlag = !dirFlag;
            randInd++;
        }
        CH.GimmickContainer[WholeNodeCount] = 1;
        gimmickBallContainer.Enqueue(Instantiate(GimmickBall, LastNode.transform.position + new Vector3(0.0f, 0.13f, 0.0f) , Quaternion.identity));

    }



    private void generateSingleNode(bool isRight)
    {
        Vector3 nextPos;
        GameObject newNode;

        if(isRight)
        {
            nextPos = LastNode.transform.position + nextRightPos;
            newNode = Instantiate(NodePrefab, nextPos, Quaternion.Euler(0.0f, 45.0f, 0.0f)) as GameObject;
            nodesContainer.Enqueue(newNode);
            LastNode = newNode;
        }
        else
        {
            nextPos = LastNode.transform.position + nextLeftPos;
            newNode = Instantiate(NodePrefab, nextPos, Quaternion.Euler(0.0f, 45.0f, 0.0f)) as GameObject;
            nodesContainer.Enqueue(newNode);
            LastNode = newNode;
        }
        NodeCount++;
        WholeNodeCount++;
    }

    public void SendNodeCount()
    {
        UM.NodeCount = NodeCount;
    }


    public void GameOver()
    {

        while (nodesContainer.Count != 0)
            DestroyNodeWithEffect();

        if (CH != null)
            Destroy(CH.gameObject);

        Invoke("PopupGameOver", 2.0f);

        ScoreText.text = UM.MarathonScore.ToString();
        if(PlayerPrefs.GetInt("BestScore") < UM.MarathonScore)
        {
            PlayerPrefs.SetInt("BestScore", UM.MarathonScore + 1);
            PlayerPrefs.Save();
            
        }
        else
        {
            BestScoreText.text = "Best : " + PlayerPrefs.GetInt("BestScore").ToString();
        }
        
        if(UM.MarathonScore >= 500)
        {
            string prefsKey = "BG" + GM.rand.ToString();
            PlayerPrefs.SetInt(prefsKey, 1);
            TrophyText.gameObject.SetActive(true);
            TrophyText.text = "500 점 달성!\nTheme" + (GM.rand + 1).ToString() + " 획득";
        }


    }


    private void PopupGameOver()
    {
        GameOverPopup.SetActive(true);
    }

}
