using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private SpawnConfig m_Config;
    [SerializeField]
    private GameObject m_EnemySpawner;
    private float m_CurrentTime;
    private List<EnemySpawner> m_Spawners;
    private void Start()
    {
        m_Spawners = new List<EnemySpawner>();
        for (int i = 0; i < m_Config.m_RoundConfigs.Count; ++i)
        {
            EnemySpawner spawner = Instantiate(m_EnemySpawner, transform.position, Quaternion.identity).GetComponent<EnemySpawner>();
            RoundConfig roundConfig = m_Config.m_RoundConfigs[i];
            spawner.m_SpawnRate = 1.0f / roundConfig.m_SpawnCD;
            spawner.m_NumberOfSpawn = roundConfig.m_NumberOfSpawn;
            spawner.EnemyTag = roundConfig.m_EnemyTag;
            m_Spawners.Add(spawner);
        }
    }

    private void Update()
    {
        m_CurrentTime += Time.deltaTime;
        List<RoundConfig> roundConfig = m_Config.m_RoundConfigs;
        for (int i = 0; i < roundConfig.Count; ++i)
        {
            float startTime = roundConfig[i].m_StartTime;
            float endTime = startTime + roundConfig[i].m_Duration;
            if (m_CurrentTime >= startTime && m_CurrentTime < endTime && !m_Spawners[i].CanSpawn)
            {
                m_Spawners[i].CanSpawn = true;
            }
        }
    }
}
