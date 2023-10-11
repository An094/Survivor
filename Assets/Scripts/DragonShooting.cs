using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonShooting : MonoBehaviour
{
    public Transform m_FirePoint;
    public DragonHandler m_Handler;
    public float m_BulletForce;
    public float m_FireRate = 1.0f;

    private int m_MaxAmmo = 6;
    private int m_ShootedNum = 0;
    private float m_ReloadRate = 1.0f;
    // Start is called before the first frame update

    private void Awake()
    {
        m_Handler = GameObject.FindWithTag("Player").GetComponent<DragonHandler>();
        m_Handler.OnDragonShooted += Shoot;
    }
    void Start()
    {
        //StartCoroutine(Shoot());
    }


    //private IEnumerator Shoot()
    //{
    //    WaitForSeconds wait = new WaitForSeconds(m_FireRate);
    //    while (m_Handler.CanShoot())
    //    {
    //        yield return wait;
    //        GameObject bullet = ObjectPooler.Instance.SpawnFromPool("DragonBullet", m_FirePoint.position, m_FirePoint.rotation);
    //        bullet.SetActive(true);
    //        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    //        rb.AddForce(m_FirePoint.right * m_BulletForce, ForceMode2D.Impulse);

    //    }

    //}
    public void Shoot(object sender, EventArgs e)
    {
        GameObject bullet = ObjectPooler.Instance.SpawnFromPool("DragonBullet", m_FirePoint.position, m_FirePoint.rotation);
        bullet.SetActive(true);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector3 dir = (GetNearestEnemy().transform.position - m_FirePoint.position).normalized;
        rb.AddForce(dir * m_BulletForce, ForceMode2D.Impulse);

    }

    private GameObject GetNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestObject = null;
        float minDistance = 14.0f;
        foreach (GameObject obj in enemies)
        {
            float distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObject = obj;
            }
        }
        return nearestObject;

    }
}
