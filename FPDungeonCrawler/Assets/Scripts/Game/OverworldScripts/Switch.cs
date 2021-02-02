using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Switch : MonoBehaviour
{
    enum SwitchState
    {
        On,
        Off
    }
    
    private bool m_IsSwitchOn;
    public GameObject m_SwitchObject;
    private Dictionary<SwitchState, Vector3> m_DirectionRotations;
    private float m_SwitchMovementLength;
    private bool m_isSwitchMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        m_DirectionRotations = new Dictionary<SwitchState, Vector3>();
        
        m_DirectionRotations.Add( SwitchState.On, new Vector3(0,0,55));
        m_DirectionRotations.Add( SwitchState.Off, new Vector3(0,0,125));

        m_SwitchMovementLength = 1;
        
        Interact(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Interact();
        }
    }

    public void Interact(bool aSetSwitchPosition)
    {
        m_IsSwitchOn = aSetSwitchPosition;
        
        SwitchState SwitchState = m_IsSwitchOn ? SwitchState.On : SwitchState.Off;

        StartCoroutine(
            RotateSwitch(m_SwitchObject.transform, m_DirectionRotations[SwitchState], m_SwitchMovementLength));

    }
    
    public void Interact()
    {
        if (m_isSwitchMoving)
        {
            return;
        }

        m_IsSwitchOn = !m_IsSwitchOn;
        
        SwitchState SwitchState = m_IsSwitchOn ? SwitchState.On : SwitchState.Off;

        StartCoroutine(
            RotateSwitch(m_SwitchObject.transform, m_DirectionRotations[SwitchState], m_SwitchMovementLength));

    }

    public IEnumerator RotateSwitch(Transform aObject, Vector3 aTargetRotation, float aTimeUntilDone)
    {
        m_isSwitchMoving = true;
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
        m_isSwitchMoving = false;
        yield return null;
    }
}
