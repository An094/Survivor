using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Round", menuName = "RoundConfig")]
public class RoundConfig : ScriptableObject
{
    public string m_EnemyTag;
    public int m_Max;
    public int m_NumberOfSpawn;
    public float m_SpawnCD;
    public float m_StartTime;
    public float m_Duration;
}

