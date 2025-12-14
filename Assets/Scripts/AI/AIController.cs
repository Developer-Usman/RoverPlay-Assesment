using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;

    [Header("AI Settings")]
    [SerializeField] private float chaseRange = 15f;
    [SerializeField] private float shootingRange = 8f;
    [SerializeField] private float shootCooldown = 1f;

    // INTERFACE
    private IShoot Ishoot;
    private float lastShootTime;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Ishoot = GetComponent<IShoot>();
    }

    private void Update()
    {
        if (!player) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // CHASE LOGIC
        if (distance <= chaseRange)
        {
            agent.SetDestination(player.position);

            // Turn toward player smoothly
            Vector3 dir = (player.position - transform.position).normalized;
            dir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
        }
        else
        {
            agent.ResetPath();
        }

        // SHOOT LOGIC
        if (distance <= shootingRange)
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (Time.time - lastShootTime < shootCooldown)
            return;

        lastShootTime = Time.time;
        
        if(Ishoot!=null)
            Ishoot.StartShooting();
        else
            Debug.LogError("IShoot is Missing");
    }
}
