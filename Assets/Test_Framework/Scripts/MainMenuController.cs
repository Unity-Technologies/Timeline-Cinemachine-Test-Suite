using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject Framework_UI;

    public void BeginTests()
    {
        Framework_UI.SetActive(true);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

}
