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
        UpdateTurnWrapper();
    }

    public  IEnumerator SetIconOpacity( int aOpacity, float aTimeUntilDone)
    {

      // float timeTaken = 0f;


      // Color newColor = m_DefaultIcon.color;
      // 
      // while (aTimeUntilDone - timeTaken > 0)
      // {
      //     if (Quaternion.(aObject.transform.localRotation, targetRotation) < 0.5f)
      //     {
      //         timeTaken = aTimeUntilDone;
      //     }


      //     float testo = newColor.a;
      //     
      //     
      //     newColor.a = Vector3.Lerp(testo, aOpacity, timeTaken/aTimeUntilDone);
      //     timeTaken += Time.deltaTime;
      //     yield return null;
      // }
      // aObject.localEulerAngles = aTargetRotation;
  
        yield return null;
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
