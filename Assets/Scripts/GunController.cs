using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private Transform m_GunPoint;

    private GameObject[] enemies;
    //private GameObject nearestObject;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = m_GunPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        transform.position = m_GunPoint.position;
        GameObject nearestObject = GetNearestEnemy();
        if (nearestObject == null)
        {
            return;
        }
        else
        {
            foreach (GameObject en in enemies)
            {
                if (en != nearestObject)
                {
                    en.GetComponent<Enemy>().IsTargeted = false;
                }
            }
            Enemy enemy = nearestObject.GetComponent<Enemy>();
            enemy.IsTargeted = true;

            Vector2 lookDir = nearestObject.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            if (angle > 90 && angle < 180 || angle < -90 && angle > -180)
            {
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y) * -1, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            }
        }

    }

    private GameObject GetNearestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
