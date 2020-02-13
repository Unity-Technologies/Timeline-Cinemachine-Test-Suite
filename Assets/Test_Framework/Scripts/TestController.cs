using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : ScriptableObject
{

    public GameObject UIObject;
    public int status;
    public string sceneName;
    public bool selected;

    private float r, g, b, a;

    public void SelectThis()
    {
        if (UIObject != null && !selected)
        {
            selected = true;
            r = UIObject.GetComponent<Image>().color.r;
            g = UIObject.GetComponent<Image>().color.g;
            b = UIObject.GetComponent<Image>().color.b;
            a = UIObject.GetComponent<Image>().color.a;

            UIObject.GetComponent<Image>().color = new Color(r - 100f / 255, g - 100f / 255, b - 100f / 255, a);
        }
    }

    public void DeselectThis()
    {
        if (UIObject != null && selected)
        {
            selected = false;
            r = UIObject.GetComponent<Image>().color.r;
            g = UIObject.GetComponent<Image>().color.g;
            b = UIObject.GetComponent<Image>().color.b;
            a = UIObject.GetComponent<Image>().color.a;

            UIObject.GetComponent<Image>().color = new Color(r + 100f / 255, g + 100f / 255, b + 100f / 255, a);
        }
    }

    public void PassThis()
    {
        if (UIObject != null)
        {
            status = 1;
            UIObject.GetComponent<Image>().color = new Color(0, 0.6f, 0, a);
        }
        if (selected)
            SelectThis();
    }

    public void FailThis()
    {
        if (UIObject != null)
        {
            status = 2;
            UIObject.GetComponent<Image>().color = new Color(0.6f, 0, 0, a);
        }
        if (selected)
            SelectThis();
    }
}