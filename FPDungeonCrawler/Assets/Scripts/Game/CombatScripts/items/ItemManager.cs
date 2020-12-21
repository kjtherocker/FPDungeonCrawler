using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Dictionary<Items, int> m_AllItems;


    public void AddItems(Items aNewItem)
    {
        if(m_AllItems.ContainsKey(aNewItem))
        {
        //    m_AllItems[aNewItem] = m_AllItems.;
            
        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
