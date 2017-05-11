using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleSystem : MonoBehaviour {

    public int minDamage = 25;
    public int maxDamage = 50;
    public float weaponRange = 3.5f;

    public Camera FPSCamera;

    private ChopTree treeHealth;

    private int layerMask = 1 << 8;

    

	// Update is called once per frame
	void Update ()
    {
        layerMask = ~layerMask;

        Ray ray = FPSCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        RaycastHit hitInfo;

        Debug.DrawRay(ray.origin, ray.direction * weaponRange, Color.green);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hitInfo, weaponRange, layerMask))
            {
                if (hitInfo.collider.tag == "Tree")
                {
                    treeHealth = hitInfo.collider.GetComponentInParent<ChopTree>();
                    AttackTree();
                }
            }
        }
	}

    private void AttackTree()
    {
        Debug.Log("Hit Tree" + "Tree Health" + treeHealth.health);
        treeHealth.health -= maxDamage;
        Debug.Log("Hit Tree" + "Tree Health" + treeHealth.health);
    }

}
