using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTabTurnKeeper : UiTabs
{
    public List<TurnWrapper> m_Images;

    public Material m_PlayerIcon;
    public Material m_EnemyIcon;

    private Material m_IconMaterial;

    public int m_Turns;

    public void SetIconType(bool aIsPlayer)
    {
        m_IconMaterial = aIsPlayer ? m_PlayerIcon : m_EnemyIcon;
        
        for (int i = 0; i < m_Images.Count; i++)
        {
            m_Images[i].m_DefaultIcon.material = m_IconMaterial;
        }
    }

    public void SetPressTurns(List<PressTurn> aPressTurns)
    {
        for (int i = 0; i < aPressTurns.Count; i++)
        {
            m_Images[i].SetPressTurn(aPressTurns[i]);
        }
        
    }

    public void UpdateTurnIcons(int aTurns)
    {
        for (int i = 0; i < m_Images.Count; i++)
        {
            m_Images[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < aTurns; i++)
        {
            m_Images[i].UpdateTurnWrapper();
            m_Images[i].gameObject.SetActive(true);
        }

        m_Turns = aTurns;
    }
}
