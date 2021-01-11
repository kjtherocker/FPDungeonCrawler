using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeCombatArena : MonoBehaviour
{

    public Camera m_PlayerCamera;
    public Camera m_CombatCamera;
    public FloorManager m_FloorManager;
    public PlayerMovementController mOverworldPlayerMovementController;
    public bool PreloadScene = false;
    public UiMap m_UiMap;
    public void Start()
    {
#if UNITY_EDITOR

     //  if (PreloadScene == true)
     //  {
     //      SceneManager.LoadScene("PreloadScene", LoadSceneMode.Additive);
     //  }
#endif
        
        StartCoroutine(Initialize());

      //  m_LevelGrid = GetComponent<GridFormations>();
    }

    
    public IEnumerator Initialize()
    {
        
        yield return new WaitForEndOfFrame();
        
        yield return new WaitUntil(() => Preloader.Instance.m_InitializationSteps == Preloader.InitializationSteps.Finished);

        
        Debug.Log("Preload Is Initialized");

        m_UiMap.SetMap(m_FloorManager.m_FloorCore);
        m_FloorManager.Initialize();
        mOverworldPlayerMovementController.Initialize();

        // TacticsManager.Instance.CombatStsart();
    }

}
