using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{

    [SerializeField] Material[] colorMats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Material GetColorMat(ColorType colorType)
    {
        return colorMats[(int)colorType];
    }
}
