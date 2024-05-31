using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStageBox : MonoBehaviour
{
    public Stage stage;

    public List<ColorType> colorTypes = new List<ColorType>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!colorTypes.Contains(other.gameObject.GetComponent<ColorObjects>().colorType))
        {
            other.gameObject.GetComponent<Character>().stage = stage;
            colorTypes.Add(other.gameObject.GetComponent<ColorObjects>().colorType);
            stage.OnInit(other.gameObject.GetComponent<ColorObjects>().colorType);
        }
    }
}
