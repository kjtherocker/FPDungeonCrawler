using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiMap : UiScreen
{
    private Level m_LevelCore;

    public GameObject m_MapTextureNode;
    
    public GameObject[]  _MapNodes;

    public Texture[] MapNodeTexture;
    
    public GameObject m_StartingPoint;

    public GameObject SpawnPoint;
    public void SetMap(Level aLevelCore)
    {
        m_LevelCore = aLevelCore;
        GenerateMap();
        
        
    }

    public void GenerateMap()
    {
        _MapNodes = new GameObject[m_LevelCore.GridDimensionX * m_LevelCore.GridDimensionY];
        
        for (int x = 0; x < m_LevelCore.GridDimensionX; x++)
        {
            for (int y = 0; y < m_LevelCore.GridDimensionY; y++)
            {
                
                int nodeindex = m_LevelCore.GetIndex(x, y);
                
                if (m_LevelCore.LevelBlueprint[nodeindex] == (short) Level.LevelCreationDirections.Empty)
                {
                    continue;
                }

                Vector3 Spawnposition = new Vector3(m_StartingPoint.transform.position.x + 22 * x, m_StartingPoint.transform.position.y +22 * y,0);
                
                
                _MapNodes[nodeindex] = Instantiate(m_MapTextureNode,SpawnPoint.transform);
                _MapNodes[nodeindex].GetComponent<RawImage>().texture =
                    MapNodeTexture[m_LevelCore.LevelBlueprint[nodeindex]];
                _MapNodes[nodeindex].transform.position = Spawnposition;

            }
        }

    }
    
}
