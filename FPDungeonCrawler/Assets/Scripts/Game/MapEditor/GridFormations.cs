using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[ExecuteInEditMode]
[System.Serializable]
public class GridFormations : MonoBehaviour
{

    public OverWorldPlayer m_OverworldPlayer;
    public Level m_LevelCore;
    public LevelNode[] _levelNodes;
    public LevelNode m_LevelNodePrefab;

    public void Start()
    {
        SpawnCamera();
    }

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
                if (aLevelBlueprint[LevelIndex] == (short) Level.Directions.Empty)
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
        //Default Node
        LevelNode SpawnNode = GetNode(m_LevelCore.m_DefaultSpawnPosition.x, m_LevelCore.m_DefaultSpawnPosition.y);
        
        Vector3 StartNodePosition = SpawnNode.transform.position;
        m_OverworldPlayer.transform.position =
            new Vector3(StartNodePosition.x, StartNodePosition.y + Constants.Constants.m_HeightOffTheGrid, StartNodePosition.z);
    }

    public void SpawnGimmicks()
    {
        
    }

    public LevelNode GetNode(int aRow, int aColumn)
    {
        return _levelNodes[aColumn * m_LevelCore.GridDimensionX + aRow] ;
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

