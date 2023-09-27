using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{

    private GameObject[] gameObjects = null;

    UnityEngine.Vector3 position = new UnityEngine.Vector3();

    GameObject table = null;

    TMP_InputField field = null;
    Button x1 = null;
    Button y1 = null;
    Button x2 = null;
    Button y2 = null;



    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new GameObject[108];
        string mjname = "MaJiang (";
        for (int i = 0; i < gameObjects.Length; i++)
        {
            string mname = mjname + i + ")";
            gameObjects[i] = GameObject.Find(mname);
        }
        table = GameObject.Find("Plane");
        field = transform.Find("ind").GetComponent<TMP_InputField>();
        x1 = transform.Find("x1").GetComponent<Button>();
        x2 = transform.Find("x2").GetComponent<Button>();
        y1 = transform.Find("y1").GetComponent<Button>();
        y2 = transform.Find("y2").GetComponent<Button>();

        x1.onClick.AddListener(x1click);
        x2.onClick.AddListener(x2click);
        y1.onClick.AddListener(y1click);
        y2.onClick.AddListener(y2click);
    }

    void x1click()
    {
        UnityEngine.Vector3 ve = new UnityEngine.Vector3(5, 0, 0);
        change(ve);
    }
    void x2click()
    {
        UnityEngine.Vector3 ve = new UnityEngine.Vector3(-5, 0, 0);
        change(ve);
    }

    void y1click()
    {
        UnityEngine.Vector3 ve = new UnityEngine.Vector3(0, 0, 5);
        change(ve);
    }
    void y2click()
    {
        UnityEngine.Vector3 ve = new UnityEngine.Vector3(0, 0, -5);
        change(ve);
    }

    void change(UnityEngine.Vector3 change)
    {
        position = table.transform.position;
        int ind = int.Parse(field.text);
        GameObject mj = gameObjects[ind - 1];
        mj.transform.position = mj.transform.position + change;
    }

}
