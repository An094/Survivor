using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New SkillScriptableObject", menuName = "SkillScriptableObject")]
public class SkillScriptableObject : ScriptableObject
{
    public Sprite m_Sprite;
    //public string m_Name;
    public string SkillName;
    public string SkillDescription;
}
