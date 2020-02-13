using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using System.Linq;

public class FrameworkController : MonoBehaviour
{
    // Remove the warning that tells you this value is not assigned. We're assigning these values via the Inspector
#pragma warning disable 0649
    [SerializeField]
    GameObject PanelPrefab;
    [SerializeField]
    List<TestController> Tests;
    [SerializeField]
    RectTransform ScrollviewContent;
    [SerializeField]
    GameObject finishDialogue;
    [SerializeField]
    Text finishDialogueText;
    
    private int currentIndex = 0;
#pragma warning restore 0414
    // Variables for Toggling sidebar on and off
    private GameObject Sidebar;
    private GameObject HideButton;

    // Use this for initialization
    void Start()
    {
        Sidebar = transform.Find("Sidebar").gameObject;
        HideButton = transform.Find("HideButton").gameObject;
        Tests = new List<TestController>();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        int count = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < count; i++)
        {
            int x = i;
            string pathToScene = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
            if (!sceneName.Contains("Menu") && !sceneName.Contains("Summary"))
            {
                GameObject go = (GameObject)Instantiate(PanelPrefab, ScrollviewContent);
                go.GetComponentInChildren<Text>().text = (i-1) + "." + sceneName;
                go.GetComponent<Button>().onClick.AddListener(() => LoadSceneAt(x));
                if (sceneName == SceneManager.GetActiveScene().name)
                {
                    go.GetComponent<Image>().color = new Color(40f / 255, 40f / 255, 40f / 255, 100f / 255);
                }
                TestController temp = (TestController)ScriptableObject.CreateInstance(typeof(TestController));
                temp.name = sceneName + "_object";
                temp.sceneName = sceneName;
                temp.UIObject = go;
                temp.status = 0;
                temp.selected = false;
                Tests.Add(temp);
                //Destroy(temp);
            }
        }
    }
    
    void Update()
    {
    }

    public void LoadSceneAt(int index)
    {                                                           
        currentIndex = index - 2;                           // Since we're getting the index in "Build settings" scope, 
        SceneManager.LoadScene(index, LoadSceneMode.Single);// Just remove 2 to convert it to "Tests" scope which just doesn't have "MainMenu" and "Summmary" scenes in it
    }

    private void UpdateChosenTest(string sceneName)
    {
        for (int i = 0; i < Tests.Count; i++)
        {
            if (Tests[i].sceneName == sceneName)
            {
                Tests[i].SelectThis();
                currentIndex = i;
            }
            else
            {
                Tests[i].DeselectThis();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateChosenTest(scene.name);
    }

    public void PassTest()
    {
        for (int i = 0; i < Tests.Count; i++)
        {
            if (Tests[i].selected)
                Tests[i].PassThis();
        }
        NextTest();
    }

    public void FailTest()
    {
        for (int i = 0; i < Tests.Count; i++)
        {
            if (Tests[i].selected)
                Tests[i].FailThis();
        }
        NextTest();
    }

    bool CheckIfEnd()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            FinishYes();
            return true;
        }

        return false;
    }

    public void NextTest()
    {
        if (CheckIfEnd())
            return;
        else
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
                LoadSceneAt(SceneManager.GetActiveScene().buildIndex + 1);
            else LoadSceneAt(2); // 0 is the Main Menu, 1 is Summary, we loop back to 2 - the first test
        }
    }

    public void PreviousTest()
    {
        if (SceneManager.GetActiveScene().buildIndex - 2 > 0)
        {
            LoadSceneAt(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else LoadSceneAt(SceneManager.sceneCountInBuildSettings - 1); // We loop back to the end
    }
    public void FinishClick()
    {
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            source.Stop();
        }
        int unfinishedCount = 0;
        foreach (TestController test in Tests)
        {
            if (test.status == 0)
                unfinishedCount++;
        }

        if (unfinishedCount == 0)
            FinishYes();
        else
        {
            if (unfinishedCount > 1)
                finishDialogueText.text = "You have " + unfinishedCount + " unfinished tests. \n Do you still want to finish?";
            else finishDialogueText.text = "You have 1 unfinished test. \n Do you still want to finish?";
            ShowFinishDialogue();
        }
    }

    public void FinishYes()
    {
        StartCoroutine(TestSummary());
    }

    public void FinishNo()
    {
        finishDialogue.SetActive(false);
    }

    void ShowFinishDialogue()
    {
        finishDialogue.SetActive(true);
    }

    IEnumerator TestSummary ()
    {
        SceneManager.LoadScene("Summary", LoadSceneMode.Single);
        yield return new WaitForSeconds(0.1f);  //Wait till scene finishes loading
        SummaryController sc = FindObjectOfType<SummaryController>();
        if (sc != null)
        {
            sc.PopulateScrollView(Tests);
            finishDialogue.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Summary Controller script not found");
        }
    }

    public void ToggleSidebar()
    {
        RectTransform rtSidebar = Sidebar.GetComponent<RectTransform>();
        RectTransform rtButton = HideButton.GetComponent<RectTransform>();
        Canvas canvas = GetComponent<Canvas>();
        float positionX = 0;
        Sidebar.SetActive(!Sidebar.activeSelf);
        if (Sidebar.activeSelf)
        {
            positionX = rtSidebar.position.x - (rtSidebar.rect.width * canvas.scaleFactor) / 2 -
                              (rtButton.rect.width * canvas.scaleFactor) / 2;
        }
        else
        {
            positionX = Screen.width - (rtButton.rect.width * canvas.scaleFactor) / 2;
        }
        HideButton.transform.position = new Vector3(positionX, rtButton.position.y, rtButton.position.z);
    }
}
