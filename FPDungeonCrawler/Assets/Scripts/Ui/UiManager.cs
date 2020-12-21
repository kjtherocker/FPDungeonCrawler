using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public enum UiScreens
    {
        CommandBoard,
        SkillBoard,
        DomainBoard,
        EnemyScreen,
        DomainClash,
        Memoria,
        MainMenu,
        PartyMenu,
        TurnIndicator,
        EndCombatMenu,
        Notifcation,
        PartyStats,
        Dialogue,


        _NumberOfScreens
    }

    public enum UiTab
    {
        PlayerStatus,
        EnemyStatus,
        DebugUi,
        DomainTab,

        
        _NumberOfScreens
    }
    
    
    public UiScreen[] m_UiScreens;

    
    public List<global::UiTab> m_UiTabs;

    public List<KeyValuePair<UiScreens, UiScreen>> m_ScreenStack = new List<KeyValuePair<UiScreens, UiScreen>>();
    
    public List<UiScreens> m_LastScreen = new List<UiScreens>();

    void OnValidate()
    {
        System.Array.Resize(ref m_UiScreens, (int)UiScreens._NumberOfScreens);
    }

    // Use this for initialization
    public void Initialize()
    {

        m_UiScreens = new UiScreen[15];
        m_UiScreens[(short) UiScreens.CommandBoard] = GetComponentInChildren<UiScreenCommandBoard>(true);
        m_UiScreens[(short) UiScreens.SkillBoard] = GetComponentInChildren<UiSkillBoard>(true);
      //  m_UiScreens[(short) Screen.CommandBoard] = GetComponentInChildren<UiScreenCommandBoard>();
        m_UiScreens[(short) UiScreens.EnemyScreen] = GetComponentInChildren<UiEnemyScreen>(true);
   
        
        for (int i = 0; i < m_UiScreens.Length - 1; i++)
        {
            if (m_UiScreens[i] != null)
            {
                m_UiScreens[i].Initialize();
                m_UiScreens[i].SetGameObjectState(false);
            }

        }
        
        for (int i = 0; i <= m_UiTabs.Count - 1; i++)
        {
            if (m_UiTabs[i] != null)
            {
                m_UiTabs[i].Initialize();
                // m_UiScreens[i].SetGameObjectState(false);
            }

        }
    }

    public global::UiTab GetUiTab(UiTab aUiTab)
    {
        return m_UiTabs[(int)aUiTab];
    }

    public void PushTab(UiTab aUiTab)
    {
         m_UiTabs[(int)aUiTab].gameObject.SetActive(true);
    }


    public UiScreens GetTopScreenType()
    {
        return m_ScreenStack[m_ScreenStack.Count - 1].Key;
    }

    public UiScreen GetTopScreen()
    {
        return m_ScreenStack[m_ScreenStack.Count - 1].Value;
    }

    public UiScreen GetScreen(UiScreens aUiScreens)
    {

        if (m_UiScreens[(short)aUiScreens] != null)
        {
            return m_UiScreens[(short)aUiScreens];
        }
        

        return null;
    }

    public void PushScreen(UiScreens aUiScreens)
    {
        if (m_ScreenStack.Count != 0)
        {
            m_ScreenStack[m_ScreenStack.Count - 1].Value.m_InputActive = true;
        }

        UiScreen screenToAdd = m_UiScreens[(int)aUiScreens];
        screenToAdd.OnPush();

        Debug.Log(screenToAdd.ToString());
        m_ScreenStack.Add(new KeyValuePair<UiScreens, UiScreen>(aUiScreens, screenToAdd));
        m_ScreenStack[m_ScreenStack.Count - 1].Value.m_InputActive = true;
    }

    public void PopScreen()
    {
        if (m_LastScreen.Count > 5)
        {
            m_LastScreen.RemoveAt(0);
        }
        m_LastScreen.Add(m_ScreenStack[m_ScreenStack.Count - 1].Key);
        
        Debug.Log("Popped Screen " + m_ScreenStack[m_ScreenStack.Count - 1].Key);
        
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPop();
        m_ScreenStack.RemoveAt(m_ScreenStack.Count - 1);
       
    }

    public void PopScreenNoLastScreen()
    {
        if (m_ScreenStack.Count <= 0)
        {
            return;
        }

        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPop();
        m_ScreenStack.RemoveAt(m_ScreenStack.Count - 1);
    }

    public void ReturnToLastScreen()
    {
        PopScreenNoLastScreen();
        if (m_LastScreen.Count == 0)
        {
            return;
        }

        PushScreen(m_LastScreen[m_LastScreen.Count - 1]);
        m_LastScreen.RemoveAt(m_LastScreen.Count - 1);
    }

    public void PopInvisivble()
    {
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPop();
    }

    public void PopAllInvisivble()
    {
        foreach (var screenPair in m_ScreenStack)
        {
            screenPair.Value.OnPop();
        }
    }

    public void PushToTurnOn()
    {
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPush();
    }

    public void PopAllScreens()
    {
        foreach (var screenPair in m_ScreenStack)
        {
            UiScreen Screen = screenPair.Value;

            Screen.OnPop();
        }

        m_ScreenStack.Clear();
    }
    
    
    public void PopAllTabs()
    {
        foreach (var screenPair in m_UiTabs)
        {
            screenPair.gameObject.SetActive(false);
        }
        
    }
}
