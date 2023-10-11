using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingAI : MonoBehaviour
{
    private GameObject m_Player;
    public float m_Speed;
    private bool m_FacingRight = true;
    private Rigidbody2D m_Rigidbody;
    // Update is called once per frame
    private void Awake()
    {
        m_Player = GameObject.FindWithTag("Player");
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distance = Vector2.Distance(m_Player.transform.position, transform.position);
        Vector2 direction = m_Player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.x < 0 && m_FacingRight || direction.x > 0 && !m_FacingRight)
        {
            Flip();
        }
        //transform.position = Vector2.MoveTowards(transform.position, m_Player.transform.position, m_Speed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(Vector2.forward * angle);
        m_Rigidbody.MovePosition(m_Rigidbody.position + direction * m_Speed * Time.deltaTime);
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
}
