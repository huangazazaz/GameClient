using AhpilyServer;
using Protocol.Code;
using Protocol.Constant;
using Protocol.Dto;
using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : HandlerBase
{
    SocketMsg socketMsg = new SocketMsg();
    public override void OnReceive(int subCode, object value)
    {
        switch (subCode)
        {
            case FightCode.SHUFFLE or FightCode.DISPLAY_PLAY:
                refreshMahjongPosition(value as FightRoomDto);
                break;
            case FightCode.LEAVE_BRO:
                playerLeave((int)value);
                break;
            case FightCode.READY_THROW:
                Dispatch(AreaCode.UI, UIEvent.READY_THROW, value as FightRoomDto);
                break;
            case FightCode.THROW_THE_DICE:
                Dispatch(AreaCode.UI, UIEvent.THROW_THE_DICE, value as FightRoomDto);
                break;
            case FightCode.INITIAL_DRAW:
                Dispatch(AreaCode.UI, UIEvent.INITIAL_DRAW, value as FightRoomDto);
                break;
            case FightCode.INITIAL_DRAW_FINISH:
                Dispatch(AreaCode.UI, UIEvent.INITIAL_DRAW_FINISH, value as FightRoomDto);
                socketMsg.Change(OpCode.FIGHT, FightCode.DRAW, value as FightRoomDto);
                Dispatch(AreaCode.NET, 0, socketMsg);
                break;
            case FightCode.DRAW:
                Dispatch(AreaCode.UI, UIEvent.DRAW, value as FightRoomDto);
                break;
            case FightCode.CHECK_WAFFLE_PLAY:
                Dispatch(AreaCode.UI, UIEvent.CHECK_WAFFLE_PLAY, value as FightRoomDto);
                break;
            case FightCode.CHECK_WAFFLE_DRAW:
                Dispatch(AreaCode.UI, UIEvent.CHECK_WAFFLE_DRAW, value as FightRoomDto);
                break;
            case FightCode.WAFFLE_PLAY:
                Dispatch(AreaCode.UI, UIEvent.WAFFLE_PLAY, value as FightRoomDto);
                break;
            default:
                break;
        }
    }

    private void refreshMahjongPosition(FightRoomDto dto)
    {
        Dispatch(AreaCode.UI, UIEvent.REFRESH_MAHJONG_POSITION, dto);
    }
    private void playerLeave(int index)
    {
        PromptMsg prompt = new PromptMsg();
        prompt.Change(index.ToString() + "号位玩家离开对局", UnityEngine.Color.blue);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, prompt);
    }

    /// <summary>
    /// 同步出牌
    /// </summary>
    /// <param name="dto"></param>
    private void dealBro(DealDto dto)
    {

        Dispatch(AreaCode.CHARACTER, CharacterEvent.UPDATE_SHOW_DESK, dto.SelectCardList);
        //播放出牌音效
        playDealAudio(dto.Type, dto.Weight);
    }

    /// <summary>
    /// 播放出牌音效
    /// </summary>
    private void playDealAudio(int cardType, int weight)
    {
        string audioName = "Fight/";

        //switch (cardType)
        //{
        //    case CardType.SINGLE:
        //        audioName += "Woman_" + weight;
        //        break;
        //    case CardType.DOUBLE:
        //        audioName += "Woman_dui" + weight / 2;
        //        break;
        //    case CardType.STRAIGHT:
        //        audioName += "Woman_shunzi";
        //        break;
        //    case CardType.DOUBLE_STRAIGHT:
        //        audioName += "Woman_liandui";
        //        break;
        //    case CardType.TRIPLE_STRAIGHT:
        //        audioName += "Woman_feiji";
        //        break;
        //    case CardType.THREE:
        //        audioName += "Woman_tuple" + weight / 3;
        //        break;
        //    case CardType.THREE_ONE:
        //        audioName += "Woman_sandaiyi";
        //        break;
        //    case CardType.THREE_TWO:
        //        audioName += "Woman_sandaiyidui";
        //        break;
        //    case CardType.BOOM:
        //        audioName += "Woman_zhadan";
        //        break;
        //    case CardType.JOKER_BOOM:
        //        audioName += "Woman_wangzha";
        //        break;
        //    default:
        //        break;
        //}

        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_EFFECT_AUDIO, audioName);
    }



}
