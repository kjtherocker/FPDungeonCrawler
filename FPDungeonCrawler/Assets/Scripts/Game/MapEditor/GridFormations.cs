using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[ExecuteInEditMode]
[System.Serializable]
public class GridFormations : MonoBehaviour
{
    public Level m_LevelCore;
    public LevelNode[] _levelNodes;
    public LevelNode m_LevelNodePrefab;
    
    public void CreateGrid()
    {
        m_LevelCore.Intialize();
        
        _levelNodes = new LevelNode[m_LevelCore.GridDimensionX * m_LevelCore.GridDimensionY];
        SetLevelNodes(m_LevelCore.LevelBlueprint);
    }

    public void SetLevelNodes(short[] aLevelBlueprint)
    {
        for (int x = 0; x < m_LevelCore.GridDimensionX; x++)
        {
            for (int y = 0; y < m_LevelCore.GridDimensionY; y++)
            {
                int LevelIndex = m_LevelCore.GetIndex(x, y);
                //If there is no node then continue
                if (aLevelBlueprint[LevelIndex] == (short) Level.LevelnodeType.Empty)
                {
                    continue;
                }

                SpawnNode(y , x,LevelIndex );
                
                _levelNodes[LevelIndex].Initialize(aLevelBlueprint[LevelIndex]);
            }
        }
    
    }

    public void SpawnCamera()
    {
        
    }

    public void SpawnGimmicks()
    {
        
    }

    public void SpawnNode(int aRow, int aColumn,int aIndex)
    {
        
         
        _levelNodes[aIndex] =  PrefabUtility.InstantiatePrefab(m_LevelNodePrefab) as LevelNode;

        _levelNodes[aIndex].gameObject.transform.parent = transform;
        _levelNodes[aIndex].gameObject.name  = aRow + " " + aColumn;
        _levelNodes[aIndex].transform.position = new Vector3(4 * aRow, 0.5f, 4 * aColumn);
         

        //  m_GridPathArray[x, y].m_Grid = m_Grid;
    }
    


}

