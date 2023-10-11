using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mapTile;

    [SerializeField]
    private Transform m_PlayerTransform;


    [SerializeField]
    private float m_Size = 19.0f;
    [SerializeField]
    private float m_Padding = 1.5f;
    private List<GameObject> m_CurrentTiles;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentTiles = new List<GameObject>();
        m_CurrentTiles.Add(m_mapTile);
    }

    // Update is called once per frame
    void Update()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float LeftScreen = GetWorldPosition(0, 0).x;
        float BottomScreen = GetWorldPosition(0, 0).y;
        float RightScreen = GetWorldPosition(screenWidth, screenHeight).x;
        float TopScreen = GetWorldPosition(screenWidth, screenHeight).y;

        bool spawnLeft = false;
        bool spawnRight = false;
        bool spawnBottom = false;
        bool spawnTop = false;

        GameObject currentTile = GetCurrentTile();

        float LeftCurrentTile = currentTile.transform.position.x - m_Size;
        float RightCurrentTile = LeftCurrentTile + m_Size * 2;
        float TopCurrentTile = currentTile.transform.position.y + m_Size;
        float BottomCurrentTile = TopCurrentTile - m_Size * 2;
        if (LeftScreen < LeftCurrentTile + m_Padding)
        {
            //spawn left;
            spawnLeft = true;
            Vector3 newPos = new Vector3(currentTile.transform.position.x - m_Size * 2, currentTile.transform.position.y, currentTile.transform.position.z);
            GameObject newTile = Instantiate(m_mapTile, newPos, transform.rotation);
            m_CurrentTiles.Add(newTile);
        }
        if (TopScreen > TopCurrentTile - m_Padding)
        {
            //spawn Top;
            spawnTop = true;
            Vector3 newPos = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y + m_Size * 2, currentTile.transform.position.z);
            GameObject newTile = Instantiate(m_mapTile, newPos, transform.rotation);
            m_CurrentTiles.Add(newTile);
        }

        if (RightScreen > RightCurrentTile - m_Padding)
        {
            //spawn Right;
            spawnRight = true;
            Vector3 newPos = new Vector3(currentTile.transform.position.x + m_Size * 2, currentTile.transform.position.y, currentTile.transform.position.z);
            GameObject newTile = Instantiate(m_mapTile, newPos, transform.rotation);
            m_CurrentTiles.Add(newTile);
        }
        if (BottomScreen < BottomCurrentTile + m_Padding)
        {
            //spawn Bottom;
            spawnBottom = true;
            Vector3 newPos = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y - m_Size * 2, currentTile.transform.position.z);
            GameObject newTile = Instantiate(m_mapTile, newPos, transform.rotation);
            m_CurrentTiles.Add(newTile);
        }

        if (spawnLeft && spawnTop)
        {
            Vector3 newPos = new Vector3(currentTile.transform.position.x - m_Size * 2, currentTile.transform.position.y + m_Size * 2, currentTile.transform.position.z);
            GameObject newTile = Instantiate(m_mapTile, newPos, transform.rotation);
            m_CurrentTiles.Add(newTile);
        }
        else if (spawnLeft && spawnBottom)
        {
            Vector3 newPos = new Vector3(currentTile.transform.position.x - m_Size * 2, currentTile.transform.position.y - m_Size * 2, currentTile.transform.position.z);
            GameObject newTile = Instantiate(m_mapTile, newPos, transform.rotation);
            m_CurrentTiles.Add(newTile);
        }
        else if (spawnRight && spawnTop)
        {
            Vector3 newPos = new Vector3(currentTile.transform.position.x + m_Size * 2, currentTile.transform.position.y + m_Size * 2, currentTile.transform.position.z);
            GameObject newTile = Instantiate(m_mapTile, newPos, transform.rotation);
            m_CurrentTiles.Add(newTile);

        }
        else if (spawnRight && spawnBottom)
        {
            Vector3 newPos = new Vector3(currentTile.transform.position.x + m_Size * 2, currentTile.transform.position.y - m_Size * 2, currentTile.transform.position.z);
            GameObject newTile = Instantiate(m_mapTile, newPos, transform.rotation);
            m_CurrentTiles.Add(newTile);
        }


    }

    Vector3 GetWorldPosition(float screenX, float screenY)
    {
        Camera mainCamera = Camera.main;
        Vector3 screenPos = new Vector3(screenX, screenY, mainCamera.nearClipPlane);
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        return worldPos;
    }

    private GameObject GetCurrentTile()
    {
        foreach (GameObject tile in m_CurrentTiles)
        {
            float Left = tile.transform.position.x - m_Size;
            float Right = Left + m_Size * 2;
            float Top = tile.transform.position.y + m_Size;
            float Bottom = Top - m_Size * 2;

            Vector3 playerPosition = m_PlayerTransform.position;
            if (playerPosition.x < Left || playerPosition.x > Right || playerPosition.y > Top || playerPosition.y < Bottom)
            {
                continue;
            }
            else
            {
                return tile;
            }
        }
        return null;
    }


}
