using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField]
    public float m_SpawnRate;

    public int m_NumberOfSpawn = 4;

    //[SerializeField]
    private GameObject m_Player;

    [SerializeField]
    private float spawnRadius = 14.0f;

    private bool canSpawn;
    public bool CanSpawn { get; set; }

    public string EnemyTag;

    private bool StartedSpawn = false;

    private void Awake()
    {
        m_Player = GameObject.FindWithTag("Player");

    }
    // Start is called before the first frame update
    void Update()
    {
        if(!StartedSpawn && CanSpawn)
        {
            StartCoroutine(Spawn());
            StartedSpawn = true;
        }
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(m_SpawnRate);
        while (CanSpawn)
        {
            yield return wait;
            for (int i = 0; i< m_NumberOfSpawn; ++i)
            {
                float angle = Random.Range(0f, 2f * Mathf.PI);
                float x = Mathf.Sin(angle);
                float y = Mathf.Cos(angle);

                Vector3 randomPosition = new Vector3(x, y, 0);
                Vector3 spawnPos = randomPosition * spawnRadius + m_Player.transform.position;
                GameObject enemy = ObjectPooler.Instance.SpawnFromPool(EnemyTag, spawnPos, Quaternion.identity);
                if (enemy != null)
                {
                    enemy.SetActive(true);
                }
                else
                {
                    //No spawning due to reaching maximum number
                }
            }
            
        }

    }
}
