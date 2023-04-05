using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Action<GameObject> m_OnObjectSpawn = null;
    private Action<GameObject> m_OnObjectRetrieve = null;
    private Action<GameObject> m_OnObjectReturn = null;

    private Transform _pooledObjectsParent;
    private List<GameObject> _pooledObjects = new List<GameObject>();

    public ObjectPool(Action<GameObject> OnObjectSpawn, Action<GameObject> OnObjectRetrieve, Action<GameObject> OnObjectReturn)
    {
        m_OnObjectSpawn = OnObjectSpawn;
        m_OnObjectRetrieve = OnObjectRetrieve;
        m_OnObjectReturn = OnObjectReturn;
    }

    public ObjectPool(Action<GameObject> OnObjectSpawn, Action<GameObject> OnObjectRetrieve, Action<GameObject> OnObjectReturn, Transform parent) : this(OnObjectSpawn, OnObjectRetrieve, OnObjectReturn)
    {
        _pooledObjectsParent = parent;
    }


    public void CreatePooledItem(GameObject go, int startAmmout)
    {
        for (int i = 0; i < startAmmout; i++)
        {
            GameObject spawnedObject = SpawnObject(go);
            spawnedObject.SetActive(false);
        }
    }

    public GameObject GetObject(GameObject go)
    {
        var obj = _pooledObjects.FirstOrDefault();

        if (obj == null)
        {
            obj = SpawnObject(go);
        }

        if (obj != null)
        {
            _pooledObjects.Remove(obj);
            m_OnObjectRetrieve?.Invoke(obj);
        }

        return obj;
    }

    public void ReturnObject(GameObject go)
    {
        if (_pooledObjects.Contains(go))
            return;

        _pooledObjects.Add(go);
        m_OnObjectReturn?.Invoke(go);
    }

    private GameObject SpawnObject(GameObject prefab)
    {
        _pooledObjects.Add(Instantiate(prefab, _pooledObjectsParent));

        m_OnObjectSpawn?.Invoke(_pooledObjects[_pooledObjects.Count - 1]);

        return _pooledObjects[_pooledObjects.Count - 1];
    }
}
