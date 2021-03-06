﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : Singleton<PartyManager>
{
    public List<Creatures> m_CurrentParty;
    public List<Creatures> m_ReservePartymembers;
    

    // Use this for initialization
    public void Initialize()
    {
        m_CurrentParty.Add(gameObject.AddComponent<Sigma>());
        m_CurrentParty.Add(gameObject.AddComponent<Fide>());
        m_CurrentParty.Add(gameObject.AddComponent<Vella>());
        m_CurrentParty.Add(gameObject.AddComponent<Cavia>());


        foreach (Creatures ACreatures in m_CurrentParty)
        {
            ACreatures.Initialize();
        }
    }
    

    public void CombatEnd()
    {
        for (int i = 0; i < m_CurrentParty.Count; i++)
        {
            m_CurrentParty[i].m_CurrentHealth = m_CurrentParty[i].m_MaxHealth;
        }
    }

    public void ReserveToParty(int CurrentPartyPosition, int CurrentReservePosition)
    {
        Creatures TransferBuffer = m_CurrentParty[CurrentPartyPosition];
        m_CurrentParty[CurrentPartyPosition] = m_ReservePartymembers[CurrentReservePosition];
        m_ReservePartymembers[CurrentReservePosition] = TransferBuffer;

    }
}
