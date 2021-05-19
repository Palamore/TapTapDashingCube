using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverActionGimmick : MonoBehaviour
{

    public GameObject Ring;
    private float scale;
    private float speed = 90.0f;

    UIManager UM;

    private void Awake()
    {
        UM = UIManager.Instance();
    }

    private void OnEnable()
    {
        scale = 1.0f;
        Ring.transform.localScale = Vector3.one;
        speed -= 2.0f;
    }

    private void Update()
    {
        scale -= 1.0f / speed;
        Ring.transform.localScale = Vector3.one * scale;
        if(scale <= 0.0f)
        {
            UM.FailFeverAction();
            gameObject.SetActive(false);
        }
    }


    public void RingTouched()
    {
        gameObject.SetActive(false);
        if (Ring.transform.localScale.x >= 0.25f && Ring.transform.localScale.x <= 0.4f)
        {
            UM.SuccessFeverAction();
            Debug.Log("Success!");
            return;
        }
        Debug.Log("Fail!");
        UM.FailFeverAction();
    }


}
