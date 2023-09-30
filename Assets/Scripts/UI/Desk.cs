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

    Dictionary<string, GameObject> nameMahjong;
    GameObject table = null;
    Dictionary<int, string> MahjongOrder;

    private void Awake()
    {
        Bind(UIEvent.REFRESH_MAHJONG_POSITION, UIEvent.INITIAL_DRAW, UIEvent.INITIAL_DRAW_ONE);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REFRESH_MAHJONG_POSITION or UIEvent.INITIAL_DRAW_ONE:
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


    void Start()
    {
        nameMahjong = new Dictionary<string, GameObject>();
        MahjongOrder = new Dictionary<int, string>();
        for (int i = 1; i <= 108; i++)
        {
            MahjongOrder.Add(i, "");
        }
        table = GameObject.Find("Plane");
        for (int type = 1; type <= 3; type++)
        {
            for (int weight = 1; weight <= 9; weight++)
            {
                for (int index = 1; index <= 4; index++)
                {
                    MahjongDto dto = new MahjongDto(type, weight, index);
                    GameObject mah = GameObject.Find(dto.getName());
                    nameMahjong.Add(dto.getName(), mah);
                }
            }

        }
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
                nameMahjong[name].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 - 0.2), -6);
                nameMahjong[name].transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }
        for (int z = -4; z <= 8; z++)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjong[name].transform.position = table.transform.position + new Vector3(-6, (float)(h * 0.7 - 0.2), z);
                nameMahjong[name].transform.rotation = Quaternion.Euler(0, 90, -90);
            }
        }
        for (int x = -4; x <= 9; x++)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjong[name].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 - 0.2), 6);
                nameMahjong[name].transform.rotation = Quaternion.Euler(0, 180, -90);
            }
        }
        for (int z = 4; z >= -8; z--)
        {
            for (int h = 2; h >= 1; h--)
            {
                string name = dto.positions[ind++];
                if (name == "") continue;
                nameMahjong[name].transform.position = table.transform.position + new Vector3(6, (float)(h * 0.7 - 0.2), z);
                nameMahjong[name].transform.rotation = Quaternion.Euler(0, -90, -90);
            }
        }



        for (int i = 1; i <= 4; i++)
        {
            if (dto.SeatHandMahjongs[i].Count > 0)
            {
                switch (i)
                {
                    case 1:
                        for (int j = 0; j < dto.SeatHandMahjongs[i].Count; j++)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjong[name].transform.position = table.transform.position + new Vector3(j - 6, 0, -11);
                            nameMahjong[name].transform.rotation = Quaternion.Euler(-90, 90, 0);
                        }
                        break;
                    case 2:
                        for (int j = 0; j < dto.SeatHandMahjongs[i].Count; j++)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjong[name].transform.position = table.transform.position + new Vector3(11, 0, j - 6);
                            nameMahjong[name].transform.rotation = Quaternion.Euler(-90, 0, 0);
                        }
                        break;
                    case 3:
                        for (int j = 0; j < dto.SeatHandMahjongs[i].Count; j++)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjong[name].transform.position = table.transform.position + new Vector3(6 - j, 0, 11);
                            nameMahjong[name].transform.rotation = Quaternion.Euler(-90, 0, -90);
                        }
                        break;
                    case 4:
                        for (int j = 0; j < dto.SeatHandMahjongs[i].Count; j++)
                        {
                            string name = dto.SeatHandMahjongs[i][j];
                            nameMahjong[name].transform.position = table.transform.position + new Vector3(-11, 0, 6 - j);
                            nameMahjong[name].transform.rotation = Quaternion.Euler(-90, 180, 0);
                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}

