﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialogueTrigger : MonoBehaviour
{

    public enum TriggerType
    {
        Default,
        Menu,
        WaitForObjectToCome,
        OnAwake,
        Arena
    }





    public bool DialogueHasHappend;


  //  public UiManager m_UiScreen;

    public DialogueManager.DialogueType m_DialogueType;
    
    
    public AudioClip m_Audioclip;

    public TextAsset m_JsonFile;

    public TriggerType m_TriggerType;

    public bool DialogueIsDone;
    
    public PlayerMovementController mBasePlayerMovementController;

    public DialogueTimelineHandler m_DialogueTimeLineHandler;
    
    public void Start()
    {
        DialogueHasHappend = false;
        //DeSerializeJsonDialogue(m_JsonFile);

        if (m_TriggerType == TriggerType.OnAwake)
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (DialogueHasHappend == false)
            {
                if (m_TriggerType == TriggerType.Default)
                {
                //    TriggerDialogue();
                }
                else if (m_TriggerType == TriggerType.Menu)
                {
                  //  UiManager.Instance.PushScreen(m_UiScreen);
          
                }
                else if (m_TriggerType == TriggerType.WaitForObjectToCome)
                {
                }
            }
        }
    }


    public void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        if (m_DialogueTimeLineHandler.gameObject != null)
        {
            Instantiate(m_DialogueTimeLineHandler,gameObject.transform);
        }

        
        DialogueManager.Instance.m_DialogueTrigger = this;
        DialogueManager.Instance.StartDialogue();
        DialogueHasHappend = true;
        DialogueIsDone = false;
    }

}
