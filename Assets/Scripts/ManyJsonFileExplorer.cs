using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-----Other Libs----
using System;
using System.IO;
using System.Linq;
using SimpleFileBrowser;
using static SimpleFileBrowser.FileBrowser;
using Newtonsoft.Json;



public class ManyJsonFileExplorer : MonoBehaviour
{
    public string JsonPath;

    public void ExcelFileBrowseOption()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("JSON Files", ".json"));
        //FileBrowser.SetDefaultFilter(".xls");
        //FileBrowser.SetFilters(false,)

        FileBrowser.ShowLoadDialog((path) => { ReceivedExcelPath(path); }, null, PickMode.FilesAndFolders, true, JsonPath, "Select JSON File", "Select");
        //ShowLoadDialog(OnSuccess onSuccess, OnCancel onCancel, PickMode pickMode, bool allowMultiSelection = false, string initialPath = null, string initialFilename = null, string title = "Load", string loadButtonText = "Select");
    }

    private void ReceivedExcelPath(string[] path)
    {
        for (int i = 0; i < path.Length; i++)
        {
            string fileNameWithExt = path[i].Substring((path[i].LastIndexOf(@"\") + 1));
            //Debug.Log(fileNameWithExt);
            string fileName = fileNameWithExt.Substring(0, fileNameWithExt.LastIndexOf('.'));
            //Debug.Log(fileName);
            Debug.Log("------File Name------: " + fileName);
            string myJsonResponse = File.ReadAllText(path[i]);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            Debug.Log("=====Area Name====: " +
                        myDeserializedClass.
                        countingAreaName
                    );
        }
    }

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

    }
}
