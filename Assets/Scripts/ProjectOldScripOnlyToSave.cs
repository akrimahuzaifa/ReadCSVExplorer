using SimpleFileBrowser;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using static SimpleFileBrowser.FileBrowser;
using System.Linq;
using TMPro;
using Newtonsoft.Json;

public class ProjectOldScripOnlyToSave : MonoBehaviour
{
    [SerializeField] GameObject togglePrefab;
    [SerializeField] GameObject infoTextPrefab;
    List<String> fileNameList = new List<string>();
    GameObject readingContParent;
    GameObject viewContParent;
    GameObject timePanelContent;
    GameObject frequencyPanelContent;
    public string JsonPath;

    private void Awake()
    {
        readingContParent = GameObject.Find("MainPanel").
                            transform.Find("ReadCountingAreaPanel/Scroll View/Viewport/Content").
                            gameObject;

        viewContParent = GameObject.Find("MainPanel").
                         transform.Find("ViewCountingAreaPanel/Scroll View/Viewport/Content").
                         gameObject;

        timePanelContent = GameObject.Find("MainPanel").
                           transform.Find("SetandStartTimePanel/Scroll View/Viewport/Content").
                           gameObject;

        frequencyPanelContent = GameObject.Find("MainPanel").
                                transform.Find("SetFrequencyDataPanel/Scroll View/Viewport/Content").
                                gameObject;
    }

    private void Start()
    {

    }

    //=================Function trigger in ReadCountingAreaPanel "Select Files" Buttons Click=========================
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
        ToggleInstantiator(path);
    }


    //================Instantiate Toggle of the Files selected====================================
    public void ToggleInstantiator(string[] path)
    {
        for (int i = 0; i < path.Length; i++)
        {
            string fileNameWithExt = path[i].Substring(path[i].LastIndexOf(@"\") + 1);
            string fileName = fileNameWithExt.Substring(0, fileNameWithExt.LastIndexOf('.'));
            Debug.Log("------File Name------: " + fileName);

            string myJsonResponse = File.ReadAllText(path[i]);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            Debug.Log("=====Area Name====: " +
                        myDeserializedClass.
                        countingAreaName
                    );


            if (!fileNameList.Contains(fileName))
            {
                fileNameList.Add(fileName);

                //-------------------Instantiate Toggle----------------------------
                var toggleHolder = Instantiate(togglePrefab.gameObject, readingContParent.transform);
                toggleHolder.GetComponent<Toggle>().isOn = false;

                //-------------------Changing Label of Instantiated Toggle----------------------------
                var label = readingContParent.transform.GetChild(fileNameList.Count - 1).gameObject.transform.GetChild(1).gameObject;
                label.GetComponent<Text>().text = fileName;

                //------------------Adding OnValueChange LISTENER-----------------------
                toggleHolder.GetComponent<Toggle>().onValueChanged.AddListener(delegate
                {
                    CheckToggleStatus(toggleHolder.GetComponent<Toggle>(), path);
                });
            }
        }
    }

    public void CheckToggleStatus(Toggle staticToggleIndividual, string[] path)
    {
        if (staticToggleIndividual.isOn == true)
        {
            //-------Move toggles into View Counting Panel that are checked from Read Counting Panel-----------------
            if (staticToggleIndividual.transform.parent.gameObject != viewContParent)
            {
                //staticToggleIndividual.transform.SetParent(viewContParent.transform);
                staticToggleIndividual.transform.parent = viewContParent.transform;

                ReadJSONFile(path, staticToggleIndividual);
            }
        }
        else
        {
            if (staticToggleIndividual.transform.parent.gameObject != readingContParent)
            {
                staticToggleIndividual.transform.SetParent(readingContParent.transform);
            }
        }
    }

    public void ReadJSONFile(string[] path, Toggle staticToggleIndividual)
    {
        var lines = File.ReadAllLines(path[0]).Select(a => a.Split(';'));
        var rows = (from line in lines
                    select line.ToArray() // get all columns
                    ).Skip(1).ToArray(); // skipping the "sep=;" separator line & take all rows

        for (int j = 0; j < rows.Length; j++)
        {
            Debug.Log(rows[j][0]);
        }

        /*        for (int i = 0; i < rows.Length; i++)
                {
                    for (int j = 0; j < rows[i].Length; j++)
                    {
                        Debug.Log(rows[i][j]);
                    }
                }*/
        /*
        //===========================Method to see files in a directory==================
        string pathToFiles = path[0].Substring(0, path[0].LastIndexOf(@"\"));
        int count = 0;

        DirectoryInfo place = new DirectoryInfo(pathToFiles);
        FileInfo[] files = place.GetFiles();

        foreach (FileInfo file in files)
        {

            Debug.Log("File Name " + count + " - " + file.Name);
            count++;
        }
        //=================================================================================
*/
        InstantiateInfoText(rows, staticToggleIndividual);
    }

    public void InstantiateInfoText(string[][] fileData, Toggle staticToggleIndividual)
    {
        string toggleText = staticToggleIndividual.
                            transform.GetChild(1).
                            GetComponent<Text>().
                            text;

        //------------------Set & Start Time Panel Content work-------------------------------
        var infoTextHolder = Instantiate(infoTextPrefab, timePanelContent.transform);
        infoTextHolder.
            transform.GetChild(0).
            transform.GetChild(1).
            GetComponent<Text>().
            text = toggleText;


        var cellData = fileData[0].Select(a => a.Split(',')).ToArray();
        //Debug.Log("====Cell Data:==== " + cellData[0][5]);

        string dateCol = "Date";
        int index = Array.IndexOf(cellData[0], dateCol);
        //Debug.Log("====Index of Date:==== " + index);
        //Debug.Log("Length of Parent Array: " + cellData.Length);



        /*        infoTextHolder.
                    transform.
                    GetChild(1).
                    GetComponent<TMP_Text>().
                    text = cellData[1][index] + " " + cellData[1][index + 1] + "-" + cellData[1][index + 2];*/

        /*//------------------Frequency Data Panel Content work-------------------------------
        var frequencyDataHolder = Instantiate(infoTextPrefab, frequencyPanelContent.transform);
        frequencyDataHolder.
            transform.
            Find("Toggle/Label").
            GetComponent<Text>().
            text = toggleText;

        frequencyDataHolder.
            transform.
            Find("Text (TMP)").
            GetComponent<TMP_Text>().
            text = data[index + 1] + " " + data[index + 2] + "-" + data[index + 3];*/
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

}
