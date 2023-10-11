using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHandler : MonoBehaviour
{

    [SerializeField]
    private float m_Radius;
    [SerializeField]
    private Transform m_CenterPoint;
    // Start is called before the first frame update
    [SerializeField]
    private float m_RotationSpeed;
    [SerializeField]
    private GameObject m_DragonPrefab;
    //[SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private float m_HatchTime = 180f;
    [SerializeField]
    private float m_FireRate = 2.0f;
    public event EventHandler OnDragonShooted;
    //[SerializeField]
    //private Transform m_FirePoint;
    private Vector3 m_DragonPosition;
    private GameObject m_Dragon;
    private float m_Angle = 0f;
    private bool isBeginFire = false;
    private bool isHatched = false;
    private float remainingTime;
    public enum DragonState
    {
        Lock,
        Egg,
        Normal,
        Attack
    }
    public DragonState m_DragonState;

    void Start()
    {
        m_DragonState = DragonState.Lock;
        m_Dragon = Instantiate(m_DragonPrefab, Vector3.zero, Quaternion.identity);
        m_Animator = m_Dragon.GetComponent<Animator>();
        m_Dragon.GetComponent<Renderer>().enabled = false;
        remainingTime = m_HatchTime;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        switch(m_DragonState)
        {
            case DragonState.Lock:
            {
                break;
            }
            case DragonState.Egg:
            {
                if(remainingTime <= 0f && !isHatched)
                {
                        HatchEgg();
                        isHatched = true;
                }
                RotateDragon();
                break;
            }
            case DragonState.Normal:
            {
                RotateDragon();
                if(!isBeginFire)
                {
                        isBeginFire = true;
                        StartCoroutine(Attack());
                }
                break;
            }
            default: break;
        }
    }

    private void RotateDragon()
    {
        m_Angle += m_RotationSpeed * Time.deltaTime;

        float x = Mathf.Sin(m_Angle) * m_Radius;
        float y = Mathf.Cos(m_Angle) * m_Radius;

        Vector3 newPosition = m_CenterPoint.position + new Vector3(x, y, 0);
        m_Dragon.transform.position = newPosition;
    }

    private IEnumerator Attack()
    {
        while(isBeginFire)
        {
            yield return new WaitForSeconds(m_FireRate);
            Debug.Log("Fire");
            //GameObject bullet = ObjectPooler.Instance.SpawnFromPool("DragonBullet", m_FirePoint.position, m_FirePoint.rotation);
            //bullet.SetActive(true);
            //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            //rb.AddForce(m_FirePoint.right * 10f, ForceMode2D.Impulse);
            OnDragonShooted(this, EventArgs.Empty);
            m_Animator.SetBool("IsAttack", true);
            yield return new WaitForSeconds(1.0f);
            m_Animator.SetBool("IsAttack", false);
        }
    }

    public void LayEgg()
    {
        m_DragonState = DragonState.Egg;
        m_Dragon.GetComponent<Renderer>().enabled = true;
    }

    private void HatchEgg()
    {
        
        m_DragonState = DragonState.Normal;
        m_Animator.SetTrigger("Hatched");
    }

    public bool CanShoot()
    {
        return m_DragonState == DragonState.Normal ? true : false;
    }
}
