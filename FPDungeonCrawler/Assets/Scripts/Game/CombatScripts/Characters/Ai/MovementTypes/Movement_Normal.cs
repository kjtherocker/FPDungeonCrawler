﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Movement_Normal : MovementType
{
    public override bool CheckIfNodeIsClearAndReturnNodeIndex(FloorNode aNode, Vector2Int m_Position)
    {
        // if the node is out of bounds, return -1 (an invalid tile index)

        if (aNode == null)
        {
            Debug.Log("YOU BROKE " + aNode.m_PositionInGrid.ToString());
        }

        FloorNode nodeIndex = aNode;

        // if the node is already closed, return -1 (an invalid tile index)
        if (nodeIndex.m_HeuristicCalculated == true)
        {
            return false;
        }

        
        if (nodeIndex.m_IsCovered == true)
        {
            return false;
        }

        if (nodeIndex.m_PositionInGrid == m_Position)
        {
            return false;
        }


        if (nodeIndex.m_NodeHeight > 0)
        {
            return false;
        }
        // return a valid tile index
        return true;
    }
}
