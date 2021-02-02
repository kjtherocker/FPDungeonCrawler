using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiMap : UiScreen
{
    private Floor m_FloorCore;

    private Vector2Int m_MapDimensions;
    
    public GameObject m_MapTextureNode;
    
    public GameObject[]  _MapNodes;

    public Texture[] MapNodeTexture;
    
    public GameObject m_SpawnPosition;

    public GameObject m_Grid;

    public GameObject PlayerNode;

    public GameObject m_NodePlayerIsOn;

    private float m_WidthHeight;
    
    private float MapNodeOffset;
    
    
    
    public void SetMap(Floor aFloorCore)
    {
        m_FloorCore = aFloorCore;
        m_WidthHeight = 17;
        GenerateMap();
        MapNodeOffset = 25;
       
    }

    public void GenerateMap()
    {
        _MapNodes = new GameObject[m_FloorCore.GridDimensionX * m_FloorCore.GridDimensionY];
        
        for (int x = 0; x < m_FloorCore.GridDimensionX; x++)
        {
            for (int y = 0; y < m_FloorCore.GridDimensionY; y++)
            {
                
                int nodeindex = m_FloorCore.GetIndex(x, y);
                
                if (m_FloorCore.FloorBlueprint[nodeindex] == (short) Floor.LevelCreationDirections.Empty)
                {
                    continue;
                }


                float temp = 17;
                //Every time I tried to use a variable it just wouldnt work correctly
                Vector3 Spawnposition = new Vector3(m_SpawnPosition.transform.position.x + m_WidthHeight * x, m_SpawnPosition.transform.position.y + m_WidthHeight * y,0);
                
                
                _MapNodes[nodeindex] = Instantiate(m_MapTextureNode,m_Grid.transform);
                _MapNodes[nodeindex].name = x + " " + y;
                
                _MapNodes[nodeindex].GetComponent<RawImage>().texture =
                    MapNodeTexture[m_FloorCore.FloorBlueprint[nodeindex]];
                _MapNodes[nodeindex].transform.position = Spawnposition;

                IsNodeRevealed(nodeindex);


            }
        }

    }

    public void Update()
    {

    }

    public void CenterMapToPlayer()
    {
        Vector2 PlayerPosition = new Vector2(m_NodePlayerIsOn.transform.localPosition.x, m_NodePlayerIsOn.transform.localPosition.y);
        m_Grid.transform.localPosition =  new Vector3(-PlayerPosition.y,PlayerPosition.x,0);
    }

    public void IsNodeRevealed(int aIndex)
    {
        if (!m_FloorCore.FloorRevealed[aIndex])
        {
            _MapNodes[aIndex].SetActive(false);
        }
        else
        {
            _MapNodes[aIndex].SetActive(true); 
        }
    }

    public void SetMapRevealed(int aIndex, bool aSetRevealStatus)
    {
        if (m_FloorCore.FloorRevealed[aIndex] == aSetRevealStatus)
        {
            return;
        }

        m_FloorCore.FloorRevealed[aIndex] = aSetRevealStatus;
        
        IsNodeRevealed(aIndex);
    }

    public void SetPlayerNode(int index)
    {
        m_NodePlayerIsOn = _MapNodes[index];
        CenterMapToPlayer();
        PlayerNode.transform.position = m_NodePlayerIsOn.transform.position; 
        
        SetMapRevealed(index, true);
    }

}
