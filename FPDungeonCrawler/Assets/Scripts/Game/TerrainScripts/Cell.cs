using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour, IGraphNode
{
    public int GetDistance(IGraphNode other)
    {
        return GetDistance(other as FloorNode);
    }

    public abstract List<FloorNode> GetNeighbours(List<FloorNode> cells);


}
