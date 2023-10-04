using Protocol.Dto;
using Protocol.Dto.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Desk : UIBase
{
    Dictionary<int, Dictionary<string, GameObject>> nameMahjongs;
    GameObject table;
    GameObject[] dices;

    private void Awake()
    {
        Bind(UIEvent.WAFFLE_PLAY, UIEvent.REFRESH_MAHJONG_POSITION, UIEvent.INITIAL_DRAW, UIEvent.INITIAL_DRAW_FINISH, UIEvent.DRAW, UIEvent.THROW_THE_DICE);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REFRESH_MAHJONG_POSITION or UIEvent.INITIAL_DRAW_FINISH or UIEvent.DRAW:
                if (style < 3 && mjCnt == 108)
                {
                    RefreshPosition_108_12(message as FightRoomDto);
                }
                else
                {
                    RefreshPosition_144_12(message as FightRoomDto);
                }
                break;
            case UIEvent.INITIAL_DRAW or UIEvent.WAFFLE_PLAY:
                if (style < 3 && mjCnt == 108)
                {
                    RefreshPosition_108_12(message as FightRoomDto);
                }
                else
                {
                    RefreshPosition_144_12(message as FightRoomDto);
                }
                Dispatch(AreaCode.UI, UIEvent.INITIAL_DRAW_ONE, message as FightRoomDto);
                break;
            case UIEvent.THROW_THE_DICE:
                refreshDice(message as FightRoomDto);
                break;
            default:
                break;
        }
    }

    int style = 1;
    int styleCnt = 2;
    int mjCnt = 144;

    void refreshDice(FightRoomDto dto)
    {
        for (int i = 0; i < 2; i++)
        {
            switch (dto.dice[i])
            {
                case 1:
                    dices[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                    dices[i].transform.position = table.transform.position + new Vector3(1 - 2 * i, (float)0.47, 1 - 2 * i);
                    break;
                case 2:
                    dices[i].transform.rotation = Quaternion.Euler(0, 0, -90);
                    dices[i].transform.position = table.transform.position + new Vector3(1 - 2 * i, (float)0.74, 1 - 2 * i);
                    break;
                case 3:
                    dices[i].transform.rotation = Quaternion.Euler(90, 0, -90);
                    dices[i].transform.position = table.transform.position + new Vector3(1 - 2 * i, (float)0.71, 1 - 2 * i);
                    break;
                case 4:
                    dices[i].transform.rotation = Quaternion.Euler(180, 0, -90);
                    dices[i].transform.position = table.transform.position + new Vector3(1 - 2 * i, (float)0.77, 1 - 2 * i);
                    break;
                case 5:
                    dices[i].transform.rotation = Quaternion.Euler(-90, 0, -90);
                    dices[i].transform.position = table.transform.position + new Vector3(1 - 2 * i, (float)0.79, 1 - 2 * i);
                    break;
                case 6:
                    dices[i].transform.rotation = Quaternion.Euler(0, 0, 180);
                    dices[i].transform.position = table.transform.position + new Vector3(1 - 2 * i, (float)1.035, 1 - 2 * i);
                    break;

            }
        }
    }

    void Start()
    {
        nameMahjongs = new Dictionary<int, Dictionary<string, GameObject>>();
        dices = new GameObject[] { GameObject.Find("dice1"), GameObject.Find("dice2") };

        table = GameObject.Find("table");
        for (int i = 1; i <= styleCnt; i++)
        {
            nameMahjongs.Add(i, new Dictionary<string, GameObject>());
        }
        for (int type = 1; type <= 5; type++)
        {
            int num = type <= 3 ? 9 : (type == 4 ? 7 : 8);
            for (int weight = 1; weight <= num; weight++)
            {
                int len = type == 5 ? 1 : 4;

                for (int index = 1; index <= len; index++)
                {
                    string name = new MahjongDto(type, weight, index).getName();
                    for (int i = 1; i <= styleCnt; i++)
                    {
                        GameObject mah = GameObject.Find(name + i.ToString());
                        nameMahjongs[i].Add(name + i.ToString(), mah);
                        mah.SetActive(false);
                    }
                }
            }

        }
    }



    void RefreshPosition_108_12(FightRoomDto dto)
    {
        for (int type = 1; type <= 3; type++)
        {
            for (int weight = 1; weight <= 9; weight++)
            {
                for (int index = 1; index <= 4; index++)
                {
                    string name = new MahjongDto(type, weight, index).getName();
                    for (int i = 1; i <= styleCnt; i++)
                    {
                        if (i != style) nameMahjongs[i][name + i.ToString()].SetActive(false);
                        else nameMahjongs[i][name + i.ToString()].SetActive(true);
                    }
                }
            }
        }
        int ind = 0;

        for (int x = 4; x >= -9; x--)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 + 0.2), -6);
                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }
        for (int z = -4; z <= 8; z++)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(-6, (float)(h * 0.7 + 0.2), z);
                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(90, -90, 0);
            }
        }
        for (int x = -4; x <= 9; x++)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 + 0.2), 6);
                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(90, 180, 0);
            }
        }
        for (int z = 4; z >= -8; z--)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(6, (float)(h * 0.7 + 0.2), z);
                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(90, 90, 0);
            }
        }


        for (int i = 1; i <= 4; i++)
        {
            if (dto.drawMahjong[i] != "")
            {
                switch (i)
                {
                    case 1:
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(8, (float)1.2, -11);
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    case 2:
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(11, (float)1.2, 8);
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case 3:
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(-8, (float)1.2, 11);
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case 4:
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(-11, (float)1.2, -8);
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, -90, 0);
                        break;
                    default:
                        break;
                }
            }

            if (dto.SeatHandMahjongs[i].Count > 0)
            {
                switch (i)
                {
                    case 1:
                        for (int j = dto.SeatHandMahjongs[i].Count - 1; j >= 0; j--)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(j + 7 - dto.SeatHandMahjongs[i].Count, (float)1.2, -11);
                            nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                        break;
                    case 2:
                        for (int j = dto.SeatHandMahjongs[i].Count - 1; j >= 0; j--)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(11, (float)1.2, 7 - dto.SeatHandMahjongs[i].Count + j);
                            nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(0, 90, 0);
                        }
                        break;
                    case 3:
                        for (int j = dto.SeatHandMahjongs[i].Count - 1; j >= 0; j--)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(-(7 - dto.SeatHandMahjongs[i].Count + j), (float)1.2, 11);
                            nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        break;
                    case 4:
                        for (int j = dto.SeatHandMahjongs[i].Count - 1; j >= 0; j--)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(-11, (float)1.2, -(7 - dto.SeatHandMahjongs[i].Count + j));
                            nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(0, -90, 0);
                        }
                        break;
                    default:
                        break;
                }
                if (dto.playMahjongs[i].Count > 0)
                {
                    switch (i)
                    {
                        case 1:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(j - 8, (float)0.85, -9);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 180, 0);
                            }
                            break;
                        case 2:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(9, (float)0.85, j - 8);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 90, 0);
                            }
                            break;
                        case 3:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(8 - j, (float)0.85, 9);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 0, 0);
                            }
                            break;
                        case 4:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(-9, (float)0.85, 8 - j);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, -90, 0);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
        }
    }
    void RefreshPosition_144_12(FightRoomDto dto)
    {
        for (int type = 1; type <= 5; type++)
        {
            int num = type <= 3 ? 9 : (type == 4 ? 7 : 8);
            for (int weight = 1; weight <= num; weight++)
            {
                int len = type == 5 ? 1 : 4;

                for (int index = 1; index <= len; index++)
                {
                    string name = new MahjongDto(type, weight, index).getName();
                    for (int i = 1; i <= styleCnt; i++)
                    {
                        if (i != style) nameMahjongs[i][name + i.ToString()].SetActive(false);
                        else nameMahjongs[i][name + i.ToString()].SetActive(true);
                    }
                }
            }

        }

        int ind = 0;

        for (int x = 7; x >= -10; x--)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 + 0.2), (float)-8.5);
                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }
        for (int z = -7; z <= 10; z++)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3((float)-8.5, (float)(h * 0.7 + 0.2), z);
                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(90, -90, 0);
            }
        }
        for (int x = -7; x <= 10; x++)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 + 0.2), (float)8.5);
                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(90, 180, 0);
            }
        }
        for (int z = 7; z >= -10; z--)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3((float)8.5, (float)(h * 0.7 + 0.2), z);
                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(90, 90, 0);
            }
        }


        for (int i = 1; i <= 4; i++)
        {
            if (dto.drawMahjong[i] != "")
            {
                switch (i)
                {
                    case 1:
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(8, (float)1.2, -11);
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    case 2:
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(11, (float)1.2, 8);
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case 3:
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(-8, (float)1.2, 11);
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case 4:
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(-11, (float)1.2, -8);
                        nameMahjongs[style][dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, -90, 0);
                        break;
                    default:
                        break;
                }
            }

            if (dto.SeatHandMahjongs[i].Count > 0)
            {
                switch (i)
                {
                    case 1:
                        for (int j = dto.SeatHandMahjongs[i].Count - 1; j >= 0; j--)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(7 - dto.SeatHandMahjongs[i].Count + j, (float)1.2, -11);
                            nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                        break;
                    case 2:
                        for (int j = dto.SeatHandMahjongs[i].Count - 1; j >= 0; j--)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(11, (float)1.2, 7 - dto.SeatHandMahjongs[i].Count + j);
                            nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(0, 90, 0);
                        }
                        break;
                    case 3:
                        for (int j = dto.SeatHandMahjongs[i].Count - 1; j >= 0; j--)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(-(7 - dto.SeatHandMahjongs[i].Count + j), (float)1.2, 11);
                            nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        break;
                    case 4:
                        for (int j = dto.SeatHandMahjongs[i].Count - 1; j >= 0; j--)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(-11, (float)1.2, -(7 - dto.SeatHandMahjongs[i].Count + j));
                            nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(0, -90, 0);
                        }
                        break;
                    default:
                        break;
                }

                if (dto.playMahjongs[i].Count > 0)
                {
                    switch (i)
                    {
                        case 1:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(j % 9 - 6, (float)0.85, (float)(-6 + j / 9 * 1.5));
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 180, 0);
                            }
                            break;
                        case 2:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3((float)(6 - (j / 9 * 1.5)), (float)0.85, j % 9 - 6);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 90, 0);
                            }
                            break;
                        case 3:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(6 - j % 9, (float)0.85, (float)(6 - j / 9 * 1.5));
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 0, 0);
                            }
                            break;
                        case 4:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3((float)(-6 + j / 9 * 1.5), (float)0.85, 6 - j % 9);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, -90, 0);
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (dto.waffles[i].Count > 0)
                {
                    switch (i)
                    {
                        case 1:
                            for (int j = 0; j < dto.waffles[i].Count; j++)
                            {
                                string name = dto.waffles[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(j - 10, (float)0.85, -11);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 180, 0);
                            }
                            break;
                        case 2:
                            for (int j = 0; j < dto.waffles[i].Count; j++)
                            {
                                string name = dto.waffles[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(11, (float)0.85, j - 10);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 90, 0);
                            }
                            break;
                        case 3:
                            for (int j = 0; j < dto.waffles[i].Count; j++)
                            {
                                string name = dto.waffles[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(10 - j, (float)0.85, 11);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 0, 0);
                            }
                            break;
                        case 4:
                            for (int j = 0; j < dto.waffles[i].Count; j++)
                            {
                                string name = dto.waffles[i][j];
                                nameMahjongs[style][name + style.ToString()].transform.position = table.transform.position + new Vector3(-11, (float)0.85, 10 - j);
                                nameMahjongs[style][name + style.ToString()].transform.rotation = Quaternion.Euler(-90, -90, 0);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }

        }
    }
}

