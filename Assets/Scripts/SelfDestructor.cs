using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestroy", 3.0f);
    }


    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

}
