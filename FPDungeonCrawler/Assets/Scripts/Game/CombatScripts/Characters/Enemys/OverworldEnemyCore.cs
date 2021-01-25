using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemyCore : MonoBehaviour
{
   public InitializeOverWorldEnemy m_OverworldEnemy;
   private FloorManager m_FloorManager;
   private FloorNode m_CurrentNode;
   private FloorNode m_PreviousNode;

   private bool m_FollowingPath;
   private int m_PositionInPath;

   private Animator m_Animator;
   
   public void SetEnemy(InitializeOverWorldEnemy aOverworldEnemy, FloorManager aFloorManager)
   {
       m_FloorManager = aFloorManager;
       m_OverworldEnemy = aOverworldEnemy;
       m_PositionInPath = 0;
       m_FollowingPath = true;

       m_Animator = GetComponentInChildren<Animator>();
       SetNodePosition(m_OverworldEnemy.m_SpawnPosition);
   }

   public void SetNodePosition(Vector2Int aPosition)
   {
       m_CurrentNode = m_FloorManager.GetNode(aPosition.x, aPosition.y);
       m_PreviousNode = m_CurrentNode;
       Vector3 nodePosition = m_CurrentNode.gameObject.transform.position;
       
       Vector3 NewNodePosition = new Vector3(nodePosition.x,nodePosition.y,
           nodePosition.z);
       
       transform.position = NewNodePosition;
   }

   
   public void EnemyMovement()
   {

       m_PreviousNode.RemoveOverWorldEnemy();

       FloorNode NextNode = GetNextNode();
       
       if (NextNode == null)
       {
           Debug.Log("Cant Find Node ");
           return;
       }
        
       StartCoroutine(DirectMovement(transform, NextNode, 0.6f));
       
   }

   public FloorNode GetNextNode()
   {
       Vector2Int m_PositiontoGoTo = m_OverworldEnemy.m_PathToFollow[m_PositionInPath];
       FloorNode TargetNode = m_FloorManager.GetNode(m_PositiontoGoTo.x, m_PositiontoGoTo.y);
       return TargetNode;
   }

   public  IEnumerator DirectMovement(Transform aObject, FloorNode  aTargetNode, float aTimeUntilDone)
   {
       Vector3 NewNodePosition = new Vector3(aTargetNode.transform.position.x,aTargetNode.transform.position.y,
           aTargetNode.transform.position.z);

       float timeTaken = 0.0f;
       aTargetNode.SetOverWorldEnemy(this);
       m_Animator.SetBool("b_IsWalking",true);
       while (aTimeUntilDone - timeTaken > 0)
       {
           if (Vector3.Distance(aObject.transform.position, NewNodePosition) < 0.05f)
           {
               timeTaken = aTimeUntilDone;
           }

           timeTaken += Time.deltaTime;
           aObject.position = Vector3.Lerp(aObject.position, NewNodePosition, timeTaken /aTimeUntilDone );
           yield return null;
       }

       m_CurrentNode = aTargetNode;
       m_Animator.SetBool("b_IsWalking",false);
       aObject.position = NewNodePosition;
       MovementEnd();
       yield return 0;
   }

   public void MovementEnd()
   {
       m_PositionInPath += m_FollowingPath ? 1 : -1;

       if (m_PositionInPath >= m_OverworldEnemy.m_PathToFollow.Count - 1)
       {
           m_FollowingPath = false;
       }
       if (m_PositionInPath <= 0)
       {
           m_FollowingPath = true;
       }
       
       m_PreviousNode = m_CurrentNode;
       
     
       
       FloorNode NextNode = GetNextNode();
       Vector3 relativePos = NextNode.gameObject.transform.position - transform.position;
       
       Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
       transform.rotation = rotation;
       
       
   }

}
