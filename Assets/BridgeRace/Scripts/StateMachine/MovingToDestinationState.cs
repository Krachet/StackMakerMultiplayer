using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToDestination : IState<BotMove>
{
    //GO TO DESTINATION
    public void OnEnter(BotMove t)
    {
        Debug.Log("MovingToDestination");
        t.SetDestination(LevelManager.Ins.EndPos);
        if (t.brickCount < 5)
        {
            t.ChangeState(new PatrolState());
        }
    }

    //CHECK IF CAN GO TO DESTINATION
    public void OnExecute(BotMove t)
    {
        if (t.brickCount == 0)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(BotMove t)
    {

    }

}
