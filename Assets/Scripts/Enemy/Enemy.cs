using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Chase,
    Attack,
    Die
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private Animator animator;
    private float attackRange = 3f;
    private float killRange = 1f;
    private float rayDistance = 1f;
    private float stopDistance = 1.5f;
    [SerializeField] private float forceSpeed;
    private bool playerTouch;

    public GameObject playerGO;

    private Vector3 destination;
    private Quaternion rotation;
    private Vector3 direction;
    public EnemyState currentState;


    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Wander:
                

                if (NeedsDestination())
                {
                    GetDestination();
                }

                transform.rotation = rotation;
                transform.Translate(Vector3.forward * Time.deltaTime * 5f);

                var rayColor = IsPathBlocked() ? Color.red : Color.green;

                while (IsPathBlocked())
                {
                   
                    GetDestination();
                }

                //var targetToAggro = CheckForAggro();
                if (CheckForAggro() == playerGO)
                {
                    //player = targetToAggro.GetComponent<Player>();
                    currentState = EnemyState.Chase;
                }

                break;
            case EnemyState.Chase:


                if (playerGO == null)
                {
                    currentState = EnemyState.Wander;
                    return;
                }

                transform.LookAt(playerGO.transform);
                transform.Translate(Vector3.forward * Time.deltaTime * 5f);

                if (Vector3.Distance(transform.position, playerGO.transform.position) < attackRange)
                {
                    currentState = EnemyState.Attack;
                }

                break;
            case EnemyState.Attack:


                if (playerGO != null)
                {
                    if (Vector3.Distance(transform.position, playerGO.transform.position) < killRange)
                    {
                        StartCoroutine(AttackAnim());
                        
                    }
                }

                currentState = EnemyState.Chase;

                break;

            case EnemyState.Die:
                Throw();

                Destroy(gameObject, 1f);
                break;
        }
    }

    IEnumerator AttackAnim()
    {
        animator.SetBool("enemyAttack", true);
        yield return new WaitForSeconds(1f);
        GameManager.LOSE = true;
        animator.SetBool("enemyAttack", false);
    }

    private void Throw()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Vector3 force = transform.forward;
        force = new Vector3(force.x, 1, force.z);
        rb.AddForce(force * forceSpeed);
    }


    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        var hitSomething = Physics.RaycastAll(ray, rayDistance, layerMask);
        return hitSomething.Any();
    }

    private void GetDestination()
    {
        Vector3 blockPos = (transform.position + (transform.forward * 4f)) + new Vector3(Random.Range(-4.5f, 4.5f),
            0,
            Random.Range(-4.5f, 4.5f));

        if (!IsPathBlocked())
        {
            destination = new Vector3(playerGO.transform.position.x, 1f, playerGO.transform.position.z);
        }
        else
        {
            destination = new Vector3(blockPos.x, 1f, blockPos.z);
        }

        direction = Vector3.Normalize(destination - transform.position);
        direction = new Vector3(direction.x, 0, direction.z);
        rotation = Quaternion.LookRotation(direction);
    }

    private bool NeedsDestination()
    {
        if (destination == Vector3.zero)
        {
            return true;
        }

        var distance = Vector3.Distance(transform.position, destination);

        if (distance <= stopDistance)
        {
            return true;
        }
        return false;
    }

    

    private Transform CheckForAggro()
    {
        Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
        Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

        float radius = 5f;

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = playerGO.transform.position;
        var pos = transform.position;
        for (int i = 0; i < 24; i++)
        {
            Ray ray = new Ray(pos, direction);
            
            
            if (Physics.Raycast(ray, out hit, radius))
            {
                var playerHit = hit.collider.tag;

                
                if (playerHit  == "Player")
                {
                    currentState = EnemyState.Chase;
                    return playerGO.transform;
                }
               
            }

            direction = stepAngle * direction;
        }

        return null;
    }

}
