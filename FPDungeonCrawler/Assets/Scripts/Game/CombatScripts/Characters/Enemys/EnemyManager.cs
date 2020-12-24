using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Creatures> m_EnemyList;

    public void AddEnemysToManager(List<EnemyList.EnemyTypes> aEnemys, CombatArena aArena)
    {
        for (int i = 0; i < aEnemys.Count; i++)
        {
            
            
            GameObject m_Enemy = Instantiate(EnemyList.instance.ReturnEnemyData(aEnemys[i]));
            m_Enemy.transform.position = aArena.SpawnPositions[i].transform.position;

            Creatures creatures = m_Enemy.GetComponent<Creatures>();
            
            m_EnemyList.Add(creatures);
            creatures.m_SpawnObject = m_Enemy;
        }
    }

    public void ResetEnemyManager()
    {
        for (int i = m_EnemyList.Count - 1; i >= 0; i--)
        {
            if (m_EnemyList[i])
            {    
                m_EnemyList.RemoveAt(i);
            }
        }
    }


}
