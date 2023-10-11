using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int m_Level;
    private int m_Experience;

    public LevelSystem()
    {
        m_Level = 1;
        m_Experience = 0;
    }

    public void AddExperience(int amount)
    {
            m_Experience += amount;
            while (m_Experience >= GetExperienceToNextLevel(m_Level))
            {
            // Enough experience to level up
                m_Experience -= GetExperienceToNextLevel(m_Level);
            m_Level++;
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }
            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber()
    {
        return m_Level;
    }

    public float GetExperienceNormalized()
    {
        return (float)m_Experience / GetExperienceToNextLevel(m_Level);
    }

    public int GetExperience()
    {
        return m_Experience;
    }

    public int GetExperienceToNextLevel(int level)
    {
        if (level < 20)
        {
            return (level + 1) * 10 - 5;
        }
        else if (level > 20 && level < 40)
        {
            return (level + 1) * 13 - 6;
        }
        else
        {
            return (level + 1) * 16 - 8;
        }
    }

}
