using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JASONReader : MonoBehaviour
{
    private class PlayerData
    {
        public Vector3 position;
        public int health;
    }

    private void Start()
    {
        /*        Debug.Log("Started Jason file");
                PlayerData playerData = new PlayerData();
                playerData.position = new Vector3(5, 1);
                playerData.health = 100;

                string json = JsonUtility.ToJson(playerData);
                Debug.Log(json);

                File.WriteAllText(Application.dataPath + @"\Resources\saveFile.json", json);
        */


        string json = File.ReadAllText(Application.dataPath + "/Resources/saveFile.json");

        PlayerData loadPlayerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("Position====: " + loadPlayerData.position);
        Debug.Log("=====Health====: " + loadPlayerData.health);

    }
}
