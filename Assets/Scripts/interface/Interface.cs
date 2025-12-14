using UnityEngine;

interface IShoot
{
    void StartShooting();
}
interface IGetFromPool
{
    GameObject GetFromPool();
}
interface IReturnToPool
{
    void ReturnToPool();
}