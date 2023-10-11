using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;
    public Transform m_FirePoint;
    public float m_BulletForce;
    public float m_FireRate = 1.0f;

    private int m_MaxAmmo = 6;
    private int m_ShootedNum = 0;
    private float m_ReloadRate = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }


    private IEnumerator Shoot()
    {
        WaitForSeconds wait = new WaitForSeconds(m_FireRate);
        while (true)
        {
            yield return wait;
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool("Bullet", m_FirePoint.position, m_FirePoint.rotation);
            bullet.SetActive(true);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(m_FirePoint.right * m_BulletForce, ForceMode2D.Impulse);
            m_ShootedNum++;
            if (m_ShootedNum == m_MaxAmmo)
            {
                m_ShootedNum = 0;
                m_Animator.SetTrigger("Reload");
                yield return new WaitForSeconds(m_ReloadRate);
                m_Animator.SetTrigger("Shoot");
            }

        }

    }

    public void ChangeFireRate(float _amount)
    {
        m_FireRate *= _amount;
    }

    public void ChangeMaxAmmo(int _maxAmmo)
    {
        m_MaxAmmo += _maxAmmo;
    }
       
    public void ChangeReloadRate(float _amount)
    {
        m_ReloadRate *= _amount;
    }

}
