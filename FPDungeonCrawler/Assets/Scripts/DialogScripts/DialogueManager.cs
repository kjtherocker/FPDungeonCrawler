using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class DialogueManager : Singleton<DialogueManager>
{

    public enum ChatBoxType
    {
        White,
        Black
    }

    public enum DialogueType
    {
        DialogueBox,
        DialogueVertical
    }

    public enum FontTypes
    {
        Arial,

        NumberOfFonts
    }

    [SerializeField] Texture[] m_TypesPortraits;
    
    public DialogueTrigger m_DialogueTrigger;


    public TextMeshProUGUI m_DisplayText;
    public TextMeshProUGUI m_DisplayName;
    public Canvas m_DialogueCanvas;
    
    public RawImage m_Portrait;

    public GameObject m_DialogueBox;

    public string CurrentText;

    public PlayableBehaviour m_Timeline;
    public PlayableDirector m_ActivePlayableDirector;
    private int m_LastDialoguesMaxCharacterCount;
    private int m_MaxVerticalDialogueCharacters;
    
    public PlayerInput m_DialogueControls;

    public AudioClip DialogueClick;
    // Use this for initialization
    void Start()
    {
        m_DialogueControls = new PlayerInput();

        m_MaxVerticalDialogueCharacters = 200;
        m_DialogueControls.Player.XButton.performed += XButton => ContinueDialogue();
    }

    public void StartDialogue()
    {

        if (Constants.Constants.TurnDialogueOff == false)
        {
            UiManager.instance.PopAllTabs();
            gameObject.SetActive(true);
            m_DialogueControls.Enable();

            m_ActivePlayableDirector.gameObject.SetActive(true);
            m_DisplayText.text = "";

            InputManager.Instance.m_MovementControls.Disable();
        }
    }

    public void DisplayNextSentence(string aName,string aDialogue,bool aClearScreen)
    {
        if (aClearScreen == true)
        {
            m_DisplayText.text = "";
        }

        CurrentText = aDialogue;
        SetText(CurrentText);
        

        m_DisplayName.text = aName;
    }


    public void ContinueDialogue()
    {
        ResumeTimeline();
    }

    public void SetText(string strComplete)
    {
        StartCoroutine(AnimateText(strComplete));
    }


    public IEnumerator AnimateText(string aText)
    {
        m_DisplayText.text = aText;
        m_LastDialoguesMaxCharacterCount = 0;
        
        int TotalVisibleCharacters = m_DisplayText.textInfo.characterCount;
        int counter = m_LastDialoguesMaxCharacterCount;
        
        yield return  new WaitForSeconds(0.03f);
        
      //  while (true)
      //  {
      //      int visibleCount = counter % (TotalVisibleCharacters + 1);
      //      m_DisplayText.maxVisibleCharacters = visibleCount;
//
      //      if (visibleCount >= TotalVisibleCharacters)
      //      {
      //          if (m_CurrentDialogueType == DialogueType.DialogueVertical)
      //          {
      //              m_LastDialoguesMaxCharacterCount = TotalVisibleCharacters;
      //          }
//
      //          break;
      //      }
      //      AudioManager.Instance.PlaySoundOneShot(DialogueClick,AudioManager.Soundtypes.Dialogue);
      //      counter += 1;
      //      yield return  new WaitForSeconds(0.03f);
      //  }

    }
    
    public void PauseTimeline(PlayableDirector whichOne)
    {
        m_ActivePlayableDirector = whichOne;
        m_ActivePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    }

    public void ResumeTimeline()
    {
        m_ActivePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
    }


    public void ShowTextWithParse(string strComplete)
    {
        
    }

    public void SetPortrait(Constants.Portrait aPortrait, string sourceName = "Global")
    {
        m_Portrait.texture = m_TypesPortraits[(int)aPortrait];
    }

    public void SetFont(FontTypes aFont, string sourceName = "Global")
    {
       // m_DisplayText.fontStyle = m_TypesOfFonts[(int)aFont];

    }

    public void EndDialogue()
    {

        m_DialogueControls.Disable();
        m_DialogueCanvas.gameObject.SetActive(false);
        
        if (m_DialogueTrigger != null)
        {
            m_DialogueTrigger.DialogueIsDone = true;
        }
        m_DisplayText.text = "";
        
        m_DialogueBox.SetActive(false);
        InputManager.Instance.m_BaseMovementControls.Enable();
 
    }


}
