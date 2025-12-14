using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] private List<PoolItem> itemsToPool;

    private Dictionary<PoolObjectType, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<PoolObjectType, Queue<GameObject>>();

        foreach (var item in itemsToPool)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < item.initialSize; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.transform.SetParent(item.parent);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            poolDictionary.Add(item.type, objPool);
        }
    }

    public GameObject Get(PoolObjectType type)
    {
        Queue<GameObject> pool = poolDictionary[type];

        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        PoolItem item = itemsToPool.Find(i => i.type == type);
        GameObject newObj = Instantiate(item.prefab);
        newObj.transform.SetParent(item.parent);
        return newObj;
    }

    public void ReturnToPool(PoolObjectType type, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[type].Enqueue(obj);
    }
}
[System.Serializable]
public class PoolItem
{
    public PoolObjectType type;
    public GameObject prefab;
    public int initialSize;
    public Transform parent;
}
public enum PoolObjectType
{
    Enemy,
    Bullet,
    Hit,
    Kill
}