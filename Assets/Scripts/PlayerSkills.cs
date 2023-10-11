using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillType skillType;
    }

    public enum SkillType
    {
        None,
        HatchEgg,
        IncreaseMove_DecreaseFireRate,
        DecreaseFireRate_DecreaseReload,
        IncreaseDragonStat,
        IncreaseWalkSpeed,
        DecreaseReload_IncreaseMaxAmmo,
        Number_Enum
    }

    private List<SkillType> unlockedSkillTypeList;

    public PlayerSkills()
    {
        unlockedSkillTypeList = new List<SkillType>();
    }

    public void UnlockSkill(SkillType skillType)
    {
        //if (!IsSkillUnlocked(skillType))
        {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);
    }

    private bool IsPermanentSkill(SkillType skillType)
    {
        if(skillType == SkillType.HatchEgg)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }


    public bool CanUnlock(SkillType skillType)
    {
        if(IsPermanentSkill(skillType) && IsSkillUnlocked(skillType))
        {
            return false;
        }

        SkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequirement))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public SkillType GetSkillRequirement(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.IncreaseDragonStat: return SkillType.HatchEgg;
        }
        return SkillType.None;
    }

    public bool TryUnlockSkill(SkillType skillType)
    {
        if (CanUnlock(skillType))
        {
            UnlockSkill(skillType);
            return true;
        }
        else
        {
            return false;
        }
    }

}
