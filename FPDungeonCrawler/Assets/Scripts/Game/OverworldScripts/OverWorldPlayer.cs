using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class OverWorldPlayer : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    
    public float m_RotationSpeed = 0.5f;

    Vector3 m_Velocity = Vector3.zero;

    public Vector2 MoveDirection;

    public LevelNode CurrentLevelNode;
    
    public LevelNode.CardinalNodeDirections[] CardinalDirections;
    public LevelNode.CardinalNodeDirections CurrentDirection;
    public int CurrentDirectionValue;

    public GridFormations m_GridFormation;
    private Dictionary<LevelNode.CardinalNodeDirections, Vector3> m_DirectionRotations;

    public float timefucker;
    
    public void Initialize ()
    {
        CardinalDirections = new []
        {
            LevelNode.CardinalNodeDirections.Up, LevelNode.CardinalNodeDirections.Right, LevelNode.CardinalNodeDirections.Down,LevelNode.CardinalNodeDirections.Left
        };
        
        m_DirectionRotations = new Dictionary<LevelNode.CardinalNodeDirections, Vector3>();
        
        m_DirectionRotations.Add( LevelNode.CardinalNodeDirections.Down, new Vector3(0, 90, 0));
        m_DirectionRotations.Add( LevelNode.CardinalNodeDirections.Left,  new Vector3(0, 360, 0));
        m_DirectionRotations.Add( LevelNode.CardinalNodeDirections.Up, new Vector3(0, 270, 0));
        m_DirectionRotations.Add( LevelNode.CardinalNodeDirections.Right, new Vector3(0, 180, 0));
        
        
        
        CurrentDirectionValue = 0;

        CurrentDirection = CardinalDirections[CurrentDirectionValue];
        StartCoroutine(InterpolateRotationSmooth(transform, m_DirectionRotations[CurrentDirection],0.0f));
       
        
       InputManager.Instance.m_MovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());

    } 
    
    public  IEnumerator InterpolateRotationSmooth(Transform aObject, Vector3 aTargetRotation, float aTimeUntilDone)
    {
        Vector3 CurrentPostion = new Vector3(aObject.localRotation.eulerAngles.x,aObject.localRotation.eulerAngles.y,aObject.localRotation.eulerAngles.z);
        float elapsedTime = 0.0f;
        
        Quaternion targetQuaternion = Quaternion.Euler(0, aTargetRotation.y, 0);
        for(var t = 0f; t < 1; t += Time.deltaTime/aTimeUntilDone)
        {
            Debug.Log("Elapsed time test rotation " + t );
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuaternion, t * 15);
            
            yield return new WaitForFixedUpdate();
        }

        aObject.localEulerAngles = aTargetRotation;
        yield return 0;
    }
    public  IEnumerator Testo(Transform MainObject, Vector3 targetPosition, float TimeUntilDone)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < TimeUntilDone) 
        {
            
            yield return new WaitForFixedUpdate();
            Debug.Log("Elapsed time test movement " + elapsedTime );
            elapsedTime += Time.deltaTime;
            MainObject.position = Vector3.Lerp(MainObject.position, targetPosition, elapsedTime /TimeUntilDone );
        }
        
        
        MainObject.position = targetPosition;

        yield return 0;
    }

    
    public void PlayerMovement(Vector2 aDirection)
    {
        
        int direction = (int)aDirection.x * -1;
        
        CurrentDirectionValue += direction;

        CheckMinAndMax();

        CurrentDirection = CardinalDirections[CurrentDirectionValue];
        
        Vector3 NewRotation = m_DirectionRotations[CurrentDirection];

        StartCoroutine(InterpolateRotationSmooth(transform, NewRotation,0.3f));
        
       // transform.eulerAngles = m_Directions[CurrentDirection];

        if (aDirection.y > 0)
        {
           // if (CurrentLevelNode.IsDirectionWalkable(CurrentDirection))
           // {
                LevelNode TargetNode = m_GridFormation.GetNode(CurrentLevelNode.m_PositionInGrid, CurrentDirection);

                if (TargetNode == null)
                {
                    Debug.Log("Cant Find Node");
                    return;
                }

                
                Vector3 NewNodePosition = new Vector3(TargetNode.transform.position.x,TargetNode.transform.position.y + Constants.Constants.m_HeightOffTheGrid,
                    TargetNode.transform.position.z);
                
                CurrentLevelNode = TargetNode;
                StartCoroutine(Testo(transform, NewNodePosition, 0.2f));
         //   }
        }



        
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

