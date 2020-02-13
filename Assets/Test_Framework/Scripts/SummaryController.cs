using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SummaryController : MonoBehaviour {

    // Remove the warning that tells you this value is not assigned. We're assigning these values via the Inspector
#pragma warning disable 0649
    [SerializeField]
    GameObject summaryItemObject;
    [SerializeField]
    RectTransform ScrollviewContent;
    FrameworkController fc;
    [SerializeField]
    GameObject ExportDialogue;

    List<TestController> testList;

    [SerializeField]
    Text ExportStatus;
#pragma warning restore 0649
    // Use this for initialization
	void Start () {
        fc = FindObjectOfType<FrameworkController>();
	}

    public void PopulateScrollView(List<TestController> list)
    {
        testList = list;
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = (GameObject)Instantiate(summaryItemObject, ScrollviewContent);
            //Test Number
            go.transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);
            //Test Name
            go.transform.GetChild(1).GetComponent<Text>().text = list[i].sceneName;
            //Test Result
            if (list[i].status == 1)
            {
                go.transform.GetChild(2).GetComponent<Image>().color = Color.green;
                go.transform.GetChild(2).GetComponentInChildren<Text>().text = "PASSED";
            }
            else
            if (list[i].status == 2)
            {
                go.transform.GetChild(2).GetComponent<Image>().color = Color.red;
                go.transform.GetChild(2).GetComponentInChildren<Text>().text = "FAILED";
            }

        }
    }

    public void BackClick()
    {
        fc.gameObject.SetActive(true);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void RestartClick()
    {
        Destroy(fc.gameObject);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void ExportClick()
    {
        ExportDialogue.SetActive(true);
        string results = "";
        int i = 1;
        foreach (TestController test in testList)
        {
            string status;
            switch (test.status)
            {
                case 0:
                    status = "UNFINISHED";
                    break;
                case 1:
                    status = "PASSED";
                    break;
                case 2:
                    status = "FAILED";
                    break;
                default:
                    status = "ERROR";
                    break;
            }
            results += i + "," + test.sceneName + "," + status + ",\n";
            i++;
        }
        Debug.Log(results);
        string path = Application.persistentDataPath + "/Results.csv";
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(results);
        writer.Close();
        ExportStatus.text = "Results output to log. \n Results can also be found at " + path;
    }

    public void ExportOkayClick()
    {
        ExportDialogue.SetActive(false);
    }
}
