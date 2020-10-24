using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour, IGraphNode
{
    public int GetDistance(IGraphNode other)
    {
        return GetDistance(other as LevelNode);
    }

    public abstract List<LevelNode> GetNeighbours(List<LevelNode> cells);


}
