﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InitializeOverWorldEnemy
{
    public List<Vector2Int> m_PathToFollow;
    public Vector2Int m_SpawnPosition;
    public delegate List<EnemyList.EnemyTypes> EnemySet();

    public EnemySet m_Enemyset;
    public InitializeOverWorldEnemy(Vector2Int aSpawnPosition, List<Vector2Int> aPathToFollow,EnemySet aEnemySet )
    {
        m_PathToFollow = aPathToFollow;
        m_SpawnPosition = aSpawnPosition;
        m_Enemyset = aEnemySet;
    }
}


public struct InitializeItemMarkers
{
    public Vector2Int m_PositionInGrid;
    public Items m_Item;
    
    public InitializeItemMarkers(Items aItem, Vector2Int aSpawnPosition)
    {
        m_PositionInGrid = aSpawnPosition;
        m_Item = aItem;

    }
}
