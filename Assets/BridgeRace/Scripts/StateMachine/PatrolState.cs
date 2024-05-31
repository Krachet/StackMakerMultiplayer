using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<BotMove>
{
    int targetBrick;    
    public void OnEnter(BotMove t)
    {
        Debug.Log("PatrolState");
        t.ChangeAnim("Run");
        targetBrick = 7;   
        SeekTarget(t);
    }

    public void OnExecute(BotMove t)
    {
        if (t.isAtDestination)
        {   
            if (t.brickCount >= targetBrick)
            {
                t.ChangeState(new MovingToDestination()); 
            }
            else
            {
                SeekTarget(t);
            }
        }
    }

    public void OnExit(BotMove t)
    {

    }

    public void SeekTarget(BotMove t)
    {
        if (t.stage != null)
        {
            Bricks brick = t.stage.SeekBrickPoint(t.colorType);
            if (brick == null)
            {
                t.ChangeState(new MovingToDestination());
            }
            else
            {
                t.SetDestination(brick.transform.position);
            }
        }
        else
        {
            t.SetDestination(t.transform.position);
        }
    }

}
