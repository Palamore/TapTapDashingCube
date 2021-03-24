using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Camera MainCamera = null;
    public GameObject LPointPrefab;
    public GameObject RPointPrefab;
    private GameObject cube;

    public GameObject LPoint;
    public GameObject RPoint;

    public Transform CameraTransform;



    // Start is called before the first frame update
    void Start()
    {
        cube = CubeHandler.Instance().gameObject;
        CameraTransform = MainCamera.transform;
        MakePoints();
    }

    // Update is called once per frame
    void Update()
    {
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


}
