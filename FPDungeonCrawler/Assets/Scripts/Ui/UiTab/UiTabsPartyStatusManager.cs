using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTabsPartyStatusManager : UiTabs
{
    public List<UiStatus> m_Playerstatus;
    public RawImage m_Portrait;
    
    // Start is called before the first frame update
    public override void Initialize()
    {
        for (int i = 0; i < m_Playerstatus.Count; i++)
        {
            m_Playerstatus[i].SetCharacter(PartyManager.instance.m_CurrentParty[i]);
            m_Playerstatus[i].SetStatus(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
