using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscHandler : MonoBehaviour {

    public Canvas EscCanv;
    public GameObject gc;

    SaveManager sm;

    bool canvActive;

    void Start()
    {
        sm = gc.GetComponent<SaveManager>();

        EscCanv.enabled = false;
        canvActive = false;
    }

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!canvActive)
            {
                EscCanv.enabled = true;
                canvActive = true;
                Cursor.lockState = CursorLockMode.None; 
            }
            else
            {
                EscCanv.enabled = false;
                canvActive = false;
                Cursor.lockState = CursorLockMode.Locked; 
            }
        }
	}

    public void ResumeButton()
    {
        EscCanv.enabled = false;
        canvActive = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    public void OptionButton()
    {
        sm.Save();
    }

    public void SkillEditorButton()
    {
        int i = 2;
        SceneManager.LoadScene(i);
        PlayerPrefs.SetFloat("X", transform.position.x);
        PlayerPrefs.SetFloat("Y", transform.position.y);
        PlayerPrefs.SetFloat("Z", transform.position.z);
    }

    public void ExitButton()
    {
        PlayerPrefs.DeleteAll();

        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
