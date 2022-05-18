using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadProjectJSON : MonoBehaviour
{
    public class PeopleSimulationInfo
    {
        public int id { get; set; }
        public List<PositionsOverTime> positionsOverTime { get; set; }
    }

    public class PositionsOverTime
    {
        public string position { get; set; }
        public string Time { get; set; }
        public string operation { get; set; }
    }

    public class Root
    {
        public string countingAreaName { get; set; }
        public string cameraName { get; set; }
        public List<string> areaGPS { get; set; }
        public int platformLevel { get; set; }
        public List<PeopleSimulationInfo> peopleSimulationInfo { get; set; }
    }

    private void Start()
    {
        string myJsonResponse = File.ReadAllText(Application.dataPath + "/Resources/AreaExample.json");
        //Debug.Log("-----My Json Text----:  " + myJsonResponse);

        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        //Debug.Log("AreaName====: " + myDeserializedClass.countingAreaName);
        //Debug.Log("=====CameraName====: " + myDeserializedClass.cameraName);
        /*        Debug.Log("=====Person ID====: " + myDeserializedClass.peopleSimulationInfo[0].id);
                Debug.Log("=====Person Position====: " + 
                            myDeserializedClass.
                            peopleSimulationInfo[0].
                            positionsOverTime[0].
                            position
                           );
                Debug.Log("=====Person Time====: " +
                            myDeserializedClass.
                            peopleSimulationInfo[0].
                            positionsOverTime[0].
                            Time
                        );
                Debug.Log("=====Person Operation====: " +
                            myDeserializedClass.
                            peopleSimulationInfo[0].
                            positionsOverTime[0].
                            operation
                       );*/

        Debug.Log("=====Area GPS====: " +
                    myDeserializedClass.
                    areaGPS[0]
                );
    }

}
