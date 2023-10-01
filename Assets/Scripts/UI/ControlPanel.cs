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
            MahjongBtns[i].onClick.AddListener(ChooseClick);
            MahjongBtns[i].gameObject.SetActive(false);
        }
    }
    void ChooseClick()
    {
        this.transform.position = this.transform.position + new Vector3(0, (float)20, 0);
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
            MahjongBtns[14].image.sprite = Resources.Load<Sprite>("UI/Mahjong/" + style.ToString() + "/" + dto.drawMahjong[index].Substring(0, dto.drawMahjong[index].Length));
        }
    }
}
