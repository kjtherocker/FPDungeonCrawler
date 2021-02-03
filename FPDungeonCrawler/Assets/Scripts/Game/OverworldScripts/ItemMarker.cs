using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMarker : Gimmick
{
    private FloorNode.WalkOntopTriggerTypes m_TriggerType;
    private FloorNode m_FloorNodeConnectedTo;
    private Items m_Item;
    
    public void SetItem(Items aItem, FloorNode aFloorNode)
    {
        m_TriggerType = FloorNode.WalkOntopTriggerTypes.Items;
        m_FloorNodeConnectedTo = aFloorNode;

        gameObject.transform.position = aFloorNode.transform.position;
        
        m_FloorNodeConnectedTo.SetWalkOnTopDelegate(WalkOnTopActivation);
        m_Item = aItem;
    }


    public void WalkOnTopActivation()
    {
     ItemManager.instance.AddItems(m_Item); 
    }
}
