using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glowball : MonoBehaviour
{

    CubeHandler CH;

    private void Awake()
    {
        CH = CubeHandler.Instance();
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (transform.localScale.x <= 0.15f)
        {
            transform.localScale += Vector3.one * 0.01f;
        }
        transform.position = CH.gameObject.transform.position + new Vector3(0.0f, 0.1f, 0.0f);
    }


}
