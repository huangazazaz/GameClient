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
    Dictionary<string, GameObject> nameMahjongs2;
    Dictionary<string, GameObject> nameMahjongs1;
    Dictionary<string, GameObject> nameMahjongs;
    GameObject table;
    Dictionary<int, string> MahjongOrder;

    private void Awake()
    {
        Bind(UIEvent.REFRESH_MAHJONG_POSITION, UIEvent.INITIAL_DRAW, UIEvent.INITIAL_DRAW_FINISH, UIEvent.DRAW);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REFRESH_MAHJONG_POSITION or UIEvent.INITIAL_DRAW_FINISH or UIEvent.DRAW:
                RefreshPosition(message as FightRoomDto);
                break;
            case UIEvent.INITIAL_DRAW:
                RefreshPosition(message as FightRoomDto);
                Dispatch(AreaCode.UI, UIEvent.INITIAL_DRAW_ONE, message as FightRoomDto);
                break;
            default:
                break;
        }
    }

    int style = 2;

    void Start()
    {
        nameMahjongs = new Dictionary<string, GameObject>();
        nameMahjongs1 = new Dictionary<string, GameObject>();
        nameMahjongs2 = new Dictionary<string, GameObject>();
        MahjongOrder = new Dictionary<int, string>();
        for (int i = 1; i <= 108; i++)
        {
            MahjongOrder.Add(i, "");
        }
        table = GameObject.Find("table");
        for (int type = 1; type <= 3; type++)
        {
            for (int weight = 1; weight <= 9; weight++)
            {
                for (int index = 1; index <= 4; index++)
                {
                    MahjongDto dto = new MahjongDto(type, weight, index);
                    string name = dto.getName();
                    GameObject mah = GameObject.Find(name);
                    GameObject mah2 = GameObject.Find(name + "2");
                    nameMahjongs1.Add(name, mah);
                    nameMahjongs2.Add(name + "2", mah2);
                    mah.gameObject.SetActive(false);
                }
            }

        }
        nameMahjongs = nameMahjongs2;
    }



    void RefreshPosition(FightRoomDto dto)
    {
        int ind = 0;

        for (int x = 4; x >= -9; x--)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 + 0.2), -6);
                nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }
        for (int z = -4; z <= 8; z++)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(-6, (float)(h * 0.7 + 0.2), z);
                nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(90, -90, 0);
            }
        }
        for (int x = -4; x <= 9; x++)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 + 0.2), 6);
                nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(90, 180, 0);
            }
        }
        for (int z = 4; z >= -8; z--)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(6, (float)(h * 0.7 + 0.2), z);
                nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(90, 90, 0);
            }
        }


        for (int i = 1; i <= 4; i++)
        {
            if (dto.drawMahjong[i] != "")
            {
                switch (i)
                {
                    case 1:
                        nameMahjongs[dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(8, (float)1.2, -11);
                        nameMahjongs[dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    case 2:
                        nameMahjongs[dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(11, (float)1.2, 8);
                        nameMahjongs[dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case 3:
                        nameMahjongs[dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(-8, (float)1.2, 11);
                        nameMahjongs[dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case 4:
                        nameMahjongs[dto.drawMahjong[i] + style.ToString()].transform.position = table.transform.position + new Vector3(-11, (float)1.2, -8);
                        nameMahjongs[dto.drawMahjong[i] + style.ToString()].transform.rotation = Quaternion.Euler(0, -90, 0);
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
                        for (int j = 0; j < dto.SeatHandMahjongs[i].Count; j++)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(j - 6, (float)1.2, -11);
                            nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                        break;
                    case 2:
                        for (int j = 0; j < dto.SeatHandMahjongs[i].Count; j++)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(11, (float)1.2, j - 6);
                            nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(0, 90, 0);
                        }
                        break;
                    case 3:
                        for (int j = 0; j < dto.SeatHandMahjongs[i].Count; j++)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(6 - j, (float)1.2, 11);
                            nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        break;
                    case 4:
                        for (int j = 0; j < dto.SeatHandMahjongs[i].Count; j++)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(-11, (float)1.2, 6 - j);
                            nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(0, -90, 0);
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
                                nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(j - 8, (float)0.85, -9);
                                nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 180, 0);
                            }
                            break;
                        case 2:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(9, (float)0.85, j - 8);
                                nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 90, 0);
                            }
                            break;
                        case 3:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(8 - j, (float)0.85, 9);
                                nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(-90, 0, 0);
                            }
                            break;
                        case 4:
                            for (int j = 0; j < dto.playMahjongs[i].Count; j++)
                            {
                                string name = dto.playMahjongs[i][j];
                                nameMahjongs[name + style.ToString()].transform.position = table.transform.position + new Vector3(-9, (float)0.85, 8 - j);
                                nameMahjongs[name + style.ToString()].transform.rotation = Quaternion.Euler(-90, -90, 0);
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

