using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPanel : UIBase
{
    private TMP_Text ready0;
    private TMP_Text ready1;
    private TMP_Text ready2;
    private TMP_Text ready3;
    private Button ready;
    private int ind;
    private MatchRoomDto matchRoom;

    protected virtual void Awake()
    {
        Bind(UIEvent.SET_CAMERA, UIEvent.SET_INFORMATION);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.SET_CAMERA:
                {
                    ind = (int)message;
                    Debug.Log(ind);
                    break;
                }
            case UIEvent.SET_INFORMATION:
                {
                    matchRoom = (MatchRoomDto)message;
                    //int readyInd = matchRoom;

                    for (int i = 1; i <= 4; i++)
                    {
                        if (matchRoom.ReadySeat[i] != 1) continue;
                        switch (i)
                        {
                            case 0:
                                ready0.gameObject.SetActive(true);
                                ready.gameObject.SetActive(false);
                                break;
                            case 1:
                                ready1.gameObject.SetActive(true);
                                break;
                            case 2:
                                ready2.gameObject.SetActive(true);
                                break;
                            case 3:
                                ready3.gameObject.SetActive(true);
                                break;

                        }
                    }
                    break;
                }
            default:
                break;
        }
    }


    void Start()
    {
        ready0 = transform.Find("ready0").GetComponent<TMP_Text>();
        ready1 = transform.Find("ready1").GetComponent<TMP_Text>();
        ready2 = transform.Find("ready2").GetComponent<TMP_Text>();
        ready3 = transform.Find("ready3").GetComponent<TMP_Text>();
        ready = transform.Find("ready").GetComponent<Button>();


        ready0.gameObject.SetActive(false);
        ready1.gameObject.SetActive(false);
        ready2.gameObject.SetActive(false);
        ready3.gameObject.SetActive(false);
        ready.gameObject.SetActive(true);

        ready.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        Dispatch(AreaCode.NET, 0, new SocketMsg(OpCode.MATCH, MatchCode.READY_CREQ, null));
    }

    // Update is called once per frame

}
