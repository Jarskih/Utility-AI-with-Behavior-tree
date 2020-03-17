using BehaviorTrees;
using UnityEngine;
using UnityEngine.AI;

[Leaf(typeof(FindPath))]
public class FindPath : BehaviorTreeNode
{
    public PathType pathType;

    public float repathTolerance = 2;
    public float repathCount = 10;
    public float minWanderDistance = 10;
    public float maxWanderDistance = 15;

    public override BehaviorTreeResult Execute()
    {
        switch (pathType)
        {
            case PathType.TARGET:
                if (agent.Owner.currentTarget != null && !agent.navAgent.pathPending && agent.navAgent.enabled)
                {
                    SetDestinationNearTarget(agent.Owner.currentTarget.GetPosition());
                }
                break;
            case PathType.RANDOM:
                if (!agent.navAgent.pathPending && agent.navAgent.enabled)
                {
                    SetRandomDestination(agent.navAgent);
                }
                break;
            case PathType.EnemyTarget:
                if (agent.Owner.blackboard.enemyTarget != null && !agent.navAgent.pathPending && agent.navAgent.enabled)
                {
                    SetDestinationNearTarget(agent.Owner.blackboard.enemyTarget.GetPosition());
                }
                break;
            case PathType.FriendlyTarget:
                if (agent.Owner.blackboard.friendlyTarget != null && !agent.navAgent.pathPending && agent.navAgent.enabled)
                {
                    SetDestinationNearTarget(agent.Owner.blackboard.friendlyTarget.GetPosition());
                }
                break;
            default:
                break;
        }

        return agent.navAgent.hasPath || agent.navAgent.pathPending ? BehaviorTreeResult.Success : BehaviorTreeResult.Failure;
       
    }

    private void SetRandomDestination(NavMeshAgent agent)
    {
        float radius = Random.Range(minWanderDistance, maxWanderDistance);
        Vector3 randomPosition = Random.insideUnitSphere * radius;
        randomPosition += agent.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, radius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    //Tries RepathCount times to find a path near the player, increasing the radius each time. 
    //This is needed in case the player is not on valid Navmesh
    private void SetDestinationNearTarget(Vector3 pos)
    {
        NavMeshHit hit;
        float radius = 0;
        for (int i = 0; i < repathCount; ++i)
        {
            Vector3 randomPosition = Random.insideUnitSphere * radius;
            randomPosition += pos;
            if (NavMesh.SamplePosition(randomPosition, out hit, radius, 1))
            {
                agent.navAgent.SetDestination(hit.position);
                break;
            }
            else
            {
                ++radius;
            }
        }
    }
}
