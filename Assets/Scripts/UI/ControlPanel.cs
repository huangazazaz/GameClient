using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : UIBase
{

    private Button Throw;

    private void Awake()
    {
        Bind(UIEvent.READY_THROW, UIEvent.SET_CAMERA, UIEvent.THROW_THE_DICE);
    }

    FightRoomDto fightRoomDto;
    PromptMsg promptMsg;
    int index;

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
                Throw.gameObject.SetActive(false);
                break;
            case UIEvent.SET_CAMERA:
                index = (int)message;
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
