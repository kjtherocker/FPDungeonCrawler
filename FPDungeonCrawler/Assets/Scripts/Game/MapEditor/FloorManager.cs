﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.PrefabUtility;

[ExecuteInEditMode]
[System.Serializable]
public class FloorManager : MonoBehaviour
{

    //TODO spawn the arena somewhere and latch onto it
    public CombatArena m_CombatArena;
    public PlayerMovementController m_OverworldPlayerMovementController;
    public Floor m_FloorCore;
    public FloorNode[] m_FloorNodes;
    public FloorNode m_FloorNodePrefab;
    private Dictionary<FloorNode.CardinalNodeDirections, Vector2Int>  m_CardinalPositions;

    public GameObject m_Node;
    public ItemMarker m_Prefab_ItemMarker;
    private List<OverworldEnemyCore> m_EnemysInFloor;
    public OverworldEnemyCore m_OverworldEnemyCorePrefab;
    public GameObject m_Map;
    
    public void Initialize()
    {
        SpawnCamera();
        SpawnGimmicks();
        m_CardinalPositions = new Dictionary<FloorNode.CardinalNodeDirections, Vector2Int>();
        m_CardinalPositions.Add(FloorNode.CardinalNodeDirections.Up, new Vector2Int(-1,0));
        m_CardinalPositions.Add(FloorNode.CardinalNodeDirections.Down, new Vector2Int(1,0));
        m_CardinalPositions.Add(FloorNode.CardinalNodeDirections.Left, new Vector2Int(0,-1));
        m_CardinalPositions.Add(FloorNode.CardinalNodeDirections.Right, new Vector2Int(0,1));
        SwitchToExploration();
        SpawnEnemys();
    }


    
    
    public void SwitchToCombat(OverworldEnemyCore aOverworldEnemyCores)
    {
        AudioManager.instance.PlaySoundOneShot(AudioManager.AudioClips.Encounter,AudioManager.Soundtypes.SoundEffects);
        TacticsManager.instance.StartCombat(m_CombatArena,m_FloorCore,this,aOverworldEnemyCores);
        InputManager.instance.m_MovementControls.Disable();
        gameObject.SetActive(false);
        m_Map.gameObject.SetActive(false);
    }
    
    public void SwitchToExploration()
    {
        InputManager.instance.m_MovementControls.Enable();
        gameObject.SetActive(true);
        m_Map.gameObject.SetActive(true);
        
        
        
        AudioManager.instance.PlaySoundRepeating(AudioManager.AudioClips.Exploration,AudioManager.Soundtypes.Music);
    }

    public void RemoveEnemy(OverworldEnemyCore aOverworldEnemy)
    {
        for (int i = m_EnemysInFloor.Count - 1; i >= 0; i--)
        {
            if (m_EnemysInFloor[i] == aOverworldEnemy)
            {
                Destroy(m_EnemysInFloor[i].gameObject);
                m_EnemysInFloor.RemoveAt(i);
            }
        }

    }

    public void CreateGrid()
    {
        m_FloorCore.Intialize();

        if (m_FloorNodes.Length > 0 && m_FloorNodes != null)
        {
            for (int i = m_FloorNodes.Length -1; i >= 0; i--)
            {
                if (m_FloorNodes[i] == null)
                {
                    continue;
                }

                DestroyImmediate(m_FloorNodes[i].gameObject);
            }
        }


        m_FloorNodes = new FloorNode[m_FloorCore.GridDimensionX * m_FloorCore.GridDimensionY];
        SetLevelNodes(m_FloorCore.FloorBlueprint);
      

    }

    public void SpawnEnemys()
    {
        if (m_OverworldEnemyCorePrefab == null)
        {
            Debug.Log("OverworldEnemyPrefab is not attached");
            return;
        }

        m_EnemysInFloor = new List<OverworldEnemyCore>();
        m_FloorCore.InitializeEnemys();
        foreach (InitializeOverWorldEnemy aEnemy in m_FloorCore.m_Enemy)
        {
            OverworldEnemyCore tempEnemy = Instantiate(m_OverworldEnemyCorePrefab,transform);
            tempEnemy.SetEnemy(aEnemy,this);
            m_EnemysInFloor.Add(tempEnemy);
        }
    }

