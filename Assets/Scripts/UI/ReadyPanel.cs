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
    private TMP_Text name0;
    private TMP_Text name1;
    private TMP_Text name2;
    private TMP_Text name3;
    private TMP_Text token0;
    private TMP_Text token1;
    private TMP_Text token2;
    private TMP_Text token3;
    private Button ready;
    private int ind;
    private MatchRoomDto matchRoom;

    protected virtual void Awake()
    {
        Bind(UIEvent.SET_CAMERA, UIEvent.SET_INFORMATION, UIEvent.HIND_INFO);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.HIND_INFO:
                {
                    ready0.gameObject.SetActive(false);
                    ready1.gameObject.SetActive(false);
                    ready2.gameObject.SetActive(false);
                    ready3.gameObject.SetActive(false);
                    break;
                }
            case UIEvent.SET_CAMERA:
                {
                    ind = (int)message;
                    //Debug.Log(ind);
                    break;
                }
            case UIEvent.SET_INFORMATION:
                {
                    matchRoom = (MatchRoomDto)message;
                    //int readyInd = matchRoom;

                    for (int i = 1; i <= 4; i++)
                    {
                        if (matchRoom.ReadySeat[i] == -1)
                        {
                            switch ((i - ind + 4) % 4)
                            {
                                case 0:
                                    name0.gameObject.SetActive(false);
                                    token0.gameObject.SetActive(false);
                                    ready0.gameObject.SetActive(false);
                                    ready.gameObject.SetActive(false);
                                    break;
                                case 1:
                                    name1.gameObject.SetActive(false);
                                    token1.gameObject.SetActive(false);
                                    ready1.gameObject.SetActive(false);
                                    break;
                                case 2:
                                    name2.gameObject.SetActive(false);
                                    token2.gameObject.SetActive(false);
                                    ready2.gameObject.SetActive(false);
                                    break;
                                case 3:
                                    name3.gameObject.SetActive(false);
                                    token3.gameObject.SetActive(false);
                                    ready3.gameObject.SetActive(false);
                                    break;

                            }

                        }
                        else if (matchRoom.ReadySeat[i] == 1)
                        {
                            switch ((i - ind + 4) % 4)
                            {
                                case 0:
                                    name0.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    token0.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    name0.gameObject.SetActive(true);
                                    token0.gameObject.SetActive(true);
                                    ready0.gameObject.SetActive(true);
                                    ready.gameObject.SetActive(false);
                                    break;
                                case 1:
                                    name1.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    token1.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    name1.gameObject.SetActive(true);
                                    token1.gameObject.SetActive(true);
                                    ready1.gameObject.SetActive(true);
                                    break;
                                case 2:
                                    name2.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    token2.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    name2.gameObject.SetActive(true);
                                    token2.gameObject.SetActive(true);
                                    ready2.gameObject.SetActive(true);
                                    break;
                                case 3:
                                    name3.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    token3.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    name3.gameObject.SetActive(true);
                                    token3.gameObject.SetActive(true);
                                    ready3.gameObject.SetActive(true);
                                    break;

                            }
                        }
                        else
                        {
                            switch ((i - ind + 4) % 4)
                            {
                                case 0:
                                    name0.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    name0.gameObject.SetActive(true);
                                    token0.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    token0.gameObject.SetActive(true);
                                    ready0.gameObject.SetActive(false);
                                    ready.gameObject.SetActive(true);
                                    break;
                                case 1:
                                    name1.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    name1.gameObject.SetActive(true);
                                    token1.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    token1.gameObject.SetActive(true);
                                    ready1.gameObject.SetActive(false);
                                    break;
                                case 2:
                                    name2.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    name2.gameObject.SetActive(true);
                                    token2.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    token2.gameObject.SetActive(true);
                                    ready2.gameObject.SetActive(false);
                                    break;
                                case 3:
                                    name3.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    name3.gameObject.SetActive(true);
                                    token3.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    token3.gameObject.SetActive(true);
                                    ready3.gameObject.SetActive(false);
                                    break;

                            }
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
        name0 = transform.Find("name0").GetComponent<TMP_Text>();
        name3 = transform.Find("name3").GetComponent<TMP_Text>();
        name2 = transform.Find("name2").GetComponent<TMP_Text>();
        name1 = transform.Find("name1").GetComponent<TMP_Text>();
        token0 = transform.Find("token0").GetComponent<TMP_Text>();
        token3 = transform.Find("token3").GetComponent<TMP_Text>();
        token2 = transform.Find("token2").GetComponent<TMP_Text>();
        token1 = transform.Find("token1").GetComponent<TMP_Text>();
        ready = transform.Find("ready").GetComponent<Button>();


        ready0.gameObject.SetActive(false);
        ready1.gameObject.SetActive(false);
        ready2.gameObject.SetActive(false);
        ready3.gameObject.SetActive(false);
        name3.gameObject.SetActive(false);
        name2.gameObject.SetActive(false);
        name1.gameObject.SetActive(false);
        name0.gameObject.SetActive(false);
        token3.gameObject.SetActive(false);
        token2.gameObject.SetActive(false);
        token1.gameObject.SetActive(false);
        token0.gameObject.SetActive(false);
        ready.gameObject.SetActive(true);

        ready.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        Dispatch(AreaCode.NET, 0, new SocketMsg(OpCode.MATCH, MatchCode.READY_CREQ, null));
    }

    // Update is called once per frame

}
