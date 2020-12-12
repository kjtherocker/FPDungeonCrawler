using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTabPartyStatus : UiTabScreen
{
    public List<UiStatus> m_Playerstatus;
    
    
    // Start is called before the first frame update
    public override void Initialize()
    {
        for (int i = 0; i < m_Playerstatus.Count; i++)
        {
            m_Playerstatus[i].SetCharacter(PartyManager.instance.m_CurrentParty[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
