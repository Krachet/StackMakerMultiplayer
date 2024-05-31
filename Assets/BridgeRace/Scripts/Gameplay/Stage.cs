using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using MarchingBytes;

public class Stage : MonoBehaviour
{
    public Transform[] brickPos;
    public List<Vector3> emptyPos = new List<Vector3>();
    public List<Bricks> bricks = new List<Bricks>();
    public List<Bricks> bricksToPool = new List<Bricks>();


    public Bricks brickPrefab;

    public Stage stage;

    private int totalBrick = 0;
    private int amountToPool = 110;



    void Start()
    {

    }

    internal Bricks SeekBrickPoint(ColorType colorType)
    {
        Bricks brick = null;
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                brick = bricks[i];
                break;
            }
        }
        return brick;
    }

    internal void OnInit(ColorType colorType)
    {
        for (int i = 0; i < brickPos.Length; i++)
        {
            emptyPos.Add(brickPos[i].position);
        }
        for (int i = 0; i < 5; i++)
        {
            NewBricks(colorType);
        }
    }

    public void NewBricks(ColorType colorType)
    {
        if (emptyPos.Count == 0)
        {
            return;
        }
        int ran = Random.Range(0, emptyPos.Count);  
        Bricks brick = EasyObjectPool.instance.GetObjectFromPool("Brick", emptyPos[ran], Quaternion.identity).GetComponent<Bricks>();
        brick.GetComponent<Bricks>().stage = this;
        brick.ChangeColor(colorType);
        emptyPos.RemoveAt(ran);
        bricks.Add(brick);
        
    }

    private ColorType RandomBricks(int ran)
    {
        switch (ran)
        {
            case 0:
                return ColorType.Black;
            case 1:
                return ColorType.Blue;
            case 2:
                return ColorType.Brown;
            case 3:
                return ColorType.Green;
            case 4:
                return ColorType.Orange;
            case 5:
                return ColorType.Red;
            case 6:
                return ColorType.Violet;
            case 7:
                return ColorType.Yellow;
            default:
                return ColorType.Default;
        }
    }

    public void RemoveBricks(Bricks brick)
    {
        emptyPos.Add(brick.transform.position); 
        bricks.Remove(brick);
        EasyObjectPool.instance.ReturnObjectToPool(brick.gameObject);
        brick.gameObject.SetActive(false);
        StartCoroutine(SpawnBricks(5f, brick.colorType));
    }

    public IEnumerator SpawnBricks(float delay, ColorType colorType)
    {
        yield return new WaitForSeconds(delay);
        NewBricks(colorType);
    }
}
