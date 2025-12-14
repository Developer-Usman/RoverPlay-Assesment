using DG.Tweening;
using UnityEngine;

public class DeactivateBullet : MonoBehaviour, IReturnToPool,IGetFromPool
{
    public float lifeTime = 2f;
    private Rigidbody rb;
    public GameObject hit;

    void Awake() => rb = GetComponent<Rigidbody>();
    private void OnEnable()
    {
        Invoke("Deactivate", lifeTime);
    } 
    private void Deactivate()
    {
        ReturnToPool();
        rb.velocity = Vector3.zero;
    }
    

    #region DamageTriggerHandling
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Health>(out var health))
        {
            GameObject hit = GetFromPool();
            hit.transform.position = other.transform.position;
            health.TakeDamage();
            ReturnToPool();

            if(other.gameObject.TryGetComponent<ShootingHandler>(out var shootingHandler))
            {
                if(shootingHandler.characterType == CharacterType.Enemy)
                {
                    Transform otherObjectTransform = other.transform;
                    otherObjectTransform.DOScale(1.2f,0.15f).SetEase(Ease.Linear).OnComplete(()=>
                    {
                        otherObjectTransform.DOScale(1,0.05f).SetEase(Ease.Linear).OnComplete(()=>
                        {
                            otherObjectTransform.localScale = Vector3.one;
                        });
                    });
                }
            }
        }
    }

    public void ReturnToPool() => PoolManager.Instance.ReturnToPool(PoolObjectType.Bullet, gameObject);
    public GameObject GetFromPool()
    {
        return PoolManager.Instance.Get(PoolObjectType.Hit);
    }
    #endregion
}

