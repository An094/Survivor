using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float m_MoveSpeed = 3f; 
	[SerializeField]
	private GameObject m_ExpBar;
	[SerializeField]
	private Text m_LevelText;
	[SerializeField]
	private GameObject m_UpgradePopup;
	[SerializeField]
	private GameObject m_Gun;
	[SerializeField]
	private List<SkillScriptableObject> m_SkillData;


	private int m_Heart;
	private bool m_FacingRight = true;
	private LevelSystem m_LevelSystem;
	private PlayerSkills m_PlayerSkills;
	private Image barImage;

	private float m_WalkSpeed = 1f; // or 1/3 move Speed

	public event EventHandler OnUpgradePopupShowed;

	private void Awake()
    {
		m_LevelSystem = new LevelSystem();
		m_PlayerSkills = new PlayerSkills();
		m_LevelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
		m_LevelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
		m_PlayerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
	}

    private void Start()
    {
		barImage = m_ExpBar.GetComponent<Image>();
		barImage.fillAmount = 0f;
		m_LevelText.text = "Level: 1";
	}

	public List<PlayerSkills.SkillType> GetSkillToUpgrade()
	{
		List<PlayerSkills.SkillType> result = new List<PlayerSkills.SkillType>();
		int numberSkill = 0;
		PlayerSkills.SkillType latestChoice = PlayerSkills.SkillType.None;
		while (numberSkill != 2)
		{
			//Add for debugging
			int level = m_LevelSystem.GetLevelNumber();
			PlayerSkills.SkillType type;
			if (level == 2 && latestChoice != PlayerSkills.SkillType.HatchEgg)
            {
				type = PlayerSkills.SkillType.HatchEgg;

			}
			else
            {

				type = (PlayerSkills.SkillType)UnityEngine.Random.Range(1, (int)PlayerSkills.SkillType.Number_Enum);
            }
			if (m_PlayerSkills.CanUnlock(type) && type != latestChoice)
			{
				latestChoice = type;
				numberSkill++;
				result.Add(type);
			}
		}
		return result;
	}

	private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
	{
		switch (e.skillType)
		{
			case PlayerSkills.SkillType.HatchEgg:
            {
				GetComponent<DragonHandler>().LayEgg();
				break;
            }
			case PlayerSkills.SkillType.IncreaseMove_DecreaseFireRate:
			{
				ChangeMoveSpeed(1.2f);
				ChangeFireRate(0.95f);
				break;
            }
			case PlayerSkills.SkillType.DecreaseFireRate_DecreaseReload:
			{
				ChangeFireRate(0.95f);
				ChangeReloadRate(0.8f);
				break;
			}
			case PlayerSkills.SkillType.IncreaseDragonStat:
			{
				break;
			}
			case PlayerSkills.SkillType.IncreaseWalkSpeed:
			{
				break;
			}
			case PlayerSkills.SkillType.DecreaseReload_IncreaseMaxAmmo:
			{
				ChangeReloadRate(0.9f);
				ChangeMaxAmmo(2);
				break;
			}
			break;
		}
	}

	private void LevelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
		barImage.fillAmount = m_LevelSystem.GetExperienceNormalized();

	}

	private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
	{
		//PlayAnimation
		m_LevelText.text = "Level " + m_LevelSystem.GetLevelNumber();
		m_UpgradePopup.GetComponent<UpgradPopupManager>().UpdateData();
		m_UpgradePopup.GetComponent<UpgradPopupManager>().UpdateInfo(0);
		m_UpgradePopup.SetActive(true);
		OnUpgradePopupShowed(this, EventArgs.Empty);
		//List<PlayerSkills.SkillType> skillTypes = GetSkillToUpgrade();

	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public void Move(Vector2 _move)
    {
		if(_move.x < 0 && m_FacingRight || _move.x > 0 && !m_FacingRight)
        {
			Flip();
        }
		Vector3 scaledMovement = m_MoveSpeed * Time.deltaTime * new Vector3(
			_move.x,
			_move.y,
			0
		);

		transform.Translate(scaledMovement);
	}

	private void SetMovementSpeed(float speed)
    {
		m_MoveSpeed = speed;
    }
	private void IncreaseHeart(int amount)
	{
		m_Heart += amount;
	}

	public void IncreaseExp(int amount)
    {
		m_LevelSystem.AddExperience(amount);
    }

	private void ChangeMoveSpeed(float _amount)
    {
		m_MoveSpeed *= _amount;
	}

	private void ChangeWalkSpeed(float _amount)
    {
		m_WalkSpeed *= _amount;
    }

	private void ChangeFireRate(float _amount)
    {
		m_Gun.GetComponent<Shooting>().ChangeFireRate(_amount);
    }

	private void ChangeReloadRate(float _amount)
    {
		m_Gun.GetComponent<Shooting>().ChangeReloadRate(_amount);

	}

	private void ChangeMaxAmmo(int _amount)
    {
		m_Gun.GetComponent<Shooting>().ChangeMaxAmmo(_amount);

	}

	public void UpgradeSkill()
    {
		PlayerSkills.SkillType currentType = m_UpgradePopup.GetComponent<UpgradPopupManager>().GetCurrentSkillType();
		m_UpgradePopup.SetActive(false);
		Time.timeScale = 1;//Resume
		m_PlayerSkills.UnlockSkill(currentType);
    }
}
