using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Renderer m_Renderer;
    private Camera m_Camera;
    private void Awake()
    {

        m_Renderer = GetComponent<Renderer>();
        m_Camera = FindObjectOfType<Camera>();
    }
    public bool IsInScreen()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector3 topLeft = GetWorldPosition(0, 0);
        Vector3 bottomRight = GetWorldPosition(screenWidth, screenHeight);
        //Debug.Log("TopLeft:" + topLeft);
        //Debug.Log("BottomRight: " + bottomRight);
        Vector3 position = transform.position;
        //Debug.Log("Bullet: " + position);
        if (position.x < topLeft.x || position.x > bottomRight.x || position.y > bottomRight.y || position.y < topLeft.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Update()
    {
        if(!IsInScreen())
        {
            gameObject.SetActive(false);
        }
    }

    Vector3 GetWorldPosition(float screenX, float screenY)
    {
        Camera mainCamera = Camera.main;
        Vector3 screenPos = new Vector3(screenX, screenY, mainCamera.nearClipPlane);
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        return worldPos;
    }
}
