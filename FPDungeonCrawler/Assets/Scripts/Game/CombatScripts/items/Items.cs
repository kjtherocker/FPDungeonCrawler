using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Start is called before the first frame update


    public Skills.ElementalType m_ItemElementalType;
    public Skills.SkillType m_ItemSkillType;


    public string m_ItemName;
    public string m_ItemDescription;
    
    public virtual void Start()
    {

        m_ItemElementalType = Skills.ElementalType.Null;
        m_ItemSkillType = Skills.SkillType.Defence;
        m_ItemName = "Item";
        m_ItemDescription = "If this is here something bad happened";
    }
    

    public virtual IEnumerator UseItem(Creatures aVictum, Creatures aAttacker )
    {

        return null;

    }

}
