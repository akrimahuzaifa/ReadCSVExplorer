using SimpleFileBrowser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static SimpleFileBrowser.FileBrowser;

public class ReadCSV : MonoBehaviour
{
    public string ExcelPath;
    List<string> cellsData = new List<string>();

    private void Start()
    {
        //ExcelFileBrowseOption();
    }

    public void ExcelFileBrowseOption()
    {

        FileBrowser.SetFilters(false, new FileBrowser.Filter("Excel Files", ".xls", ".xlsx", ".aes", ".csv"));
        //FileBrowser.SetDefaultFilter(".xls");
        //FileBrowser.SetFilters(false,)

        FileBrowser.ShowLoadDialog((path) => { ReceivedExcelPath(path); }, null, PickMode.FilesAndFolders, true, ExcelPath, "Select Excel File", "Select");
        //ShowLoadDialog(OnSuccess onSuccess, OnCancel onCancel, PickMode pickMode, bool allowMultiSelection = false, string initialPath = null, string initialFilename = null, string title = "Load", string loadButtonText = "Select");
    }

    private void ReceivedExcelPath(string[] path)
    {
        //Debug.Log(path.Length);
        List<String> filePath = new List<string>();
        
        for (int i = 0; i < path.Length; i++)
        {
            string fileNameWithExt = path[i].Substring((path[i].LastIndexOf(@"\") + 1));
            //Debug.Log(fileNameWithExt);
            string fileName = fileNameWithExt.Substring(0, fileNameWithExt.LastIndexOf('.'));
            //Debug.Log(fileName);



            //Debug.Log("path " + path[i]);
            //Debug.Log("Back Slash: " + (path[i].LastIndexOf(@"\")+1)); 
            //Debug.Log("path Length: " + path[i].Length);
            //Debug.Log(path[i].Substring((path[i].LastIndexOf(@"\") + 1)));
            //Debug.Log(".CSV: " + path[i].LastIndexOf(".csv"));
            //Debug.Log(filePath[i]);


            var lines = File.ReadAllLines(path[i]);
            var rows = (from line in lines
                        select line.ToArray() // get all columns
                        ).Skip(1).ToArray(); // skipping the "sep=;" separator line & take all rows


            for (int x = 0; x < lines.Length; x++)
            {
                var cells = lines[x].Split(',');
                for (int y = 0; y < cells.Length; y++)
                {
                    //Debug.Log(cells[y]);
                    cellsData.Add(cells[y]);
                }
                
            }

            for (int x = 0; x < cellsData.Count; x++)
            {
                if (cellsData[x] == "Area Unity Cooridnate")
                {
                    var indexofArea = cellsData.IndexOf(cellsData[x]);
                    Debug.Log("Index of Area" + indexofArea);
                }
            }


            //Debug.Log("List Length========:" + cellsData.Count);
            cellsData.Clear();
            
        }
    }
}
