using AhpilyServer;
using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : UIBase
{

    private Button Throw;

    private void Awake()
    {
        Bind(UIEvent.REFRESH_MAHJONG_POSITION, UIEvent.INITIAL_DRAW, UIEvent.INITIAL_DRAW_FINISH, UIEvent.READY_THROW, UIEvent.SET_CAMERA, UIEvent.THROW_THE_DICE, UIEvent.INITIAL_DRAW_ONE, UIEvent.DRAW);
    }

    FightRoomDto fightRoomDto;
    PromptMsg promptMsg;
    int index;
    SocketMsg socketMsg;
    Dictionary<int, Button> MahjongBtns;
    int style = 1;


    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.READY_THROW:
                fightRoomDto = message as FightRoomDto;
                if (index == fightRoomDto.dealer) Throw.gameObject.SetActive(true);
                promptMsg.Change(index == fightRoomDto.dealer ? "请投掷骰子" : "请等待庄家投掷骰子", Color.blue);
                Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
                break;
            case UIEvent.THROW_THE_DICE:
                fightRoomDto = message as FightRoomDto;
                break;
            case UIEvent.SET_CAMERA:
                index = (int)message;
                break;
            case UIEvent.DRAW:
                refresh(message as FightRoomDto);
                //TODO
                break;
            case UIEvent.REFRESH_MAHJONG_POSITION or UIEvent.INITIAL_DRAW or UIEvent.INITIAL_DRAW_FINISH:
                refresh(message as FightRoomDto);
                break;
            case UIEvent.INITIAL_DRAW_ONE:
                refresh(message as FightRoomDto);
                fightRoomDto = message as FightRoomDto;
                if (index == fightRoomDto.drawIndex[1])
                {
                    Thread.Sleep(500);
                    socketMsg.Change(OpCode.FIGHT, FightCode.INITIAL_DRAW, message as FightRoomDto);
                    Dispatch(AreaCode.NET, 0, socketMsg);
                }
                break;

            default:
                break;
        }
    }

    void Start()
    {
        promptMsg = new PromptMsg();
        Throw = transform.Find("throw").GetComponent<Button>();
        Throw.gameObject.SetActive(false);
        Throw.onClick.AddListener(ThrowClick);
        socketMsg = new SocketMsg();
        MahjongBtns = new Dictionary<int, Button>();
        for (int i = 1; i <= 14; i++)
        {
            MahjongBtns.Add(i, transform.Find("Mahjong" + i).GetComponent<Button>());
        }
        MahjongBtns[1].onClick.AddListener(ChooseClick1);
        MahjongBtns[2].onClick.AddListener(ChooseClick2);
        MahjongBtns[3].onClick.AddListener(ChooseClick3);
        MahjongBtns[4].onClick.AddListener(ChooseClick4);
        MahjongBtns[5].onClick.AddListener(ChooseClick5);
        MahjongBtns[6].onClick.AddListener(ChooseClick6);
        MahjongBtns[7].onClick.AddListener(ChooseClick7);
        MahjongBtns[8].onClick.AddListener(ChooseClick8);
        MahjongBtns[9].onClick.AddListener(ChooseClick9);
        MahjongBtns[10].onClick.AddListener(ChooseClick10);
        MahjongBtns[11].onClick.AddListener(ChooseClick11);
        MahjongBtns[12].onClick.AddListener(ChooseClick12);
        MahjongBtns[13].onClick.AddListener(ChooseClick13);
        MahjongBtns[14].onClick.AddListener(ChooseClick14);
        for (int i = 1; i <= 14; i++)
        {
            MahjongBtns[i].gameObject.SetActive(false);
        }
        checks = new bool[14];
        for (int i = 0; i < 14; i++)
        {
            checks[i] = false;
        }
    }

    bool[] checks;


    void stateChange(int ind)
    {
        for (int i = 1; i <= 14; i++)
        {
            if (checks[i - 1])
            {
                if (i == ind)
                {
                    checks[i - 1] = false;
                    stateInactive(MahjongBtns[i].gameObject);
                    return;
                    //TODO 出牌
                }
                else
                {
                    checks[i - 1] = false;
                    checks[ind - 1] = true;
                    stateActive(MahjongBtns[ind].gameObject);
                    stateInactive(MahjongBtns[i].gameObject);
                    return;
                }
            }
        }
        checks[ind - 1] = true;
        stateActive(MahjongBtns[ind].gameObject);
    }

    void ChooseClick1()
    {
        stateChange(1);
    }

    void ChooseClick2()
    {
        stateChange(2);
    }
    void ChooseClick3()
    {
        stateChange(3);
    }

    void ChooseClick4()
    {
        stateChange(4);
    }
    void ChooseClick5()
    {
        stateChange(5);
    }

    void ChooseClick6()
    {
        stateChange(6);
    }
    void ChooseClick7()
    {
        stateChange(7);
    }

    void ChooseClick8()
    {
        stateChange(8);
    }
    void ChooseClick9()
    {
        stateChange(9);
    }

    void ChooseClick10()
    {
        stateChange(10);
    }
    void ChooseClick11()
    {
        stateChange(11);
    }

    void ChooseClick12()
    {
        stateChange(12);
    }
    void ChooseClick13()
    {
        stateChange(13);
    }

    void ChooseClick14()
    {
        stateChange(14);
    }

    void stateActive(GameObject obj)
    {
        obj.transform.position = obj.transform.position + new Vector3(0, 20, 0);
    }
    void stateInactive(GameObject obj)
    {
        obj.transform.position = obj.transform.position + new Vector3(0, -20, 0);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        Throw.onClick.RemoveListener(ThrowClick);
    }

    private void ThrowClick()
    {
        SocketMsg socketMsg = new SocketMsg();
        socketMsg.Change(OpCode.FIGHT, FightCode.READY_THROW, fightRoomDto.SeatUId);
        Dispatch(AreaCode.NET, 0, socketMsg);
        Throw.gameObject.SetActive(false);
    }

    void refresh(FightRoomDto dto)
    {
        for (int i = 1; i <= 13; i++)
        {
            MahjongBtns[i].gameObject.SetActive(false);
        }
        for (int j = 1; j <= dto.SeatHandMahjongs[index].Count; j++)
        {
            string name = dto.SeatHandMahjongs[index][j - 1];
            name = name.Substring(0, name.Length - 1);
            MahjongBtns[j].gameObject.SetActive(true);
            MahjongBtns[j].image.sprite = Resources.Load<Sprite>("UI/Mahjong/" + style.ToString() + "/" + name);
        }

        if (dto.drawMahjong[index] == "")
        {
            MahjongBtns[14].gameObject.SetActive(false);
        }
        else
        {
            MahjongBtns[14].gameObject.gameObject.SetActive(true);
            MahjongBtns[14].image.sprite = Resources.Load<Sprite>("UI/Mahjong/" + style.ToString() + "/" + dto.drawMahjong[index].Substring(0, dto.drawMahjong[index].Length - 1));
        }
    }
}
