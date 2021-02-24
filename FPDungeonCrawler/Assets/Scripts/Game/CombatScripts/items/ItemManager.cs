using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class ItemCore
{
    public Items m_Items;
    public int m_Amount;
    public  ItemCore(Items aItem, int aAmount)
    {
        m_Items = aItem;
        m_Amount = aAmount;
        Debug.Log(m_Items.ToString() + " was added");
    }
    public void AddMoreItem(int aAddedAmount )
    {
        m_Amount += aAddedAmount;
        Debug.Log(m_Items.ToString() + " was added and the current amount is " + m_Amount);
    }
}


public class ItemManager : Singleton<ItemManager>
{
    public List<ItemCore> m_AllItems;

    public void Initialize()
    {
        m_AllItems = new List<ItemCore>();
    }


    public void AddItems(Items aNewItem)
    {

        foreach (var aItem in m_AllItems)
        {
            if (aItem.m_Items == aNewItem)
            {
                aItem.AddMoreItem(1);
            
                return;
            }
        }
        
        m_AllItems.Add(new ItemCore(aNewItem,1));
        

    }
}
