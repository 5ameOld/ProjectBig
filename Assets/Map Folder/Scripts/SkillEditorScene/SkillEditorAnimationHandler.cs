using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillEditorAnimationHandler : MonoBehaviour {

    public Animator LittleFootAnimator;

    public void AnimationDown()
    {
        int intnow = LittleFootAnimator.GetInteger("intAC");
        if (intnow != 1)
        {
            intnow--;
            LittleFootAnimator.SetInteger("intAC", intnow);
            Debug.Log(intnow);
        }
        else
            Debug.Log("sup1");
    }

    public void AnimationUp()
    {
        int intnow = LittleFootAnimator.GetInteger("intAC");
        if (intnow != 3)
        {
            intnow++;
            LittleFootAnimator.SetInteger("intAC", intnow);
            Debug.Log(intnow);
        }
        else
            Debug.Log("sup2");
    }

    public void Return() 
    {
        int i = 1;
        SceneManager.LoadScene(i);
    }
}
