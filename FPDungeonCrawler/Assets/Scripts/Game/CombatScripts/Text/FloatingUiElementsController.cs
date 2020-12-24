using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUiElementsController : MonoBehaviour {

    private static FloatingUiElements FloatingText;
    private static FloatingUiElements FloatingWeak;
    private static FloatingUiElements FloatingStrong;
    private static FloatingUiElements FloatingMiss;
    private static FloatingUiElements FloatingAttackUp;
    private static FloatingUiElements FloatingAttackDown;
    private static FloatingUiElements FloatingDefenseUp;
    private static FloatingUiElements FloatingSpeedUp;
    private static GameObject canvas;

    public enum UiElementType
    {
        Text,
        Miss,
        Strong,
        Weak,
        Attackup,
        AttackDown,
        Defenseup,
        SpeedUp
    }

    public UiElementType uiElementType;
    // Use this for initialization
    public static void Initalize()
    {
        canvas = GameObject.Find("Canvas_Numbers");

        if (!FloatingText)
        {
            FloatingText = (FloatingUiElements)Resources.Load("Ui/Indicators/Prefab/Prefab_Text_DamageParent", typeof(FloatingUiElements));
        }
        if (!FloatingWeak)
        {
            FloatingWeak = (FloatingUiElements)Resources.Load("Ui/Indicators/Prefab/Prefab_Image_Weak_Parent", typeof(FloatingUiElements));
        }
        if (!FloatingStrong)
        {
            FloatingStrong = (FloatingUiElements)Resources.Load("Ui/Indicators/Prefab/Prefab_Image_Strong_Parent", typeof(FloatingUiElements));
        }
        if (!FloatingMiss)
        {
            FloatingMiss = (FloatingUiElements)Resources.Load("Ui/Indicators/Prefab/Prefab_Image_Miss_Parent", typeof(FloatingUiElements));
        }
        if (!FloatingAttackUp)
        {
            FloatingAttackUp = (FloatingUiElements)Resources.Load("Ui/Indicators/Prefab/Prefab_Image_AttackUp_Parent", typeof(FloatingUiElements));
        }

        if (!FloatingAttackDown)
        {
            FloatingAttackDown = (FloatingUiElements)Resources.Load("Ui/Indicators/Prefab/Prefab_Image_AttackDown_Parent", typeof(FloatingUiElements));
        }
        if (!FloatingDefenseUp)
        {
            FloatingDefenseUp = (FloatingUiElements)Resources.Load("Ui/Indicators/Prefab/Prefab_Image_DefenseUp_Parent", typeof(FloatingUiElements));
        }
        if (!FloatingSpeedUp)
        {
            FloatingSpeedUp = (FloatingUiElements)Resources.Load("Ui/Indicators/Prefab/Prefab_Image_SpeedUp_Parent", typeof(FloatingUiElements));
        }

    }


    public static void CreateFloatingText(string text, Transform location, UiElementType a_uiElementtype,bool IsUi)
    {
        Camera Camera = TacticsManager.instance.m_CombatCamera;
        Vector2 screenPosition = Vector2.zero;
        if (IsUi)
        {
            screenPosition = location.position;
             
        }
        else
        {
            screenPosition = Camera.WorldToScreenPoint(location.position + Vector3.up);
        }
        
        
        
        if (a_uiElementtype == UiElementType.Text)
        {
            FloatingUiElements instance = Instantiate(FloatingText);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;
            
            instance.SetText(text);
        }
        if (a_uiElementtype == UiElementType.Strong)
        {
            FloatingUiElements instance = Instantiate(FloatingStrong);
            instance.transform.SetParent(canvas.transform, false);

            instance.transform.position = screenPosition;


        }
        if (a_uiElementtype == UiElementType.Weak)
        {
            FloatingUiElements instance = Instantiate(FloatingWeak);
            instance.transform.SetParent(canvas.transform, false);

            instance.transform.position = screenPosition;
            

        }
        if (a_uiElementtype == UiElementType.Miss)
        {
            FloatingUiElements instance = Instantiate(FloatingMiss);
            instance.transform.SetParent(canvas.transform, false);

            instance.transform.position = screenPosition;
            

        }
        if (a_uiElementtype == UiElementType.Attackup)
        {
            FloatingUiElements instance = Instantiate(FloatingAttackUp);
            instance.transform.SetParent(canvas.transform, false);

            instance.transform.position = screenPosition;
            

        }
        if (a_uiElementtype == UiElementType.AttackDown)
        {
            FloatingUiElements instance = Instantiate(FloatingAttackDown);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }
        if (a_uiElementtype == UiElementType.Defenseup)
        {
            FloatingUiElements instance = Instantiate(FloatingDefenseUp);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }
        if (a_uiElementtype == UiElementType.SpeedUp)
        {
            FloatingUiElements instance = Instantiate(FloatingSpeedUp);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }


    }

 
}
