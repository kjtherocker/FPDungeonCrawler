using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldEnemy
{
    private List<Vector2Int> m_PathToFollow;
    private Vector2Int m_SpawnPosition;


    public OverWorldEnemy(Vector2Int aSpawnPosition, List<Vector2Int> aPathToFollow)
    {
        m_PathToFollow = aPathToFollow;
        m_SpawnPosition = aSpawnPosition;
    }
    
}
