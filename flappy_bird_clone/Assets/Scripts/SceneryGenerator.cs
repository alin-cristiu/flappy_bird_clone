using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryGenerator : MonoBehaviour
{
    private const float INIT_FIRST_BUSH_POSITION = -6;

    [SerializeField] private int _startBushCount;
    [SerializeField] private GameObject _bushPrefab;
    [SerializeField] private Transform _bushContainer;
    [SerializeField] private int _startPipeCount;
    [SerializeField] private List<GameObject> _pipePrefabs = new List<GameObject>();
    [SerializeField] private Transform _pipeContainer;

    private ObjectPool _bushPool;
    private List<ObjectPool> _pipePool = new List<ObjectPool>();
    private float _firstBushPosition = INIT_FIRST_BUSH_POSITION;

    public void InitPools()
    {
        _bushPool = new ObjectPool(OnBushSpawn, OnBushRetrieve, OnBushReturn, _bushContainer);
        _bushPool.CreatePooledItem(_bushPrefab, _startBushCount);
        for (int i = 0; i < _pipePrefabs.Count; i++)
        {
            _pipePool.Add(new ObjectPool(OnPipeSpawn, OnPipeRetrieve, OnPipeReturn, _pipeContainer));
            _pipePool[_pipePool.Count - 1].CreatePooledItem(_pipePrefabs[i], _startPipeCount);
        }

        Events.Instance.OnPipeIsOutOfScreen = OnPipeIsOutOfScreen;
        Events.Instance.OnBushIsOutOfScreen = OnBushIsOutOfScreen;
    }

    public void ResetFirstPositon()
    {
        _firstBushPosition = INIT_FIRST_BUSH_POSITION;
    }

    public void GenerateBush(float bushOffset)
    {
        GameObject currentBush = _bushPool.GetObject(_bushPrefab);
        currentBush.transform.position = new Vector3(_firstBushPosition, currentBush.transform.position.y, currentBush.transform.position.z);

        _firstBushPosition += bushOffset;
    }

    public float GetFirstBushPosition()
    {
        return _firstBushPosition;
    }

    private void OnBushSpawn(GameObject go)
    {
        Debug.Log("Bush spawn");
    }

    private void OnBushRetrieve(GameObject go)
    {
        Debug.Log("Bush retrive");
        go.SetActive(true);
    }

    private void OnBushReturn(GameObject go)
    {
        Debug.Log("Bush return");
        go.SetActive(false);
    }

    private void OnPipeSpawn(GameObject go)
    {
        Debug.Log("Pipe spawn");
    }

    private void OnPipeRetrieve(GameObject go)
    {
        Debug.Log("Pipe retrieve");
        go.SetActive(true);
    }

    private void OnPipeReturn(GameObject go)
    {
        Debug.Log("Pipe return");
        go.SetActive(false);
    }

    public int GetMaximumNumberOfPipeTypes()
    {
        return _pipePrefabs.Count;
    }

    public GameObject GetPipeOfType(int pipeType)
    {
        return _pipePool[pipeType].GetObject(_pipePrefabs[pipeType]);
    }

    private void OnPipeIsOutOfScreen(Pipe pipe)
    {
        _pipePool[pipe.PipeTypeIndex].ReturnObject(pipe.gameObject);
    }

    private void OnBushIsOutOfScreen(Bush bush)
    {
        _bushPool.ReturnObject(bush.gameObject);
    }
}
