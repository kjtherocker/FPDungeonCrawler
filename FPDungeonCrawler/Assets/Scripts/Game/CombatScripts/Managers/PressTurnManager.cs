using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PressTurn
{
    public int m_Turns;
    public bool m_IsEmpowered;
}


public class PressTurnManager : Singleton<PressTurnManager>
{
    private PressTurn[] m_PressTurn;
    public List<PressTurn> m_ActivePressTurn;

    public UiTabTurnKeeper m_TurnKeeper;
    
    public enum PressTurnReactions
    {
        Normal,
        Weak,
        Strong,
        Dodge,
        Null
    }

    public void Initialize()
    {
        m_PressTurn = new PressTurn[8];
        m_ActivePressTurn = new List<PressTurn>();
    }

    public void StartTurn(int aTurnAmount)
    {
       
        
        for (int i = m_ActivePressTurn.Count; i > 0; i--)
        {
            m_ActivePressTurn.RemoveAt(i);
        }

        for (int i = 0; i < aTurnAmount; i++)
        {
            m_ActivePressTurn.Add(m_PressTurn[i]);
        }
    }

    
    //This method will be used at the end when all is activated
    public void ProcessTurn(List<PressTurnReactions> aAllTurnReactions)
    {
        

        //Null Skills Consume all pressturns completely

        foreach (PressTurnReactions reaction in aAllTurnReactions)
        {
            if (reaction == PressTurnReactions.Null)
            {
                NulledPressTurn();
                return;
            }
        }
        
        
        //If Dodged Consume two pressturns if empowered only take the empowered and the next token
        
        foreach (PressTurnReactions reaction in aAllTurnReactions)
        {
            if (reaction == PressTurnReactions.Dodge ||
                reaction == PressTurnReactions.Strong )
            {
                DodgedPressTurn();
                return;
            }
        }
        
        //If weakness is hit correctly then the turn that was used will be empowered
        
        foreach (PressTurnReactions reaction in aAllTurnReactions)
        {
            if (reaction == PressTurnReactions.Weak)
            {
                WeaknessPressTurn();
                return;
            }
        }
        
        
        //Normal action Consume 1 empowered or normal pressturn
        NormalPressTurn();
        //Passing will turn a whole icon into a empowered one but will consume an empowered one if it is

    }

    public void NormalPressTurn()
    {
        ConsumeTurn(1);
    }

    public void DodgedPressTurn()
    {
        ConsumeTurn(2);
    }

    public void NulledPressTurn()
    {
        ConsumeTurn(m_ActivePressTurn.Count);
    }

    public void WeaknessPressTurn()
    {
        EmpoweredTurn();
    }


    public void EndTurn()
    {
        
    }

    public void EmpoweredTurn()
    {
        int ActivePositionTurn = m_ActivePressTurn.Count - 1;
        ChangeActivePressTurn(m_ActivePressTurn[ActivePositionTurn],ActivePositionTurn,true);
        m_TurnKeeper.UpdateTurnIcons(m_ActivePressTurn.Count);
        m_TurnKeeper.SetPressTurns(m_ActivePressTurn);
    }


    public void ChangeActivePressTurn(PressTurn aPressTurn,int ActivePressTurnPosition ,bool aIsPressTurnEmpowered)
    {
        aPressTurn.m_IsEmpowered = aIsPressTurnEmpowered;
        m_ActivePressTurn[ActivePressTurnPosition] = aPressTurn;
    }

    public void ConsumeTurn(int aAmountOfTurnsConsumned)
    {
        int TurnsRemaining =  (m_ActivePressTurn.Count - 1)  - aAmountOfTurnsConsumned;

        if (TurnsRemaining <= 0)
        {
            m_TurnKeeper.SetPressTurns(m_ActivePressTurn);
            m_TurnKeeper.UpdateTurnIcons(-1);
            return;
        }

        for (int i = m_ActivePressTurn.Count - 1; i > TurnsRemaining; i--)
        {
            m_ActivePressTurn.RemoveAt(i);
        }
        m_TurnKeeper.SetPressTurns(m_ActivePressTurn);
        
        m_TurnKeeper.UpdateTurnIcons(m_ActivePressTurn.Count);

    }

    public void SetTurn()
    {
        m_PressTurn[0].m_Turns = 0;
        m_PressTurn[0].m_IsEmpowered = false;
    }
}
