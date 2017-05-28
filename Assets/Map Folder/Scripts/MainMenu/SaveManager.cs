using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {

    PlayerData pd;

    public GameObject player;
    public GameObject invzwischgo;

    Inventory maininv;

    

    void Start()
    {
        if (maininv == null)
            maininv = invzwischgo.GetComponent<Inventory>();

        pd = new PlayerData();
        pd.playerPos = new float[3];
        //pd.playerInv = new List<Item>();

        //Load();
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/playerInfo.dat", FileMode.Open);
            pd = (PlayerData)bf.Deserialize(file);

            DataUpdaterLoad();

            file.Close();

        }
        else
            Debug.Log("no file there");
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/playerInfo.dat");

        DataUpdaterSave();

        bf.Serialize(file, pd);
        file.Close();
    }

    void DataUpdaterLoad()
    {
        player = transform.Find("Player(Clone)").gameObject;
        //player = GameObject.FindGameObjectWithTag("Player");
        //set player
        player.transform.position = new Vector3(pd.playerPos[0], pd.playerPos[1], pd.playerPos[2]);
        //set inventory
        maininv.SetArrAsInv(pd.playerInv);
        //debug
        if (pd.playerInv.Length == 0)
        {
            //Debug.Log("u suck bro");
        }
        for (int x = 0; x < pd.playerInv.Length; x += 2)
        {
            Debug.Log(pd.playerInv[x] + " " + pd.playerInv[x + 1]);
        }
        Debug.Log(new Vector3(pd.playerPos[0], pd.playerPos[1], pd.playerPos[2]));
        Debug.Log("Loaded");
    }

    void DataUpdaterSave()
    {
        pd.playerPos[0] = player.transform.position.x;
        pd.playerPos[1] = player.transform.position.y;
        pd.playerPos[2] = player.transform.position.z;
        pd.playerInv = maininv.GetInvAsArray();

        //maininv.updateItemList();
        for (int x = 0; x < pd.playerInv.Length; x+=2)
        {
            Debug.Log(pd.playerInv[x] + " " + pd.playerInv[x+1]);
        }     
        Debug.Log("Saved");
    }
}

[Serializable]
public class PlayerData
{
    public float[] playerPos;
    public int[] playerInv;
}