using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devour : Domain
{
    
    // Start is called before the first frame update
    void Start()
    {
        m_SkillType = SkillType.Domain;
        m_SkillName = "Devour";
        m_SkillDescription = "Consume Domain tiles";
    }

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

        // if the node can't be walked on, return -1 (an invalid tile index)
        if (nodeIndex.m_CombatsNodeType == FloorNode.CombatNodeTypes.Empty)
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
