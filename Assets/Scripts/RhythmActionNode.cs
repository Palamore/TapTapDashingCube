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
        Invoke("selfDestroy", 3.2f);
    }

    private void selfDestroy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeft)
        {
            transform.Translate(new Vector3(100.0f / 60.0f, 0.0f, 0.0f));
        }
        else
        {
            transform.Translate(new Vector3(-100.0f / 60.0f, 0.0f, 0.0f));
        }
    }
}
