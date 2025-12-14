using UnityEngine;

public class DeactivateParticles : MonoBehaviour, IReturnToPool
{
    [SerializeField] private PoolObjectType poolObjectType;
    public float lifeTime = 2f;
    private void OnEnable()
    {
        Invoke("Deactivate", lifeTime);
    } 
    private void Deactivate()
    {
        ReturnToPool();
    }
    
    public void ReturnToPool() => PoolManager.Instance.ReturnToPool(poolObjectType, gameObject);
}

