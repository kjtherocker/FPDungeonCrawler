﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : Singleton<Preloader>
{

    public enum InitializationSteps
    {
        GameManager,
        PartyManager,
        Ui,
        Input,
        Audio,
        Finished
        
    }


    // Start is called before the first frame update

     public InitializationSteps m_InitializationSteps;
     public Camera m_TestCamera;

     
    void Start()
    {
        InitializePreloadObjects();
    }

    public void InitializePreloadObjects()
    {
       // m_TestCamera.gameObject.SetActive(false);
        
        
        m_InitializationSteps = InitializationSteps.GameManager;
        GameManager.Instance.Initialize();
        SkillList.instance.Initialize();
        EnemyList.instance.Initialize();
        ItemList.instance.Initialize();
        ItemManager.instance.Initialize();
        
        m_InitializationSteps = InitializationSteps.PartyManager;
        PartyManager.instance.Initialize();

        
        
        m_InitializationSteps = InitializationSteps.Ui;
        UiManager.Instance.Initialize();
        PressTurnManager.instance.Initialize();

        UiManager.Instance.PushTab(UiManager.UiTab.PlayerStatus);
        
        m_InitializationSteps = InitializationSteps.Audio;
        AudioManager.instance.Intialize();

                
        m_InitializationSteps = InitializationSteps.Input;
        if (InputManager.Instance == null)
        {
            gameObject.GetComponentInChildren<InputManager>().Initialize();
        }
        else
        {
            InputManager.instance.Initialize();
        }
        
        
        m_InitializationSteps = InitializationSteps.Finished;

    }
}
