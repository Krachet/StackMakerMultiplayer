using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProperties : Character
{
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        ChangeAnim("Idle");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 nextpos = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            if (CanMove(nextpos))
            {
                transform.position = Checkground(nextpos);
            }
            ChangeAnim("Run");

            if (JoystickControl.direct != Vector3.zero)
            {
                body.forward = JoystickControl.direct;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ChangeAnim("Idle");
        }

        if (Physics.Raycast(center.position, Vector3.down, out hit, 5f, brick))
        {
            if (hit.collider.gameObject.GetComponent<Bricks>().colorType != colorType)
            {
                ChangeAnim("Idle");
            }
        }
    }
}
