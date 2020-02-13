using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expected_Results_Controller : MonoBehaviour
{
    private GameObject shown, hidden;
    // Start is called before the first frame update
    void Start()
    {
        shown = transform.Find("Shown").gameObject;
        hidden = transform.Find("Hidden").gameObject;

        shown.SetActive(true);
        hidden.SetActive(false);
    }

    public void OnClick()
    {
        shown.SetActive(hidden.activeSelf);
        hidden.SetActive(!shown.activeSelf);
    }
}
