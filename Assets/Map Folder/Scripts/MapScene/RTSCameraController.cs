using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraController : MonoBehaviour {


    //private GameObject pGO;
    //GameObject pCamGO;
    GameObject rtsCamGO;
    GameObject pCamGO;

    bool invoked = false;

    float rEndPosX;
    float rEndPosXe;
    float rEndPosY;
    float rEndPosYe;

    bool tY;
    bool rX;
    bool rYl;

    bool negX = false;
    bool negY = false;

	void Start ()
    {
        pCamGO = GameObject.FindGameObjectWithTag("MainCamera");
        rtsCamGO = GameObject.FindGameObjectWithTag("RTSCamera");

        rEndPosX = 55;
        rEndPosXe = 57;
        rEndPosY = 0;
        rEndPosYe = 1;
	}

	void LateUpdate ()
    {
		if(Input.GetButtonDown("RTSMap"))
        {
            //Camera active switch
            pCamGO.SetActive(false);
            rtsCamGO.SetActive(true);
            
            //Set start pos
            rtsCamGO.transform.position = new Vector3(pCamGO.transform.position.x, 2, pCamGO.transform.position.z);
            rtsCamGO.transform.rotation = pCamGO.transform.rotation;

            transRTSCam();
        }  
	}
    void transRTSCam()
    {
        //Startup checks
        if (!invoked)
        {
            //Round the numbers
            rtsCamGO.transform.position = new Vector3(Mathf.Round(rtsCamGO.transform.position.x), Mathf.Round(rtsCamGO.transform.position.y), Mathf.Round(rtsCamGO.transform.position.z)); 
            rtsCamGO.transform.rotation = Quaternion.Euler(Mathf.Round(pCamGO.transform.rotation.eulerAngles.x), Mathf.Round(pCamGO.transform.rotation.eulerAngles.y), pCamGO.transform.rotation.eulerAngles.z);

            //Set in wich side the camera rotates 
            if (rEndPosX > rtsCamGO.transform.rotation.eulerAngles.x)
            {
                negX = true;
            }
            if (180 < rtsCamGO.transform.rotation.eulerAngles.y)
            {
                negY = true;
            }
            invoked = true;
        }

        //Transform position x
        if (rtsCamGO.transform.position.y <= 60)
        {
            rtsCamGO.transform.position += new Vector3(0, 1f, 0);
        }
        else
        { 
            tY = true;
        }

        //Transform rotation x
        if (negX && (rtsCamGO.transform.rotation.eulerAngles.x <= rEndPosX || rEndPosXe <= rtsCamGO.transform.rotation.eulerAngles.x))
        {
            rtsCamGO.transform.rotation = Quaternion.Euler(rtsCamGO.transform.rotation.eulerAngles.x + 1f, rtsCamGO.transform.rotation.eulerAngles.y, rtsCamGO.transform.rotation.eulerAngles.z);
        }
        else if (!negX && (rtsCamGO.transform.rotation.eulerAngles.x <= rEndPosX || rEndPosXe <= rtsCamGO.transform.rotation.eulerAngles.x))
        {
            rtsCamGO.transform.rotation = Quaternion.Euler(rtsCamGO.transform.rotation.eulerAngles.x - 1f, rtsCamGO.transform.rotation.eulerAngles.y, rtsCamGO.transform.rotation.eulerAngles.z);
        }
        else
        {
            rX = true;
        }

        //Transform rotation y
        if (negY && (rtsCamGO.transform.rotation.eulerAngles.y <= rEndPosY || rEndPosYe <= rtsCamGO.transform.rotation.eulerAngles.y))
        {
            rtsCamGO.transform.rotation = Quaternion.Euler(rtsCamGO.transform.rotation.eulerAngles.x, rtsCamGO.transform.rotation.eulerAngles.y + 1f, rtsCamGO.transform.rotation.eulerAngles.z);
        }
        else if (!negY && (rtsCamGO.transform.rotation.eulerAngles.y <= rEndPosY || rEndPosYe <= rtsCamGO.transform.rotation.eulerAngles.y))
        {
            rtsCamGO.transform.rotation = Quaternion.Euler(rtsCamGO.transform.rotation.eulerAngles.x, rtsCamGO.transform.rotation.eulerAngles.y - 1f, rtsCamGO.transform.rotation.eulerAngles.z);
        }
        else
        {
            rYl = true;
        }

        //Invoke
        if (tY && rX && rYl)
        {
            return;
        }
        else
        {
            Invoke("transRTSCam", 0.1f);
        }
        
    }
}
