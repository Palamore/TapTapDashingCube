using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmActionNode : MonoBehaviour
{

    private bool isLeft;
    public bool IsLeft
    {
        get { return isLeft; }
        set { isLeft = value; }
    }


    void Start()
    {
        Invoke("selfDestroy", 1.6f);
    }

    private void selfDestroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() //1초에 60번
    {
        if(isLeft)
        {
            transform.Translate(new Vector3(200.0f / 60.0f, 0.0f, 0.0f));
        }
        else
        {
            transform.Translate(new Vector3(-200.0f / 60.0f, 0.0f, 0.0f));
        }
    }
}
