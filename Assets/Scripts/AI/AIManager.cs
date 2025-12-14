using TMPro;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AIAttributes : MonoBehaviour
{
    protected int navMeshAreaMask = NavMesh.AllAreas;
    public Transform player;
    protected float spawnRadius = 10f;
    // Killings
    public int maxEnemiesToKill = 5;
    public int enemiesKilled = 0;

    // Enemies Active
    [SerializeField] protected int maxActive, currentActive;
    [SerializeField] protected TMP_Text maxEnemiesToKilltxt,enemiesKilledtxt;
}
public class AIManager : AIAttributes, IGetFromPool
{
    public static AIManager Instance;
    void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        spawnRadius = 50f;
        maxEnemiesToKill = Random.Range(5, 10);
        maxEnemiesToKilltxt.text = "MAX : " + maxEnemiesToKill.ToString();
    }
    void Start()
    {
        ActiveEnemies();
    }
    public void CheckWin()
    {
        enemiesKilled++;
        currentActive--;
        enemiesKilledtxt.text = "KILLED : " + enemiesKilled.ToString();
        if (enemiesKilled >= maxEnemiesToKill)
        {
            UIManager.Instance.OpenWinPenal();
            Time.timeScale = 0;
        }
        ActiveEnemies();
    }
    void ActiveEnemies()
    {
        if(currentActive < maxActive)
        {
            int _i = maxActive - currentActive;
            for (int i = 0; i < _i; i++)
            {
                Spawn();
            }
        }
    }

    public GameObject GetFromPool()
    {
        return PoolManager.Instance.Get(PoolObjectType.Enemy);
    }
    public void SpawnEnemy(Vector3 desiredPosition)
    {
        if (TryGetNearestNavMeshPoint(desiredPosition, out Vector3 spawnPoint))
        {
            GameObject enemy = GetFromPool();
            enemy.transform.position = spawnPoint;
            enemy.transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("No NavMesh point found for enemy spawn.");
        }
    }

    private bool TryGetNearestNavMeshPoint(Vector3 position, out Vector3 result)
    {
        if (NavMesh.SamplePosition(position, out NavMeshHit hit, spawnRadius, navMeshAreaMask))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    public void Spawn()
    {
        Vector3 randomPoint = player.position + Random.insideUnitSphere * 15f;
        SpawnEnemy(randomPoint);
        currentActive++;
    }
}

