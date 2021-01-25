using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyList : Singleton<EnemyList>
{
    public enum EnemyTypes
    {
        None,
        RedKnight1,
        RedKnight2,
        RedKnight3,
        RedKnight4
        
    }

    //public List<Skills> m_SkillTypes;

   
    private Dictionary <EnemyTypes, GameObject> m_Enemys = new Dictionary <EnemyTypes, GameObject>();
    public void Initialize()
    {
        
        AddEnemyToDictionary(EnemyTypes.RedKnight1,
            "Creatures/Enemy/Forest/RedKnights/Prefabs/Pref_RedKnight_Phase1");
        
        AddEnemyToDictionary(EnemyTypes.RedKnight2,
            "Creatures/Enemy/Forest/RedKnights/Prefabs/Pref_RedKnight_Phase2");
        
        AddEnemyToDictionary(EnemyTypes.RedKnight3,
            "Creatures/Enemy/Forest/RedKnights/Prefabs/Pref_RedKnight_Phase3");

        AddEnemyToDictionary(EnemyTypes.RedKnight4,
            "Creatures/Enemy/Forest/RedKnights/Prefabs/Pref_RedKnight_Phase4");
    }
    public void AddEnemyToDictionary(EnemyTypes aEnemyType, string aPath)
    {
        if (m_Enemys.ContainsKey(aEnemyType) )
        {
            Debug.Log("Prop Type " + aEnemyType + " is already initialized");
            return;
            
        }

        GameObject tempgameobject = Resources.Load<GameObject>(aPath);

        m_Enemys.Add(aEnemyType, tempgameobject);
    }



    public GameObject ReturnEnemyData(EnemyTypes aEnemyType, string sourceName = "Global")
    {
        if (m_Enemys.ContainsKey(aEnemyType))
        {
            return m_Enemys[aEnemyType];
        }

        return null;
    }

}
