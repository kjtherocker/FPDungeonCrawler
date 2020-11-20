using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeCombatArena : MonoBehaviour
{

    public Camera m_PlayerCamera;
    public GridFormations m_LevelGrid;
    public OverWorldPlayer m_OverworldPlayer;
    public bool PreloadScene = false;
    public void Start()
    {
#if UNITY_EDITOR

     //  if (PreloadScene == true)
     //  {
     //      SceneManager.LoadScene("PreloadScene", LoadSceneMode.Additive);
     //  }
#endif
        
        StartCoroutine(Initialize());

        m_LevelGrid = GetComponent<GridFormations>();
    }

    
    public IEnumerator Initialize()
    {
        
        yield return new WaitForEndOfFrame();
        
        yield return new WaitUntil(() => Preloader.Instance.m_InitializationSteps == Preloader.InitializationSteps.Finished);

        
        Debug.Log("Preload Is Initialized");
        m_OverworldPlayer.Initialize();
        // TacticsManager.Instance.CombatStart();
    }

}
