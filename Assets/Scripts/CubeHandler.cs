﻿using System.Collections;
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

    public int[] DirValidationValue = new int[generateIteration];

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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter!");
    }

    public void MoveLeft()
    {
        if (!inputFlag)
        {
            jumpFlag = true;
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
        LG.DestroyGarbageNode();
    }

    public void MoveRight()
    {
        if (!inputFlag)
        {
            jumpFlag = true;
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
        LG.DestroyGarbageNode();
    }

    private void rotatingLeft()
    {
        RotationPoint.rotation = Quaternion.Euler(moveSpeed * invokeCnt, -45.0f, 0.0f);
        invokeCnt++;

        if (invokeCnt >= 180 / moveSpeed)
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

            if(jumpFlag)
            {
                jumpFlag = false;
                MoveLeft();
            }
        }
    }

    private void rotatingRight()
    {
        RotationPoint.rotation = Quaternion.Euler(moveSpeed * invokeCnt, 45.0f, 0.0f);
        invokeCnt++;

        if (invokeCnt >= 180 / moveSpeed)
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
        }
        if (jumpFlag)
        {
            jumpFlag = false;
            MoveRight();
        }
    }


    private void validateDirection(bool isRight)
    {
        if(dirRightFlag != isRight)
        {
            /// TO DO:: 게임 오버 연출
            Debug.Log("Game Over!");
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
