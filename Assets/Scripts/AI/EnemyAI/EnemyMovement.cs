using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    private Animator Animator;
    public float UpdateRate = 0.1f;
    private NavMeshAgent Agent;
    private const string IsWalking = "Run";
    private const string Landed = "Idle";
    private Coroutine _followCoroutine;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private void Update()
        {  
            Animator.SetBool(IsWalking, Agent.velocity.magnitude > 0.01f);
        }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);
        
        while(enabled)
        {
            Agent.SetDestination(Player.transform.position);
            yield return Wait;
        }
    }

    public void StartChasing()
    {
        if (_followCoroutine==null)
        {
            _followCoroutine=StartCoroutine(FollowTarget());
        }
    }
}