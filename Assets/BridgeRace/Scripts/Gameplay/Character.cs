using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : ColorObjects
{
    public float speed = 2f;
    public Transform body;
    public Transform center;
    public Transform forward;
    public LayerMask brick;
    public LayerMask notbridge;
    public LayerMask stair;
    public LayerMask ground;
    public Animator anim;
    public Stage stage;

    private RaycastHit hit;
    private string currentAnim;
    [SerializeField] public GameObject brickParent;
    [SerializeField] public PlayerBricks brickPrefabs;

    public PlayerBricks playerBrick;
    public List<PlayerBricks> playerBricks = new List<PlayerBricks>();

    public int brickCount => playerBricks.Count;
    enum State
    {
        Up,
        Down,
        Left,
        Right
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
    public bool CanMove(Vector3 point)
    {
        bool canmove = false;
        if (Physics.Raycast(point + Vector3.up, Vector3.down, Mathf.Infinity, ground))
        {
            RaycastHit hit;
            if (Physics.Raycast(point + Vector3.up, Vector3.down, out hit, Mathf.Infinity, stair))
            {
                if (hit.collider.gameObject.GetComponent<Stair>().colorType == colorType)
                {
                    canmove = true;
                }
                else
                {
                    if (playerBricks.Count > 0)
                    {
                        hit.collider.gameObject.GetComponent<Stair>().ChangeColor(colorType);
                        RemoveBrick();
                    }
                    canmove = false;
                }
            }
            else
            {
                canmove = true;
            }
        }
        return canmove;
    }
    public Vector3 Checkground(Vector3 point)
    {
        // Cau thang
        if (Physics.Raycast(point, Vector3.down, out hit, Mathf.Infinity, ground))
        {
            return hit.point + Vector3.up * 0.2f;
        }
        // mat dat bthg
        return point;
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(animName);
        }
    }

    public void AddBrick()
    {
        PlayerBricks playerBrick = EasyObjectPool.instance.GetObjectFromPool("PlayerBrick", brickParent.transform.position, Quaternion.identity).GetComponent<PlayerBricks>();
        playerBrick.gameObject.transform.SetParent(brickParent.transform);
        playerBrick.ChangeColor(this.colorType);
        playerBrick.transform.localPosition = new Vector3(0f, 0.3f * (playerBricks.Count - 1), 0f);
        playerBrick.transform.localRotation = Quaternion.identity;
        playerBricks.Add(playerBrick);
    }

    public void RemoveBrick()
    {
        if (playerBricks.Count > 0)
        {
            PlayerBricks playerBrick = playerBricks[playerBricks.Count - 1];
            playerBricks.RemoveAt(playerBricks.Count - 1);
            EasyObjectPool.instance.ReturnObjectToPool(playerBrick.gameObject);
            playerBrick.gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bricks")
        {
            if (other.GetComponent<Bricks>().colorType == colorType)
            {
                AddBrick();
            }
        }
        if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene(1);
        }
    }
}
