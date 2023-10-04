using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 存储所有的UI事件码
/// </summary>
public class UIEvent
{
    public const int START_PANEL_ACTIVE = 0;//设置开始面板的显示
    public const int REGIST_PANEL_ACTIVE = 1;//设置注册面板的显示

    public const int REFRESH_INFO_PANEL = 2;//刷新信息面板
    public const int SHOW_ENTER_ROOM_BUTTON = 3;//显示进入房间按钮
    public const int CREATE_PANEL_ACTIVE = 4;//设置创建面板的显示
    public const int REGISTER_SUCCESS_R = 44;//注册成功，清空注册页面
    public const int REGISTER_SUCCESS_L = 444;//注册成功，打开登录页面填充账号

    public const int REFRESH_MAHJONG_POSITION = 5;//刷新麻将位置

    public const int SET_CAMERA = 19;//根据座位设置摄像头
    public const int SET_INFORMATION = 20;//更新信息
    public const int HIND_READY = 21;//隐藏准备信息
    public const int READY_THROW = 22;//准备掷骰子
    public const int THROW_THE_DICE = 23;//掷骰子
    public const int INITIAL_DRAW = 24;//开局摸牌
    public const int INITIAL_DRAW_ONE = 25;//开局摸完一张牌
    public const int INITIAL_DRAW_FINISH = 26;//开局摸牌结束
    public const int DRAW = 27;//局内摸牌
    //public const int PLAY = 28;//局内出牌
    public const int CHECK_WAFFLE_PLAY = 29;//判断出牌吃碰杠胡
    public const int CHECK_WAFFLE_DRAW = 30;//判断摸牌杠胡
    //public const int WAFFLE_DRAW = 31;//吃碰摸牌
    public const int WAFFLE_PLAY = 32;//杠打牌


    //....

    public const int PROMPT_MSG = int.MaxValue;
}
