using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayHandling : MonoBehaviour
{
    string[] dates = { "2020-05-01", "2020-05-02", "2020-05-03", "2020-05-01", "2020-05-01", "2020-05-03" };

    private void Start()
    {
        for (int i = 0; i < dates.Length; i++)
        {
            bool isDuplicate = false;
            for (int j = 0; j < i; j++)
            {
                if (dates[i] == dates[j])
                {
                    isDuplicate = true;
                    break;
                }
            }
            if (!isDuplicate)
            {
                Debug.Log(dates[i]);
            }
        }
    }
}
