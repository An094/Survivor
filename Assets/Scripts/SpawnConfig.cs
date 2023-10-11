using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config", menuName = "SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    public List<RoundConfig> m_RoundConfigs;
}
