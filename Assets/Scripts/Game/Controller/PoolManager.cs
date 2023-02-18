using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Pool _playerProjectilePool;
    [SerializeField] private Pool _shooterProjectilePool;
    [SerializeField] private Pool _chaserShips;
    [SerializeField] private Pool _shooterShips;

    private void Awake()
    {
        InstantiatePool(_playerProjectilePool);
        InstantiatePool(_shooterProjectilePool);
        InstantiatePool(_chaserShips);
        InstantiatePool(_shooterShips);
    }


    private void InstantiatePool(Pool pool)
    {
        for (var i = 0; i < pool.Size; i++)
        {
            AddToPool(pool, Vector3.zero, Quaternion.identity);
        }
    }

    public GameObject EnableFromPool(PoolTags tag, Vector3 position, Quaternion rotation)
    {
        GameObject poolObj = null;
        switch (tag)
        {
            case PoolTags.PlayerProjectile:
                poolObj = FindUnactive(_playerProjectilePool);
                if (!poolObj)
                {
                    poolObj = AddToPool(_playerProjectilePool, position, rotation);
                }
                poolObj.SetActive(true);
                break;
            case PoolTags.ShooterProjectile:
                poolObj = FindUnactive(_playerProjectilePool);
                poolObj.SetActive(true);
                break;
            case PoolTags.ChaserShips:
                poolObj = FindUnactive(_playerProjectilePool);
                poolObj.SetActive(true);
                break;
            case PoolTags.ShooterShips:
                poolObj = FindUnactive(_playerProjectilePool);
                poolObj.SetActive(true);
                break;
        }
        return poolObj;
    }
    public void DisableFromPool(GameObject poolObj)
    {
        poolObj.SetActive(false);
    }

    private GameObject FindUnactive(Pool pool)
    {
        foreach (var poolObj in pool.PoolObjs)
        {
            if (!poolObj.activeInHierarchy)
            {
                return poolObj;
            }
        }
        return null;
    }

    private GameObject AddToPool(Pool pool, Vector3 position, Quaternion rotation)
    {
        var newObj = Instantiate(pool.Prefab, position, rotation);
        pool.PoolObjs.Add(newObj);
        newObj.SetActive(false);
        return newObj;
    }
}
