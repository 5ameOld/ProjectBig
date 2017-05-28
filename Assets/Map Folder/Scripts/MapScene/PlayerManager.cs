using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{

   public override void OnStartLocalPlayer()
    {
       base.OnStartLocalPlayer();

       //Set MainCamera active
       this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
       //Set MainCamera in MouseOrbitImproved
       this.gameObject.transform.GetChild(0).gameObject.GetComponent<MouseOrbitImproved>().SetMainCam(this.gameObject.transform);
       //Set MainCamera in MeeleSystem
       this.gameObject.GetComponent<MeeleSystem>().SetMainCam(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>());
       //Set Player in StorageInventory
       GameObject[] piArr;
       piArr = new GameObject[GameObject.FindGameObjectsWithTag("StorageGO").Length];
       piArr = GameObject.FindGameObjectsWithTag("StorageGO");
       foreach (GameObject easy in piArr)
       {
           Debug.Log(easy);
           easy.GetComponent<StorageInventory>().SetPlayerGO(this.gameObject);
       }
       //Set Player in Marketplace
       GameObject[] mpArr;
       mpArr = new GameObject[GameObject.FindGameObjectsWithTag("MarketplaceGO").Length];
       mpArr = GameObject.FindGameObjectsWithTag("MarketplaceGO");
       foreach (GameObject easy in mpArr)
       {
           Debug.Log(easy);
           easy.GetComponent<Marketplace>().SetPlayerGO(this.gameObject);
       }
    }
}
