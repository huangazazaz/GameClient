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

    private void Awake()
    {
        Bind(UIEvent.REFRESH_MAHJONG_POSITION);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REFRESH_MAHJONG_POSITION:
                RefreshPosition(message as FightRoomDto);
                break;
            default:
                break;
        }
    }


    void Start()
    {
        nameMahjong = new Dictionary<string, GameObject>();
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
        for (int h = 1; h <= 2; h++)
        {
            for (int x = 4; x >= -9; x--)
            {
                string name = dto.positions[1][h][5 - x];
                nameMahjong[name].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 - 0.2), -6);
                nameMahjong[name].transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            for (int z = 4; z >= -8; z--)
            {
                string name = dto.positions[2][h][5 - z];
                nameMahjong[name].transform.position = table.transform.position + new Vector3(6, (float)(h * 0.7 - 0.2), z);
                nameMahjong[name].transform.rotation = Quaternion.Euler(0, -90, -90);
            }
            for (int x = 9; x >= -4; x--)
            {
                string name = dto.positions[3][h][10 - x];
                nameMahjong[name].transform.position = table.transform.position + new Vector3(x, (float)(h * 0.7 - 0.2), 6);
                nameMahjong[name].transform.rotation = Quaternion.Euler(0, 180, -90);
            }
            for (int z = 8; z >= -4; z--)
            {
                string name = dto.positions[4][h][9 - z];
                nameMahjong[name].transform.position = table.transform.position + new Vector3(-6, (float)(h * 0.7 - 0.2), z);
                nameMahjong[name].transform.rotation = Quaternion.Euler(0, 90, -90);
            }
        }
    }
}

