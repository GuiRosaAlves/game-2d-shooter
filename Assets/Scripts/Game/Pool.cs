using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Pool
{
    [SerializeField] public GameObject Prefab { get; private set; }
    [SerializeField] public int Size { get; private set; }
    public List<GameObject> PoolObjs;
}
