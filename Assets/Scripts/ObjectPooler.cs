using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> m_Pools;
    public Dictionary<string, List<GameObject>> m_PoolDictionary;


    // Start is called before the first frame update
    void Start()
    {
        m_PoolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in m_Pools)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            m_PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!m_PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        for (int i = 0; i < m_PoolDictionary[tag].Count; i++)
        {
            if (!m_PoolDictionary[tag][i].activeInHierarchy)
            {
                GameObject objectToSpawn = m_PoolDictionary[tag][i];
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
                IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
                if (pooledObject != null)
                {
                    pooledObject.OnObjectSpawn();
                }

                return objectToSpawn;
            }
        }
        return null;
    }
}