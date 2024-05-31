using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bricks : ColorObjects
{
    public Stage stage;
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
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Character>().colorType == colorType)
            {
                stage.RemoveBricks(this);
            }
        }
        if (other.gameObject.tag == "Bot")
        {
            if (other.gameObject.GetComponent<BotMove>().colorType == colorType)
            {
                stage.RemoveBricks(this);
            }
        }
    }
}
