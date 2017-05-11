using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChopTree : MonoBehaviour {

    public int health = 100;

    public GameObject TreeChest;
    public int spawnTimeStart;
    public int LootitemID;

    int spawnTime;

    static ItemDataBaseList inventoryItemList;
    GameObject zwischenschritt;

    Inventory inve;

    GameObject chest;

    int respawncounter = 0;

    void Start()
    {
        health = 100;
        spawnTime = spawnTimeStart;
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");
        zwischenschritt = GameObject.FindGameObjectWithTag("LootStorage");
        
    }

	void Update ()
    {

        if (health <= 0 && respawncounter == 0)
        {
            //Baum deaktivieren
            gameObject.SetActive(false);

            //Lootchest spawnen
            chest = (GameObject)Instantiate(TreeChest);

            StorageInventory sI = chest.GetComponent<StorageInventory>();
            sI.inventory = zwischenschritt;
             
            sI.storageItems.Add(inventoryItemList.getItemByID(LootitemID).getCopy());
            int randomValue = UnityEngine.Random.Range(1, 5);
            sI.storageItems[sI.storageItems.Count - 1].itemValue = randomValue;
            
            //inve.stackableSettings();
            
            respawncounter = 1;

            spawnTime = spawnTimeStart;

            Debug.Log("respawncounter" + respawncounter);
            Debug.Log("spawntime (treedeath)" + spawnTime);

            Invoke("_tick", 1f);
        }

	}

    void _tick()
    {
        spawnTime--;
        Debug.Log("spawntime" + spawnTime);

        if (spawnTime > 0)
        {
            Invoke("_tick", 1f);
        }
        else
        {
            Treerespawn();
        }

    }

    void Treerespawn()
    {
        gameObject.SetActive(true);
        Destroy(chest);
        health = 100;
        respawncounter = 0;
    }

 
}
