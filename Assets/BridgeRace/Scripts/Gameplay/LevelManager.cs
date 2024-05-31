using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ColorType
{
    Default,
    Black,
    Red,
    Blue,
    Yellow,
    Orange,
    Brown,
    Green,
    Violet
}
public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() { ColorType.Black, ColorType.Blue, ColorType.Brown, ColorType.Green, ColorType.Orange, ColorType.Red, ColorType.Violet, ColorType.Yellow };
    
    public Transform startPoint;
    public Transform[] endPoint;
    public PlayerProperties player;
    public int CharacterAmount => botAmount + 1;
    public int botAmount;

    public int currentDestination = 0;
    public Vector3 EndPos => endPoint[currentDestination].position;

    private List<BotMove> bots = new List<BotMove>();
    [SerializeField] private GameObject botPrefab;

    public NavMeshData navMeshData;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }


    public void OnInit()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        //position
        List<Vector3> startPoints = new List<Vector3>();
        for (int i = 0; i < CharacterAmount; i++)
        {
            startPoints.Add(startPoint.position + Vector3.right * 3f * i);
        }

        //player
        List<ColorType> color = colorTypes;
        int RandomColor = Random.Range(0, color.Count);
        player.ChangeColor(color[RandomColor]);
        color.RemoveAt(RandomColor);

        int RandomStartPoint = Random.Range(1, CharacterAmount);
        player.transform.position = startPoints[RandomStartPoint];
        startPoints.RemoveAt(RandomStartPoint);

        //bot
        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            int randomColor = Random.Range(1, color.Count);
            BotMove bot = Instantiate(botPrefab, startPoints[i], Quaternion.identity).GetComponent<BotMove>();
            bot.ChangeColor(color[randomColor]);
            color.RemoveAt(randomColor);
            bot.ChangeState(new PatrolState());
            bots.Add(bot);
        }
    }


}
