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
                        case 1:
                            c1.gameObject.SetActive(true);
                            break;
                        case 2:
                            c2.gameObject.SetActive(true);
                            break;
                        case 3:
                            c3.gameObject.SetActive(true);
                            break;
                        case 4:
                            c4.gameObject.SetActive(true);
                            break;
                    }
                    break;
                }
            default:
                break;
        }
    }

    //private Button b = null;
    private Camera c1 = null;
    private Camera c2 = null;
    private Camera c3 = null;
    private Camera c4 = null;

    protected virtual void Start()
    {
        //b = transform.Find("Button").GetComponent<Button>();
        c1 = transform.Find("Camera1").GetComponent<Camera>();
        c2 = transform.Find("Camera2").GetComponent<Camera>();
        c3 = transform.Find("Camera3").GetComponent<Camera>();
        c4 = transform.Find("Camera4").GetComponent<Camera>();
        c1.gameObject.SetActive(false);
        c2.gameObject.SetActive(false);
        c3.gameObject.SetActive(false);
        c4.gameObject.SetActive(false);
    }
}
