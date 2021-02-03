using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemList : Singleton<ItemList>
{
    public enum ItemEnum
    {
        None,
        Potion,
        HighPotion,
        Ether,
        HighEther
    }

    //public List<Skills> m_SkillTypes;

    public Dictionary<int, Items> m_SkillTypes = new Dictionary<int, Items>();
    
    // Use this for initialization
    public void Initialize()
    {
        SetAllSkills();
    }


    public void SetAllSkills()
    {
        m_SkillTypes = new Dictionary<int, Items>();
        
        //Regen Effects
        m_SkillTypes.Add((int)ItemEnum.Potion, new Potion());
        m_SkillTypes.Add((int)ItemEnum.HighPotion, new HighPotion());

        m_SkillTypes.Add((int) ItemEnum.Ether, new Ether());
        m_SkillTypes.Add((int)ItemEnum.HighEther, new HighEther());

        foreach (var aItem in m_SkillTypes)
        {
            aItem.Value.Start();
        }
    }
    
    public Items GetItem(ItemEnum aSkills, string sourceName = "Global")
    {
       return m_SkillTypes[(int)aSkills];
    }
}
