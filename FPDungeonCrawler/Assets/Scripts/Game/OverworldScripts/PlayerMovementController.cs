using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class PlayerMovementController : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    
    public FloorNode currentFloorNode;
    
    public FloorNode.CardinalNodeDirections[] CardinalDirections;
    public FloorNode.CardinalNodeDirections CurrentDirection;
    public int CurrentDirectionValue;

    public Vector2Int CurrentPosition;

    private bool m_IsRotating;
    private bool m_IsMoving;
    
    public FloorManager m_CurrentFloorManager;
    private Dictionary<FloorNode.CardinalNodeDirections, Vector3> m_DirectionRotations;

    public UiMap m_Map;

    public float StepCounter;
    
    public void Initialize ()
    {
        CardinalDirections = new []
        {
            FloorNode.CardinalNodeDirections.Up, FloorNode.CardinalNodeDirections.Left, 
            FloorNode.CardinalNodeDirections.Down,FloorNode.CardinalNodeDirections.Right
        };
        
        m_DirectionRotations = new Dictionary<FloorNode.CardinalNodeDirections, Vector3>();
        
        m_DirectionRotations.Add( FloorNode.CardinalNodeDirections.Down, new Vector3(0, 90, 0));
        m_DirectionRotations.Add( FloorNode.CardinalNodeDirections.Left, new Vector3(0, 180, 0));
        m_DirectionRotations.Add( FloorNode.CardinalNodeDirections.Up, new Vector3(0, 270, 0));
        m_DirectionRotations.Add( FloorNode.CardinalNodeDirections.Right,  new Vector3(0, 360, 0));        
        CurrentDirectionValue = 0;

        CurrentDirection = CardinalDirections[CurrentDirectionValue];
        StartCoroutine(InterpolateRotationSmooth(transform, m_DirectionRotations[CurrentDirection],0.0f));
        
        InputManager.Instance.m_MovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());

    }

    public void Update()
    {
      
    }

    public  IEnumerator InterpolateRotationSmooth(Transform aObject, Vector3 aTargetRotation, float aTimeUntilDone)
    {
        m_IsRotating = true;
        float timeTaken = 0f;

        Quaternion targetRotation = Quaternion.Euler(aTargetRotation);

        while (aTimeUntilDone - timeTaken > 0)
        {
            if (Quaternion.Angle(aObject.transform.localRotation, targetRotation) < 0.5f)
            {
                timeTaken = aTimeUntilDone;
            }
            
            aObject.transform.localRotation = Quaternion.Lerp( aObject.transform.localRotation, targetRotation, timeTaken/aTimeUntilDone);
            timeTaken += Time.deltaTime;
            yield return null;
        }
        aObject.localEulerAngles = aTargetRotation;
        m_IsRotating = false;
        yield return null;
    }
    public  IEnumerator DirectMovement(Transform MainObject, FloorNode  aTargetNode, float TimeUntilDone)
    {
        Vector3 NewNodePosition = new Vector3(aTargetNode.transform.position.x,aTargetNode.transform.position.y + Constants.Constants.m_HeightOffTheGrid,
            aTargetNode.transform.position.z);

        float elapsedTime = 0.0f;
        m_IsMoving = true;
        while (elapsedTime < TimeUntilDone) 
        {
            
            yield return new WaitForFixedUpdate();
            elapsedTime += Time.deltaTime;
            MainObject.position = Vector3.Lerp(MainObject.position, NewNodePosition, elapsedTime /TimeUntilDone );
        }
        
        MainObject.position = NewNodePosition;
        currentFloorNode = aTargetNode;
        aTargetNode.ActivateWalkOnTopTrigger();
        m_IsMoving = false;
        yield return 0;
    }

    
    public void PlayerMovement(Vector2 aDirection)
    {
        if (m_IsRotating == false)
        {
            RotatePlayer((int) aDirection.x);
        }

        if (m_IsMoving == false)
        {
            if (aDirection.y > 0)
            {

                if (currentFloorNode.IsDirectionWalkable(CurrentDirection))
                {
                    m_CurrentFloorManager.MoveEnemys();
                    MoveForward();
                    StepCounter++;
                }

            }

        }

        if (StepCounter >= 10)
        {
          //  m_CurrentFloorManager.SwitchToCombat();    
        }
    }

    public void RotatePlayer(int aRotateDirection)
    {
        //Inverting Direction
        int direction = aRotateDirection * -1;
        
        CurrentDirectionValue += direction;

        CheckMinAndMax();

        CurrentDirection = CardinalDirections[CurrentDirectionValue];
        
        Vector3 NewRotation = m_DirectionRotations[CurrentDirection];

        StartCoroutine(InterpolateRotationSmooth(transform, NewRotation,0.9f));
    }

    public void MoveForward()
    {
        FloorNode TargetNode = m_CurrentFloorManager.GetNode(currentFloorNode.m_PositionInGrid, CurrentDirection);
        if (TargetNode == null)
        {
            Debug.Log("Cant Find Node " + currentFloorNode.m_PositionInGrid);
            return;
        }
        
        StartCoroutine(DirectMovement(transform, TargetNode, 0.3f));

        int index = m_CurrentFloorManager.m_FloorCore.GetIndex(TargetNode.m_PositionInGrid.x,
            TargetNode.m_PositionInGrid.y);
        m_Map.SetPlayerNode(index);


    }


    public void SetPlayerMapPosition(FloorNode aFloorNode)
    {
        int index = m_CurrentFloorManager.m_FloorCore.GetIndex(aFloorNode.m_PositionInGrid.x,
            aFloorNode.m_PositionInGrid.y);
        m_Map.SetPlayerNode(index);
    }



    public void CheckMinAndMax()
    {
        if (CurrentDirectionValue > CardinalDirections.Length -1)
        {
            CurrentDirectionValue = 0;
        }
        
        if (CurrentDirectionValue < 0)
        {
            CurrentDirectionValue = CardinalDirections.Length - 1;
        }
    }
    

    
}

