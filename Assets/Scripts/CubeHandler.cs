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

    private Animation cubeMoveAnimation;
    private int invokeCnt;
    public Transform RotationPoint;
    private GameManager GM;
    private float moveSpeed = 2.0f;
    private bool inputFlag;

    void Start()
    {
        invokeCnt = 0;
        GM = GameManager.Instance();
        inputFlag = true;
    }

    void Awake()
    {
        cubeMoveAnimation = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputFlag)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                RotationPoint = GM.LPoint.transform;
                transform.SetParent(RotationPoint);
                cubeMoveAnimation.Play("CubeMoveLeft");
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                RotationPoint = GM.RPoint.transform;
                transform.SetParent(RotationPoint);
                cubeMoveAnimation.Play("CubeMoveRight");
                MoveRight();
            }
        }

    }

    public void MoveLeft()
    {
        inputFlag = false;
        invokeCnt = 0;
        InvokeRepeating("rotatingLeft", 0.0f, 1.0f / 320.0f);
    }

    public void MoveRight()
    {
        inputFlag = false;
        invokeCnt = 0;
        InvokeRepeating("rotatingRight", 0.0f, 1.0f / 320.0f);
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
        }
    }



}
