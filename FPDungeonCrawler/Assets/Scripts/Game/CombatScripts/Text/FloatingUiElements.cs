using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingUiElements : MonoBehaviour {

    public Animator animator;
    public TextMeshProUGUI DamageText;
	// Use this for initialization
	void Start ()
    {
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfos[0].clip.length);
      //  DamageText = animator.GetComponent<Text>();
	}

    public void SetText(string a_text)
    {
        DamageText = animator.GetComponent<TextMeshProUGUI>();
        DamageText.text = a_text;
    }
    public void DestroyAll()
    {
        Destroy(gameObject);
    }

}