    public void SpawnGimmicks()
    {
        m_FloorCore.initializeGimmicks();
        foreach (InitializeItemMarkers aItemMarkers in m_FloorCore.m_ItemMarkers)
        {
            ItemMarker tempItemMarker = Instantiate(m_Prefab_ItemMarker,transform);
            tempItemMarker.SetItem(aItemMarkers.m_Item,GetNode(aItemMarkers.m_PositionInGrid));
        }
    }

    
    public void MoveEnemys()
    {
        foreach (var aEnemy in m_EnemysInFloor)
        {
            aEnemy.EnemyMovement();
        }
    }


    public void SetLevelNodes(short[] aLevelBlueprint)
    {
        for (int x = 0; x < m_FloorCore.GridDimensionX; x++)
        {
            for (int y = 0; y < m_FloorCore.GridDimensionY; y++)
            {
                int LevelIndex = m_FloorCore.GetIndex(x, y);
                //If there is no node then continue
                if (aLevelBlueprint[LevelIndex] == (short) Floor.LevelCreationDirections.Empty)
                {
                    continue;
                }

                SpawnNode(x , y,LevelIndex );
                
                m_FloorNodes[LevelIndex].Initialize(aLevelBlueprint[LevelIndex]);
            }
        }
    
    }

    public void SpawnCamera()
    {
        //Default Node
        FloorNode SpawnNode = GetNode(m_FloorCore.m_DefaultSpawnPosition.x, m_FloorCore.m_DefaultSpawnPosition.y);
        
        Vector3 StartNodePosition = SpawnNode.transform.position;
        m_OverworldPlayerMovementController.m_CurrentFloorManager = this;
        m_OverworldPlayerMovementController.currentFloorNode = SpawnNode;
        m_OverworldPlayerMovementController.transform.position =
            new Vector3(StartNodePosition.x, StartNodePosition.y + Constants.Constants.m_HeightOffTheGrid, StartNodePosition.z);

        m_OverworldPlayerMovementController.SetPlayerMapPosition(SpawnNode);
    }


    
    public FloorNode GetNode(Vector2Int CurrentPosition,FloorNode.CardinalNodeDirections TargetDirection)
    {
        Vector2Int FinalPosition = new Vector2Int(CurrentPosition.x + m_CardinalPositions[TargetDirection].x,
            CurrentPosition.y + m_CardinalPositions[TargetDirection].y );
        
        int FinalIndex = m_FloorCore.GetIndex( FinalPosition.x,FinalPosition.y) ;
        
        Debug.Log("Current position " + CurrentPosition + " TargetDirection " + m_CardinalPositions[TargetDirection] + " Final index: " + FinalIndex
         + " Final Position: " + FinalPosition);
        
        return m_FloorNodes[FinalIndex] ;
    }
    
    public FloorNode GetNode(int aRow, int aColumn)
    {
        return m_FloorNodes[m_FloorCore.GetIndex( aRow,aColumn)] ;
    }
    
    public FloorNode GetNode(Vector2Int aPosition)
    {
        return m_FloorNodes[m_FloorCore.GetIndex( aPosition.x,aPosition.y)] ;
    }
    

    public void SpawnNode(int aRow, int aColumn,int aIndex)
    {

        FloorNode tempFloorNode = Instantiate(m_FloorNodePrefab,m_Node.transform);
         
        m_FloorNodes[aIndex] =  tempFloorNode;

        m_FloorNodes[aIndex].m_PositionInGrid = new Vector2Int(aRow,aColumn);
        m_FloorNodes[aIndex].gameObject.name  = aRow + " " + aColumn;
        m_FloorNodes[aIndex].transform.position = new Vector3(4 * aRow, 0.5f, 4 * aColumn);
        m_FloorNodes[aIndex].m_NodeFloorManager = this;

        //  m_GridPathArray[x, y].m_Grid = m_Grid;
    }
}

