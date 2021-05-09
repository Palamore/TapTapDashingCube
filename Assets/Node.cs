using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public GameObject VFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
    }

    public void MakeEffect()
    {
        Instantiate(VFX, gameObject.transform.position, Quaternion.identity);
    }

}
