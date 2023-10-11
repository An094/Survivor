using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private GameObject m_AimingSprite;

    [SerializeField]
    private GameObject m_ExpPrefab;
    
    [SerializeField]
    private GameObject m_ExplosionPrefab;

    [SerializeField]
    private EnemyScriptableObject m_enemyData;

    private int m_Hp;
    private float m_Exp;
    private float m_BaseSpeed;
    private float m_MaxSpeed;
    private float m_Acceleration;

    private bool isTargeted;
    public bool IsTargeted
    {
        get => isTargeted;
        set
        {
            isTargeted = value;
            m_AimingSprite.SetActive(value);
        }
    }

    public void OnObjectSpawn()
    {
        this.m_Hp = m_enemyData.m_Hp;
    }

    // Start is called before the first frame update
    void Start()
    {
        isTargeted = false;

        this.m_Hp = m_enemyData.m_Hp;
        this.m_Exp = m_enemyData.m_Exp;
        this.m_BaseSpeed = m_enemyData.m_BaseSpeed;
        this.m_MaxSpeed = m_enemyData.m_MaxSpeed;
        m_Acceleration = m_enemyData.m_Acceleration;
    }

    private void Update()
    {
        if (m_Hp <= 0)
        {
            DropExp();
            gameObject.SetActive(false);

        }
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);
            m_Hp -= 10;
        }
        else if (collision.gameObject.tag == "DragonBullet")
        {
            //collision.gameObject.SetActive(false);
            m_Hp = 0;
        }
        
            
    }

    private void DropExp()
    {
        GameObject exp = Instantiate(m_ExpPrefab, transform.position, Quaternion.Euler(0f,0f,90f));
        GameObject explosion = Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
        
    }

}
