using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New EnemyScriptableObject", menuName = "EnemyScriptableObject")]
public class EnemyScriptableObject : ScriptableObject
{
    public RuntimeAnimatorController m_Animator;
    public string m_EnemyName;
    public int m_Hp;
    public float m_Exp;
    public float m_BaseSpeed;
    public float m_MaxSpeed;
    public float m_Acceleration;
}
