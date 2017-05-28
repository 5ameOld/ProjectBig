using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : NetworkBehaviour
{

    Transform targetCamera;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float characterHeight = 1.5f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;


    // Use this for initialization
    void Start()
    {
        /*GameObject[] lol;
        lol = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        lol = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("moi setup1");
        foreach (GameObject easy in lol)
        {
            bool n1 = easy.GetComponent<NetworkIdentity>().isLocalPlayer;
            Debug.Log(easy);
            Debug.Log(n1);
            if (n1)
            {
                targetCamera = easy.transform;
            }
            else
                return;
        }*/

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = false;
        }
    }


    public void SetMainCam(Transform trans)
    {
        targetCamera = trans;
    }


    void LateUpdate()
    {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(targetCamera.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, characterHeight, -distance);
            Vector3 position = rotation * negDistance + targetCamera.position;

            transform.rotation = rotation;
            transform.position = position;     
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}