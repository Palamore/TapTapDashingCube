using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifRenderer : MonoBehaviour
{

    public Sprite[] Sprites;
    private int index = 0;
    private Image targetImage;
    private float Iteration = 0.0f;

    private void Awake()
    {
        targetImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Render", 0.0f, 1.0f / 30.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Render()
    {
        targetImage.sprite = Sprites[index++];
        if (index >= Sprites.Length) index = 0;
    }

}
