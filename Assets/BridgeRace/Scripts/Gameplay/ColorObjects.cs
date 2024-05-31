using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObjects : MonoBehaviour
{   
    public ColorType colorType;
    [SerializeField] private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = ColorController.Ins.GetColorMat(colorType);
     }
}
