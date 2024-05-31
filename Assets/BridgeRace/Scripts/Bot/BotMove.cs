using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UIElements;
using System.Drawing;
using Unity.VisualScripting;

public class BotMove : Character
{
    public NavMeshAgent agent;
    private Vector3 destination;

    public bool isAtDestination => Vector3.Distance(destination, transform.position.x * Vector3.right + Vector3.up * 0.0487628f + Vector3.forward * transform.position.z) < 0.1f;

    public float distance => Vector3.Distance(destination, transform.position.x* Vector3.right + Vector3.up* 0.0487628f + Vector3.forward* transform.position.z);
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Bot";
        destination = transform.position;
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        destination.y = 0.0487628f;
        agent.SetDestination(destination);
    }

    IState<BotMove> currentState;
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<BotMove> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        Debug.Log(currentState.GetType().Name);
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
