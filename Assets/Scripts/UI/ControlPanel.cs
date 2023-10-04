using AhpilyServer;
using Protocol.Code;
using Protocol.Constant;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : UIBase
{

    private Button Throw;

    private void Awake()
    {
        Bind(UIEvent.WAFFLE_PLAY, UIEvent.CHECK_WAFFLE_PLAY, UIEvent.CHECK_WAFFLE_DRAW, UIEvent.REFRESH_MAHJONG_POSITION, UIEvent.INITIAL_DRAW, UIEvent.INITIAL_DRAW_FINISH, UIEvent.READY_THROW, UIEvent.SET_CAMERA, UIEvent.THROW_THE_DICE, UIEvent.INITIAL_DRAW_ONE, UIEvent.DRAW);
    }

    FightRoomDto fightRoomDto;
    PromptMsg promptMsg;
    int index;
    SocketMsg socketMsg;
    Dictionary<int, Button> MahjongBtns;
    int style = 1;
    Dictionary<int, string> HandMahjongs;
    Dictionary<int, Button> waffleBtns;

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
            case UIEvent.CHECK_WAFFLE_PLAY:
                checkWafflePlay(message as FightRoomDto);
                break;
            case UIEvent.CHECK_WAFFLE_DRAW:
                checkWaffleDraw(message as FightRoomDto);
                break;
            case UIEvent.SET_CAMERA:
                index = (int)message;
                break;
            case UIEvent.DRAW or UIEvent.REFRESH_MAHJONG_POSITION or UIEvent.INITIAL_DRAW or UIEvent.INITIAL_DRAW_FINISH:
                refresh(message as FightRoomDto);
                fightRoomDto = message as FightRoomDto;
                break;
            case UIEvent.WAFFLE_PLAY:
                refresh(message as FightRoomDto, 1);
                fightRoomDto = message as FightRoomDto;
                break;
            case UIEvent.INITIAL_DRAW_ONE:
                refresh(message as FightRoomDto);
                fightRoomDto = message as FightRoomDto;
                if (index == fightRoomDto.drawIndex[1])
                {
                    Thread.Sleep(100);
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
        waffleBtns = new Dictionary<int, Button>();
        HandMahjongs = new Dictionary<int, string>();
        waffleBtns.Add(1, transform.Find("chi").GetComponent<Button>());
        waffleBtns.Add(2, transform.Find("peng").GetComponent<Button>());
        waffleBtns.Add(3, transform.Find("gang").GetComponent<Button>());
        waffleBtns.Add(4, transform.Find("gang").GetComponent<Button>());
        waffleBtns.Add(5, transform.Find("hu").GetComponent<Button>());
        waffleBtns.Add(6, transform.Find("hu_self").GetComponent<Button>());
        waffleBtns.Add(7, transform.Find("pass").GetComponent<Button>());
        waffleBtns[1].onClick.AddListener(chooseChi);
        waffleBtns[2].onClick.AddListener(choosePeng);
        waffleBtns[3].onClick.AddListener(chooseGang);
        waffleBtns[5].onClick.AddListener(chooseHu);
        waffleBtns[6].onClick.AddListener(chooseHuS);
        waffleBtns[7].onClick.AddListener(choosePass);
        waffleBtnInactive();
        for (int i = 1; i <= 14; i++)
        {
            HandMahjongs.Add(i, "");
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
    void chooseChi()
    {
        sendCheckInfo(1);
    }
    void choosePeng()
    {
        sendCheckInfo(2);
    }
    void chooseGang()
    {
        sendCheckInfo(3);
    }
    void chooseHu()
    {
        sendCheckInfo(5);
    }
    void chooseHuS()
    {
        sendCheckInfo(6);
    }
    void choosePass()
    {
        sendCheckInfo(0);
    }

    void sendCheckInfo(int waffle)
    {
        waffleBtnInactive();
        fightRoomDto.chooseWaffle[index] = waffle;
        fightRoomDto.WaffleChooseCnt += 1;
        socketMsg.Change(OpCode.FIGHT, FightCode.CHECK_WAFFLE_PLAY, fightRoomDto);
        Dispatch(AreaCode.NET, 0, socketMsg);
    }

    void checkWafflePlay(FightRoomDto dto)
    {
        fightRoomDto = dto;
        if (dto.wafflesPlay[index].Count > 0)
        {
            for (int i = 1; i <= 6; i++)
            {
                if (dto.wafflesPlay[index].Keys.Contains(i))
                {
                    waffleBtns[i].gameObject.SetActive(true);
                }
            }
            waffleBtns[7].gameObject.SetActive(true);
            int cnt = 1;
            for (int i = 6; i >= 1; i--)
            {
                if (waffleBtns[i].gameObject.activeSelf)
                {
                    waffleBtns[i].transform.position = waffleBtns[7].transform.position - new Vector3(160 * cnt, 0, 0);
                    cnt++;
                }
            }
        }
        else
        {
            sendCheckInfo(0);
        }
    }
    void checkWaffleDraw(FightRoomDto dto)
    {
        fightRoomDto = dto;
        if (dto.wafflesDraw.Count > 0)
        {
            for (int i = 1; i <= 6; i++)
            {
                if (dto.wafflesDraw.Contains(i))
                {
                    waffleBtns[7].gameObject.SetActive(true);
                    waffleBtns[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            //TODO 自己摸牌waffle
        }
    }


    bool[] checks;

    void waffleBtnInactive()
    {
        for (int i = 1; i <= 7; i++)
        {
            waffleBtns[i].gameObject.SetActive(false);
        }
    }
    void stateChange(int ind)
    {
        for (int i = 1; i <= 14; i++)
        {
            if (checks[i - 1])
            {
                checks[i - 1] = false;
                stateInactive(MahjongBtns[i].gameObject);
                if (i == ind)
                {
                    fightRoomDto.playMahjong = HandMahjongs[ind];
                    socketMsg.Change(OpCode.FIGHT, FightCode.PLAY, fightRoomDto);
                    Dispatch(AreaCode.NET, 0, socketMsg);
                    return;
                }
                else
                {
                    checks[ind - 1] = true;
                    stateActive(MahjongBtns[ind].gameObject);
                    return;
                }
            }
        }
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

    void refresh(FightRoomDto dto, int op = 0)
    {
        for (int i = 1; i <= 13; i++)
        {
            MahjongBtns[i].gameObject.SetActive(false);
        }
        for (int j = dto.SeatHandMahjongs[index].Count; j >= 1; j--)
        {
            string name = dto.SeatHandMahjongs[index][j - 1];
            HandMahjongs[13 + j - dto.SeatHandMahjongs[index].Count] = name;
            name = name.Substring(0, name.Length - 1);
            MahjongBtns[13 + j - dto.SeatHandMahjongs[index].Count].gameObject.SetActive(true);
            MahjongBtns[13 + j - dto.SeatHandMahjongs[index].Count].image.sprite = Resources.Load<Sprite>("UI/Mahjong/" + style.ToString() + "/" + name);
        }

        if (dto.drawMahjong[index] == "")
        {
            MahjongBtns[14].gameObject.SetActive(false);
            if (op == 1 && index == dto.CurrentInd)
            {
                int pos = dto.SeatHandMahjongs[index].Count;
                checks[pos] = true;
                stateActive(MahjongBtns[pos].gameObject);
                MahjongBtns[pos].gameObject.gameObject.SetActive(true);
                MahjongBtns[pos].image.sprite = Resources.Load<Sprite>("UI/Mahjong/" + style.ToString() + "/" + dto.SeatHandMahjongs[index][pos].Substring(0, dto.SeatHandMahjongs[index][pos].Length - 1));
            }
        }
        else
        {
            HandMahjongs[14] = dto.drawMahjong[index];
            if (!checks[13])
            {
                checks[13] = true;
                stateActive(MahjongBtns[14].gameObject);
            }
            MahjongBtns[14].gameObject.gameObject.SetActive(true);
            MahjongBtns[14].image.sprite = Resources.Load<Sprite>("UI/Mahjong/" + style.ToString() + "/" + dto.drawMahjong[index].Substring(0, dto.drawMahjong[index].Length - 1));
        }
    }
}
