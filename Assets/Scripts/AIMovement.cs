using UnityEngine; 
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class AIMovement : MonoBehaviour { 
    public Transform target; 
    private NavMeshAgent agent; 
    private Animator animator;
    public AudioSource howl;
    public AudioSource screech;

    public float walking_speed = 2f;
    public float running_speed = 4f;

    public float death_distance = 1.5f;
    //public float frenzy_distance = 10f;
    public float giveup_distance = 12f;

    [Range(0, 20f)]
    public float freq = 10f;

    bool isTriggered = false;
    public AudioSource walking_footstep;
    public AudioSource running_footstep;

    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    public float visionRange = 10f;
    public float fieldOfViewAngle = 60f;
    public LayerMask obstructionMask;

    float sine;

    void Start() { 
        
        agent = GetComponent<NavMeshAgent>(); 
        animator = GetComponentInChildren<Animator>();  // Looks in child objects too
        howl.Play(); 
        GoToNextWaypoint();
        StartFootsteps();

    } 
    void Update() { 

        // Get current animation state
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // If idle, stop movement
        if (!stateInfo.IsName("walking") && !stateInfo.IsName("running"))
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero; // Ensure no residual movement
        }
        else
        {
            agent.isStopped = false;
            StartFootsteps();
            if (stateInfo.IsName("running")) {
                agent.SetDestination(target.position); // Move toward player or target
            } else if (!agent.pathPending && agent.remainingDistance < 0.5f) { 
                GoToNextWaypoint();
            }
        }

        sine = Mathf.Sin(Time.time * freq);
        float distance = Vector3.Distance(this.transform.position, target.position);

        if (distance < death_distance)
        {   
            animator.Play("killing");
            SceneManager.LoadScene("LoseMenu");

            //frenzy and chase
        } else if (stateInfo.IsName("walking") && CanSeePlayer())
        {
            animator.Play("frenzing");
            screech.Play();
            agent.speed = running_speed;

            //give up
        } else if (stateInfo.IsName("running") && distance > giveup_distance && !CanSeePlayer())
        {
            animator.Play("walking");
            agent.speed = walking_speed;
        }
    }

    public void StopFollowing()
    {
        // Stop the NavMeshAgent from moving
        agent.isStopped = true;
    }

    public void StartFootsteps()
    {
        if (sine > 0.97f && isTriggered == false)
        {
            isTriggered = true;
            walking_footstep.Play();
        }
        else if (isTriggered && sine < -0.97f)
        {
            isTriggered = false;
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }


    public bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (target.transform.position - this.transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(this.transform.position, target.transform.position);

        // Check if the player is within vision range
        if (distanceToPlayer > visionRange)
        {
            return false;
        }

        // Check if the player is within the field of view
        float angleToPlayer = Vector3.Angle(this.transform.forward, directionToPlayer);
        if (angleToPlayer > fieldOfViewAngle / 2)
        {
            return false;
        }
        // Use a raycast to check if there are obstacles blocking the view
        if (Physics.Raycast(this.transform.position, directionToPlayer, out RaycastHit hit, visionRange, obstructionMask))
        {
            if (hit.transform != this.transform)
            {
                return false; // Something is blocking the view
            }
        }

        return true;
    }
}