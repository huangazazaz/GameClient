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

    public const int REFRESH_INFO_PANEL = 2;//刷新信息面板 参数：目前为止 是服务器定的
    public const int SHOW_ENTER_ROOM_BUTTON = 3;//显示进入房间按钮
    public const int CREATE_PANEL_ACTIVE = 4;//设置创建面板的显示

    public const int REFRESH_MAHJONG_POSITION = 5;//刷新麻将位置

    //public const int PLAYER_READY = 7;//角色准备
    //public const int PLAYER_ENTER = 8;//角色进入
    //public const int PLAYER_LEAVE = 9;//角色离开
    //public const int PLAYER_CHAT = 10;//角色聊天
    //public const int PLAYER_CHANGE_IDENTITY = 11;//角色更改身份
    //public const int PLAYER_HIDE_STATE = 12;//开始游戏 角色隐藏状态面板
    //public const int PLAYER_HIDE_READY_BUTTON = 17;//玩家准备 隐藏准备按钮


    //public const int SHOW_OVER_PANEL = 18;//显示结束面板

    public const int SET_CAMERA = 19;//根据座位设置摄像头
    public const int SET_INFORMATION = 20;//更新信息
    public const int HIND_READY = 21;//隐藏准备信息
    public const int READY_THROW = 22;//准备掷骰子
    public const int THROW_THE_DICE = 23;//掷骰子
    public const int INITIAL_DRAW = 24;//开局摸牌
    public const int INITIAL_DRAW_ONE = 25;//开局摸完一张牌

    //....

    public const int PROMPT_MSG = int.MaxValue;
}
