﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;
using UnityEngine.AddressableAssets;


public class TacticsManager : Singleton<TacticsManager>
{

    public PartyManager m_PartyManager;
    public EnemyManager m_EnemyManager;
    
    bool WhichSidesTurnIsIt;
    bool CombatHasStarted;

    private int m_EnemyAiCurrentlyInList;

    public HealthBar m_Healthbar;
    

    public TextMeshProUGUI m_TurnSwitchText;
    
    public List<Creatures> DeadAllys;
    public List<Creatures> TurnOrderAlly;
    public List<Creatures> TurnOrderEnemy;

    private int m_Turns;
    
    private GameObject m_MemoriaPrefab;
    public Dictionary<Creatures, Creatures> m_CreaturesWhosDomainHaveClashed;
    

    public enum CombatStates
    {
        NoTurn,
        Spawn,
        EnemyTurn,
        AllyTurn,
        DomainClash,
    
        EndOfCombat


    }

    public CombatStates m_BattleStates;

    void Start()
    {
     //   CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        m_EnemyManager = EnemyManager.instance;
        m_PartyManager = PartyManager.Instance;
        //CombatStart();
    }

    public void StartCombat(CombatArena aArena,Floor aCurrentFloor)
    {
        EnemyManager.instance.AddEnemysToManager(aCurrentFloor.EnemySet1());
        
         for (int i = 0; i < m_EnemyManager.m_EnemyList.Count; i++)
         {
             m_EnemyManager.m_EnemyList[i].Initialize();
             AddCreatureToCombat(m_EnemyManager.m_EnemyList[i], TurnOrderEnemy);
         }

         AddCreatureToCombat(m_PartyManager.m_CurrentParty[0], TurnOrderAlly);
         AddCreatureToCombat(m_PartyManager.m_CurrentParty[1], TurnOrderAlly);
         AddCreatureToCombat(m_PartyManager.m_CurrentParty[2], TurnOrderAlly);
         AddCreatureToCombat(m_PartyManager.m_CurrentParty[3], TurnOrderAlly);

         m_Turns = m_PartyManager.m_CurrentParty.Count;
         
         UiManager.instance.PushScreen(UiManager.UiScreens.CommandBoard);
         
         UiScreen temp = UiManager.instance.GetScreen(UiManager.UiScreens.CommandBoard);
         ((UiScreenCommandBoard) temp).m_CommandboardCreature = m_PartyManager.m_CurrentParty[0];

         aArena.gameObject.SetActive(true);

    }

    public void ProcessTurn(IEnumerator aCoroutine)
    {
        StartCoroutine(aCoroutine);
        m_Turns--;

        if (m_Turns > 0)
        {
            NextTurn();
        }

        if (m_Turns == 0)
        {
            //swap sides
        }
    }


    public void NextTurn()
    {
        UiManager.instance.PopAllScreens();
        UiManager.instance.PushScreen(UiManager.UiScreens.CommandBoard);
         
        UiScreen temp = UiManager.instance.GetScreen(UiManager.UiScreens.CommandBoard);
        ((UiScreenCommandBoard) temp).m_CommandboardCreature = m_PartyManager.m_CurrentParty[m_Turns];
        
    }

    public void InvokeSkill(IEnumerator aSkill)
    {
        StartCoroutine(aSkill);
    }
    
    public void EndTurn()
    {
        StartCoroutine(EnemyTurn());

    }



    public void AddCreatureToCombat(Creatures aCreature,List<Creatures> aList)
    {
        if (aCreature == null)
        {
            return;
        }

        aList.Add(aCreature);

        //int TopElement = aList.Count - 1;

        //Model
//        aList[TopElement].ModelInGame = Instantiate<GameObject>(aList[TopElement].Model);
        //aList[TopElement].ModelInGame.transform.position = // m_Grid.GetNode(aPosition.x, aPosition.y).gameObject.transform.position + CreatureOffset;
     //   aList[TopElement].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 180, 0.0f);
        
        
        //Healthbar

     //   aList[TopElement].m_CreatureAi.m_Healthbar = Instantiate<HealthBar>(m_Healthbar, aList[TopElement].m_CreatureAi.transform);
     //   AddHealthbar(aList[TopElement]);
    }


    public void AddHealthbar(Creatures aCreature)
    {

        if (aCreature == null)
        {
            Debug.Log("Creature Was null");
            return;
        }

        aCreature.m_CreatureAi.m_Healthbar = Instantiate<HealthBar>(m_Healthbar, aCreature.m_CreatureAi.transform);
        aCreature.m_CreatureAi.m_Healthbar.Partymember = aCreature;
    }


    void Update()
    {


       switch (m_BattleStates)
       {
           case CombatStates.Spawn:
             
                   m_BattleStates = CombatStates.AllyTurn;


             
               break;

           case CombatStates.AllyTurn:
               

                break;

            case CombatStates.EnemyTurn:
                

                break;



            case CombatStates.EndOfCombat:

           //    if (Input.anyKey)
            //   {
              //    CombatEnd();
             //  }
               break;
       }

    }
    
    
    public void RemoveDeadFromList(Creatures.Charactertype aCharactertype)
    {
        if (aCharactertype == Creatures.Charactertype.Ally)
        { 
            for (int i = TurnOrderAlly.Count - 1; i >= 0; i--)
            {
                if (TurnOrderAlly[i] == null)
                {    
                    TurnOrderAlly.RemoveAt(i);
                }
            }
        }



    }

    public IEnumerator EnemyTurn()
    {
        m_BattleStates = CombatStates.EnemyTurn;

        m_TurnSwitchText.gameObject.SetActive(true);
        m_TurnSwitchText.text = "ENEMY TURN";
        m_TurnSwitchText.color = Color.red;

        yield return new WaitForSeconds(2f);
        m_TurnSwitchText.gameObject.SetActive(false);
        
        m_EnemyAiCurrentlyInList = 0;
        EnemyMovement();

    }

    public void EnemyMovement()
    {

        if (m_EnemyAiCurrentlyInList > TurnOrderEnemy.Count - 1)
        {
            StartCoroutine(AllyTurn());
            return ;
        }
        else
        {
            EnemyAiController EnemyTemp = TurnOrderEnemy[m_EnemyAiCurrentlyInList].m_CreatureAi as EnemyAiController;
            m_EnemyAiCurrentlyInList++;
            if (EnemyTemp.DoNothing == false)
            {
                EnemyTemp.EnemyMovement();
            }
        }
    }

    public IEnumerator AllyTurn()
    {
        m_BattleStates = CombatStates.AllyTurn;

        m_TurnSwitchText.gameObject.SetActive(true);
        m_TurnSwitchText.text = "PLAYER TURN";
        m_TurnSwitchText.color = Color.blue;


        foreach (Creatures creature in TurnOrderAlly)
        {
            creature.m_CreatureAi.m_HasMovedForThisTurn = false;
            creature.m_CreatureAi.m_HasAttackedForThisTurn = false;
            creature.EndTurn();
        }

        yield return new WaitForSeconds(2f);
        m_TurnSwitchText.gameObject.SetActive(false);
    }
    

}
