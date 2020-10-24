﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]

public class CombatNode : Cell
{
    public enum CombatNodeTypes
    {
        Normal,
        Test,
        Wall,
        Empty
    }

    public enum WalkOntopTriggerTypes
    {
        None,
        RelicTower,
        DialoguePrompt,
        Items,
        Memoria
        
    }
    
    public enum DomainCombatNode
    {
        None,
        Domain
    }

    public int m_Heuristic;
    public Vector2Int m_PositionInGrid;

    public int m_MovementCost;
    

    public int m_NodeHeight;

    public float m_NodeHeightOffset;
    
    public bool m_IsGoal;
    public bool m_HeuristicCalculated;
    public bool m_IsWalkable;
    public bool m_IsCovered;
    public DomainCombatNode m_DomainCombatNode;
    public WalkOntopTriggerTypes m_WalkOnTopTriggerTypes;

    
    public NodeReplacement m_NodeReplacement;
    

    public Creatures m_CreatureOnGridPoint;

    List<CombatNode> neighbours = null;
    

    public Grid m_Grid;


    public CombatNodeTypes m_CombatsNodeType;

    
    public PropList.Props m_PropOnNode;

    public EnemyList.EnemyTypes m_EnemyOnNode;
    EnemyList.EnemyTypes m_EnemyOnNodeTemp;

    public PropList.NodeReplacements m_NodeReplacementOnNode;

    public GridFormations NodesGridFormation;
    

    public Memoria m_MemoriaOnTop;
    
    private float m_DomainSwapAmount;
    // Use this for initialization
    public void Initialize()
    {
        m_MovementCost = 1;

        m_Grid = Grid.Instance;
        

        m_DomainCombatNode = CombatNode.DomainCombatNode.None;




    }
    
    
    //Regioned the function since it was long and ugly
    

    public void DestroyNodeReplacement()
    {
       // DestroyImmediate(m_NodeReplacement.gameObject);
       // m_CurrentWalkablePlaneBeingUsed = m_WalkablePlane;
    }

    public void SetCombatNode(CombatNode aCombatnode)
    {
        m_CombatsNodeType = aCombatnode.m_CombatsNodeType;
   
        m_PropOnNode = aCombatnode.m_PropOnNode;
        m_NodeReplacementOnNode = aCombatnode.m_NodeReplacementOnNode;

    }



    public void DestroyEnemy()
    {
        if (m_CreatureOnGridPoint == null)
        {
            return;
        }

        DestroyImmediate(m_CreatureOnGridPoint.gameObject);
        m_CreatureOnGridPoint = null;
        m_IsCovered = false;
        NodesGridFormation.RemoveEnemyFromList();

    }
    
    public void SpawnEnemy()
    {
        
        if (m_EnemyOnNodeTemp == m_EnemyOnNode)
        {
            return;
        }
        else
        {
            DestroyEnemy();
        }
        
        if (m_EnemyOnNode == EnemyList.EnemyTypes.None)
        {
            return;
        }

        if (LevelCreator.instance.m_EnemyList == null)
        {
            LevelCreator.instance.StartEditor();
        }

        m_EnemyOnNodeTemp = m_EnemyOnNode;
        
        Vector3 CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        

        GameObject Enemy = PrefabUtility.
            InstantiatePrefab(LevelCreator.Instance.m_EnemyList.ReturnEnemyData(m_EnemyOnNode)) as GameObject;


        Creatures m_EnemysCreature = Enemy.GetComponent<Creatures>();
       
        m_CreatureOnGridPoint = m_EnemysCreature;
        NodesGridFormation.m_EnemysInGrid.Add(m_EnemysCreature);
        
        Enemy.transform.parent = NodesGridFormation.Enemy.transform;
        Enemy.transform.position = gameObject.transform.position + CreatureOffset;
        Enemy.transform.rotation = Quaternion.Euler(0.0f, 180, 0.0f);

        EnemyAiController m_CreatureAi = (EnemyAiController)m_EnemysCreature.m_CreatureAi;

        m_CreatureAi.Node_ObjectIsOn = this;
        m_CreatureAi.Node_MovingTo = this;
        m_CreatureAi.m_Position = m_PositionInGrid;
        m_CreatureAi.m_Grid = m_Grid;
            
        
        m_IsCovered = true;

    }




   public void SetCreatureOnTopOfNode(Creatures aCreatures)
   {
       m_CreatureOnGridPoint = aCreatures;

       ActivateWalkOnTopTrigger();

   }


   public void ActivateWalkOnTopTrigger()
   {
       switch (m_WalkOnTopTriggerTypes)
       {
           case WalkOntopTriggerTypes.None:

               break;
           case WalkOntopTriggerTypes.RelicTower:
               break ;
           case WalkOntopTriggerTypes.Items:
               
               break ;
           case WalkOntopTriggerTypes.DialoguePrompt:
               
               break ;
           case  WalkOntopTriggerTypes.Memoria:
               
               UiManager.Instance.PushScreen(UiManager.Screen.Memoria);
               
               UiMemoria ScreenTemp =
                   UiManager.Instance.GetScreen(UiManager.Screen.Memoria) as UiMemoria;

               ScreenTemp.SetMemoriaScreen(m_CreatureOnGridPoint,m_MemoriaOnTop);
               
               break;
               
       }
   }
   
    protected static readonly Vector2[] _directions =
    {
        new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1)
    };
    
    public override List<CombatNode> GetNeighbours(List<CombatNode> cells)
    {
        if (neighbours == null)
        {
            neighbours = new List<CombatNode>(4);
            foreach (var direction in _directions)
            {
                var neighbour = cells.Find(c => c.m_PositionInGrid == m_PositionInGrid + direction);
                if (neighbour == null) continue;

                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }

}


