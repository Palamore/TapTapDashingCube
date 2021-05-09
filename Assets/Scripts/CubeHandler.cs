using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private static CubeHandler instance;

    public static CubeHandler Instance()
    {
        if(instance == null)
        {
            instance = FindObjectOfType<CubeHandler>();
        }
        return instance;
    }

    private const int generateIteration = 10;
    private Animation cubeMoveAnimation;
    private int invokeCnt;
    public Transform RotationPoint;
    private GameManager GM;
    private UIManager UM;
    private LevelGenerator LG;
    private float moveSpeed = 6.0f;
    private bool inputFlag;
    private bool dirRightFlag;

    private bool jumpFlag; /// TODO :: 입력 한번만, 플래그 세워서 받아주기.
    private bool isRight;

    public int[] DirValidationValue = new int[generateIteration];

    public GameObject VFXButterFly;
    public GameObject VFXDestroy;

    private int dirValidationIndex;
    public int DirValidationIndex
    {
        get { return dirValidationIndex; }
        set
        {
            dirValidationIndex = value;
        }
    }

    void Start()
    {
        invokeCnt = 0;

        inputFlag = true;
        dirRightFlag = true;
        jumpFlag = false;
        isRight = true;
        DirValidationIndex = 0;
    }

    void Awake()
    {
        GM = GameManager.Instance();
        UM = UIManager.Instance();
        LG = LevelGenerator.Instance();
        cubeMoveAnimation = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
       {
           MoveLeft();
       }
       if (Input.GetKeyDown(KeyCode.D))
       {
           MoveRight();
       }

    }


    public void OnDestroy()
    {
        Instantiate(VFXDestroy, gameObject.transform.position, Quaternion.identity);
    }

    public void MoveLeft()
    {
        if (!inputFlag)
        {
            jumpFlag = true;
            isRight = false;
            return;
        }

        if(GM.GameMode == GameModeEnum.RhythmAction)
        {
            if (!GM.ValidateNodeDistance())
                return;
        }


        RotationPoint = GM.LPoint.transform;
        transform.SetParent(RotationPoint);
        cubeMoveAnimation.Play("CubeMoveLeft");
        inputFlag = false;
        invokeCnt = 0;
        InvokeRepeating("rotatingLeft", 0.0f, 1.0f / 160.0f / moveSpeed);
        LG.JustDisappear();
    }

    public void MoveRight()
    {
        if (!inputFlag)
        {
            jumpFlag = true;
            isRight = true;
            return;
        }
        if (GM.GameMode == GameModeEnum.RhythmAction)
        {
            if (!GM.ValidateNodeDistance())
                return;
        }

        RotationPoint = GM.RPoint.transform;
        transform.SetParent(RotationPoint);
        cubeMoveAnimation.Play("CubeMoveRight");
        inputFlag = false;
        invokeCnt = 0;
        InvokeRepeating("rotatingRight", 0.0f, 1.0f / 160.0f / moveSpeed);
        LG.JustDisappear();
    }

    private void rotatingLeft()
    {
        RotationPoint.rotation = Quaternion.Euler(moveSpeed * invokeCnt, -45.0f, 0.0f);
        invokeCnt++;

        if (invokeCnt > 180 / moveSpeed)
        {
            CancelInvoke("rotatingLeft");
            transform.parent = null;
            transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
            transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
            invokeCnt = 0;
            GM.DestroyPoints();
            GM.MakePoints();
            inputFlag = true;
            validateDirection(false);
            UM.AddScore();
            UM.GainPlayTime();

            Instantiate(VFXButterFly, gameObject.transform.position, Quaternion.identity);
            if (jumpFlag)
            {
                jumpFlag = false;
                if (isRight)
                    MoveRight();
                else
                    MoveLeft();
            }
        }
    }

    private void rotatingRight()
    {
        RotationPoint.rotation = Quaternion.Euler(moveSpeed * invokeCnt, 45.0f, 0.0f);
        invokeCnt++;

        if (invokeCnt > 180 / moveSpeed)
        {
            CancelInvoke("rotatingRight");
            transform.parent = null;
            transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
            transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
            invokeCnt = 0;
            GM.DestroyPoints();
            GM.MakePoints();
            inputFlag = true;
            validateDirection(true);
            UM.AddScore();
            UM.GainPlayTime();
            Instantiate(VFXButterFly, gameObject.transform.position, Quaternion.identity);
            if (jumpFlag)
            {
                jumpFlag = false;
                if (isRight)
                    MoveRight();
                else
                    MoveLeft();
            }
        }
    }


    private void validateDirection(bool isRight)
    {
        if(dirRightFlag != isRight)
        {
            /// TO DO:: 게임 오버 연출
            LG.GameOver();
            return;
        }
        DirValidationValue[DirValidationIndex]--;
        if(DirValidationValue[DirValidationIndex] == 0)
        {
            DirValidationIndex++;
            dirRightFlag = !dirRightFlag;
        }
    }


}
