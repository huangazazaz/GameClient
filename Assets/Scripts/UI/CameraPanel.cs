using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protocol.Dto;
using Protocol.Dto.Fight;
using TMPro;

public class CameraPanel : UIBase
{
    //fix bug
    //private void Awake()
    protected virtual void Awake()
    {
        Bind(UIEvent.SET_CAMERA);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SET_CAMERA:
                {
                    //setPanelActive(true);
                    switch ((int)message)
                    {
                        case 0:
                            c1.SetActive(true);
                            break;
                        case 1:
                            c2.SetActive(true);
                            break;
                        case 2:
                            c3.SetActive(true);
                            break;
                        case 3:
                            c4.SetActive(true);
                            break;
                    }
                    break;
                }
            default:
                break;
        }
    }

    //private Button b = null;
    private GameObject c1 = null;
    private GameObject c2 = null;
    private GameObject c3 = null;
    private GameObject c4 = null;

    protected virtual void Start()
    {
        //b = transform.Find("Button").GetComponent<Button>();
        c1 = GameObject.Find("Camera1");
        c2 = GameObject.Find("Camera2");
        c3 = GameObject.Find("Camera3");
        c4 = GameObject.Find("Camera4");
        c1.SetActive(false);
        c2.SetActive(false);
        c3.SetActive(false);
        c4.SetActive(false);
    }
}
