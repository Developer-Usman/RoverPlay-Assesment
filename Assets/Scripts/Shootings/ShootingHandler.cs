using UnityEngine;

public class ShootingHandler : MonoBehaviour, IShoot, IGetFromPool
{
    [SerializeField] public CharacterType characterType;
    [SerializeField] KeyCode shootKey = KeyCode.None;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float speed = 1f;
    void Update()
    {
        if (Input.GetKeyDown(shootKey))
            StartShooting();
    }
    public void StartShooting()
    {
        GameObject bullet = GetFromPool();

        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = spawnPoint.rotation;

        Shake();

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = spawnPoint.forward * speed;
        }
    }
    void Shake()
    {
        if(characterType == CharacterType.Player)
        {
            CameraShaker.Instance.ShakeCamera();
        }
    }

    public GameObject GetFromPool()
    {
        return PoolManager.Instance.Get(PoolObjectType.Bullet);
    }
}
public enum CharacterType
{
    Enemy,
    Player
}