using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject m_Panel;

    public void Pause()
    {
        m_Panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        m_Panel.SetActive(false);
        Time.timeScale = 1;
    }
}
