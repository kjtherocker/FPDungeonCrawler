using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiMap : UiScreen
{
    private Floor m_FloorCore;

    public GameObject m_MapTextureNode;
    
    public GameObject[]  _MapNodes;

    public Texture[] MapNodeTexture;
    
    public GameObject m_StartingPoint;

    public GameObject SpawnPoint;

    public GameObject PlayerNode;
    public void SetMap(Floor aFloorCore)
    {
        m_FloorCore = aFloorCore;
        GenerateMap();
        
        
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

                Vector3 Spawnposition = new Vector3(m_StartingPoint.transform.position.x + 22 * x, m_StartingPoint.transform.position.y +22 * y,0);
                
                
                _MapNodes[nodeindex] = Instantiate(m_MapTextureNode,SpawnPoint.transform);
                _MapNodes[nodeindex].GetComponent<RawImage>().texture =
                    MapNodeTexture[m_FloorCore.FloorBlueprint[nodeindex]];
                _MapNodes[nodeindex].transform.position = Spawnposition;

                IsNodeRevealed(nodeindex);


            }
        }

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
        PlayerNode.transform.position = _MapNodes[index].transform.position; 
        SetMapRevealed(index, true);
    }

}
