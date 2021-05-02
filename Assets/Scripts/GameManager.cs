using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameModeEnum
{
    Marathon = 0,
    TimeAttack,
    RhythmAction
}

/// <summary>
/// TODO :
/// 배경 디자인            10
/// 게임오버 연출           1
/// 점프 연출 (파티클)      1
/// 프로그레스바 연출,      1
/// 점수 애니메이션 연출    1
/// 나비 연출               ? 
/// </summary>


public class GameManager : MonoBehaviour
{

    static GameManager instance;

    public static GameManager Instance()
    {
        if(instance == null)
        {
            instance = FindObjectOfType<GameManager>();
        }
        return instance;
    }

    private UIManager UM;

    public Camera MainCamera = null;
    public GameObject PausePopup = null;
    public GameObject LPointPrefab;
    public GameObject RPointPrefab;
    private GameObject cube;

    public GameObject LPoint;
    public GameObject RPoint;

    public Transform CameraTransform;

    public GameModeEnum GameMode;

    public GameObject RythmNodePrefab;

    private Queue<GameObject> leftNodesQueue = new Queue<GameObject>();
    private Queue<GameObject> rightNodesQueue = new Queue<GameObject>();

    

    private void Awake()
    {
        Application.targetFrameRate = 90;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1280, 720, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        UM = UIManager.Instance();
        cube = CubeHandler.Instance().gameObject;
        CameraTransform = MainCamera.transform;
        MakePoints();

        UM.InitScores(GameMode);
        if(GameMode == GameModeEnum.RhythmAction)
        {
            StartCoroutine(MakeRhythmNodes());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cube != null)
            CameraTransform.position = cube.transform.position + new Vector3(
                0.0f, 6.0f - cube.transform.position.y, -3.0f);
    }


    public void MakePoints()
    {
        LPoint = Instantiate(LPointPrefab, cube.transform.position + new Vector3(-0.5f, 0.0f, 0.5f), Quaternion.Euler(0.0f, -45.0f, 0.0f));
        RPoint = Instantiate(RPointPrefab, cube.transform.position + new Vector3( 0.5f, 0.0f, 0.5f), Quaternion.Euler(0.0f,  45.0f, 0.0f));
    }

    public void DestroyPoints()
    {
        Destroy(LPoint);
        Destroy(RPoint);
    }
    
    public bool ValidateNodeDistance()
    {
        if (rightNodesQueue.Count == 0) return false;
        float dist = (rightNodesQueue.Peek().transform.position.x - leftNodesQueue.Peek().transform.position.x);

        if(rightNodesQueue.Count != 0)
        {
            GameObject Rnode = rightNodesQueue.Dequeue();
            Destroy(Rnode);
            GameObject Lnode = leftNodesQueue.Dequeue();
            Destroy(Lnode);
        }

        if (dist < 0.0f || dist > 60.0f)
        {
            return false;
        }
        return true;
    }

    IEnumerator MakeRhythmNodes()
    {
        while(true)
        {
            GameObject leftNode = Instantiate(RythmNodePrefab, UM.LeftEndNodePos.position, Quaternion.identity) as GameObject;
            leftNode.transform.SetParent(UM.LeftEndNodePos);
            leftNode.GetComponent<RhythmActionNode>().IsLeft = true;
            leftNodesQueue.Enqueue(leftNode);
            GameObject rightNode = Instantiate(RythmNodePrefab, UM.RightEndNodePos.position, Quaternion.identity) as GameObject;
            rightNode.transform.SetParent(UM.RightEndNodePos);
            rightNode.GetComponent<RhythmActionNode>().IsLeft = false;
            rightNodesQueue.Enqueue(rightNode);

            yield return new WaitForSeconds(0.4f);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        PausePopup.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        PausePopup.SetActive(false);
    }
}
