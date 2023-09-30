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
        Bind(UIEvent.READY_THROW, UIEvent.SET_CAMERA, UIEvent.THROW_THE_DICE, UIEvent.INITIAL_DRAW_ONE);
    }

    FightRoomDto fightRoomDto;
    PromptMsg promptMsg;
    int index;
    SocketMsg socketMsg;

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
            case UIEvent.INITIAL_DRAW_ONE:
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

}
