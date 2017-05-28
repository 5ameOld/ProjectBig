using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Marketplace : NetworkBehaviour {

    public int distanceToOpenStorage;
    public float timeToOpenStorage;
    public GameObject inventory;

    private ItemDataBaseList itemDatabase;
    private InputManager inputManagerDatabase;
    
    List<Item> storageItems = new List<Item>();    
    
    GameObject player;


    float startTimer;
    float endTimer;
    bool showTimer;

    bool closeInv;
    bool showStorage;

    Inventory inv;
    Tooltip tooltip;

    public void SetPlayerGO(GameObject pl)
    {
        player = pl;
    }

	void Start ()
    {
        inv = inventory.GetComponent<Inventory>();
        ItemDataBaseList inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        if (inputManagerDatabase == null)
            inputManagerDatabase = (InputManager)Resources.Load("InputManager");

        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
	}
	

	void Update ()
    {
        if (!isLocalPlayer)
            return;

        float distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        if (distance <= distanceToOpenStorage && Input.GetKeyDown(inputManagerDatabase.StorageKeyCode))
        {
            showStorage = !showStorage;
            StartCoroutine(OpenInventoryWithTimer());
        }
    }

    /*public void addItemToStorage(int id, int value)
    {
        Item item = itemDatabase.getItemByID(id);
        item.itemValue = value;
        //storageItems.Add(item);
    }*/


    public void setImportantVariables()
    {
        if (itemDatabase == null)
            itemDatabase = (ItemDataBaseList)Resources.Load("ItemDatabase");
    }

    IEnumerator OpenInventoryWithTimer()
    {
        if (showStorage)
        {
            startTimer = Time.time;
            showTimer = true;
            yield return new WaitForSeconds(timeToOpenStorage);
            if (showStorage)
            {
                inv.ItemsInInventory.Clear();
                inventory.SetActive(true);
                addItemsToInventory();
                showTimer = false;  
            }
        }
        else
        {
            storageItems.Clear();
            setListofStorage();
            inventory.SetActive(false);
            inv.deleteAllItems();
            tooltip.deactivateTooltip();
        }
    }
    void setListofStorage()
    {
        Inventory inv = inventory.GetComponent<Inventory>();
        storageItems = inv.getItemList();
    }

    void addItemsToInventory()
    {
        Inventory iV = inventory.GetComponent<Inventory>();
        for (int i = 0; i < storageItems.Count; i++)
        {
            //iV.addItemToInventory(storageItems[i].itemID, storageItems[i].itemValue);
        }
        iV.stackableSettings();
    }

}
