using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradPopupManager : MonoBehaviour
{
    [SerializeField]
    private Image m_Skill1Image;

    [SerializeField]
    private Image m_Skill2Image;

    [SerializeField]
    private Text m_SkillName;

    [SerializeField]
    private Text m_SkillDescription;

    [SerializeField]
    private List<SkillScriptableObject> m_SkillData;

    [SerializeField]
    private PlayerController m_PlayerController;

    private List<PlayerSkills.SkillType> m_data;
    private PlayerSkills.SkillType m_CurrentType;

    [SerializeField]
    private GameObject m_PauseGame;
    // Start is called before the first frame update

    //private bool IsInitialize = false;  

    private void Awake()
    {
        m_PlayerController.OnUpgradePopupShowed += PauseGame;
    }
    void OnStart()
    {
        //UpdateInfo(0);
        m_data = null;
    }

    public void UpdateInfo(int _pressedButtonIndex)
    {
        if(m_data == null)
        {
            m_data = m_PlayerController.GetSkillToUpgrade();
        }
        m_CurrentType = m_data[_pressedButtonIndex];
        int firstIndex = (int)m_data[0] - 1; //Since SkillType have None at index 0
        int secondIndex = (int)m_data[1] - 1;
        m_Skill1Image.sprite = m_SkillData[firstIndex].m_Sprite;
        m_Skill2Image.sprite = m_SkillData[secondIndex].m_Sprite;

        int pressedIndex = _pressedButtonIndex == 0 ? firstIndex : secondIndex;
        m_SkillName.text = m_SkillData[pressedIndex].SkillName;
        m_SkillDescription.text = m_SkillData[pressedIndex].SkillDescription;
    }

    public void UpdateData()
    {
        m_data = m_PlayerController.GetSkillToUpgrade();
    }
    public PlayerSkills.SkillType GetCurrentSkillType()
    {
        return m_CurrentType;
    }

    public void PauseGame(object sender, EventArgs e)
    {
        Time.timeScale = 0;
    }
}
