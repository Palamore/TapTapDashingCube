using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int RandMaxValue = 7;
    public int RandMinValue = 2;

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

    private Queue<GameObject> nodesContainer = new Queue<GameObject>();


    private void Awake()
    {
        UM = UIManager.Instance();
        CH = CubeHandler.Instance();
        GM = GameManager.Instance();
    }

    void Start()
    {
        nodesContainer.Enqueue(Dummy);
        MakeNextLevel();
        SendRandValue();
        SendNodeCount();
    }

    public void DestroyGarbageNode()
    {
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

    }

    public void SendNodeCount()
    {
        UM.NodeCount = NodeCount;
    }


}
