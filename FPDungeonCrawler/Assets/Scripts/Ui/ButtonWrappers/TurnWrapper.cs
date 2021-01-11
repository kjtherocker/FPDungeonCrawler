using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnWrapper : MonoBehaviour
{
    public RawImage m_DefaultIcon;
    public RawImage m_EmpoweredIcon;

    private PressTurn m_PressTurn;

    public void SetPressTurn(PressTurn aPressTurn)
    {
        m_PressTurn = aPressTurn;
    }

    public void UpdateTurnWrapper()
    {
        if (m_PressTurn.m_IsEmpowered)
        {
            m_EmpoweredIcon.enabled = true;
        }
        else
        {
            m_EmpoweredIcon.enabled = false;
        }
        
        
    }
}
