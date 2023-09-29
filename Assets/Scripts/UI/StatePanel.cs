using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatePanel : UIBase
{

    private Image dice1;
    private Image dice2;
    private int ind;
    private MatchRoomDto matchRoom;
    private TMP_Text name0;
    private TMP_Text name1;
    private TMP_Text name2;
    private TMP_Text name3;
    private TMP_Text token0;
    private TMP_Text token1;
    private TMP_Text token2;
    private TMP_Text token3;
    PromptMsg promptMsg;

    // Start is called before the first frame update
    void Start()
    {
        promptMsg = new PromptMsg();
        dice1 = transform.Find("dice1").GetComponent<Image>();
        dice2 = transform.Find("dice2").GetComponent<Image>();
        dice1.GameObject().SetActive(false);
        dice2.GameObject().SetActive(false);
        name0 = transform.Find("name0").GetComponent<TMP_Text>();
        name3 = transform.Find("name3").GetComponent<TMP_Text>();
        name2 = transform.Find("name2").GetComponent<TMP_Text>();
        name1 = transform.Find("name1").GetComponent<TMP_Text>();
        token0 = transform.Find("token0").GetComponent<TMP_Text>();
        token3 = transform.Find("token3").GetComponent<TMP_Text>();
        token2 = transform.Find("token2").GetComponent<TMP_Text>();
        token1 = transform.Find("token1").GetComponent<TMP_Text>();
        name3.gameObject.SetActive(false);
        name2.gameObject.SetActive(false);
        name1.gameObject.SetActive(false);
        name0.gameObject.SetActive(false);
        token3.gameObject.SetActive(false);
        token2.gameObject.SetActive(false);
        token1.gameObject.SetActive(false);
        token0.gameObject.SetActive(false);
    }
    private void Awake()
    {
        Bind(UIEvent.THROW_THE_DICE, UIEvent.SET_INFORMATION, UIEvent.SET_CAMERA);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.THROW_THE_DICE:
                displayDice(message as int[]);
                break;
            case UIEvent.SET_CAMERA:
                {
                    ind = (int)message;
                    //Debug.Log(ind);
                    break;
                }
            case UIEvent.SET_INFORMATION:
                {
                    matchRoom = (MatchRoomDto)message;
                    //int readyInd = matchRoom;

                    for (int i = 1; i <= 4; i++)
                    {
                        if (matchRoom.ReadySeat[i] == -1)
                        {
                            switch ((i - ind + 4) % 4)
                            {
                                case 0:
                                    name0.gameObject.SetActive(false);
                                    token0.gameObject.SetActive(false);
                                    break;
                                case 1:
                                    name1.gameObject.SetActive(false);
                                    token1.gameObject.SetActive(false);
                                    break;
                                case 2:
                                    name2.gameObject.SetActive(false);
                                    token2.gameObject.SetActive(false);
                                    break;
                                case 3:
                                    name3.gameObject.SetActive(false);
                                    token3.gameObject.SetActive(false);
                                    break;

                            }

                        }
                        else if (matchRoom.ReadySeat[i] == 1)
                        {
                            switch ((i - ind + 4) % 4)
                            {
                                case 0:
                                    name0.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    token0.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    name0.gameObject.SetActive(true);
                                    token0.gameObject.SetActive(true);
                                    break;
                                case 1:
                                    name1.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    token1.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    name1.gameObject.SetActive(true);
                                    token1.gameObject.SetActive(true);
                                    break;
                                case 2:
                                    name2.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    token2.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    name2.gameObject.SetActive(true);
                                    token2.gameObject.SetActive(true);
                                    break;
                                case 3:
                                    name3.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    token3.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    name3.gameObject.SetActive(true);
                                    token3.gameObject.SetActive(true);
                                    break;

                            }
                        }
                        else
                        {
                            switch ((i - ind + 4) % 4)
                            {
                                case 0:
                                    name0.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    name0.gameObject.SetActive(true);
                                    token0.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    token0.gameObject.SetActive(true);
                                    break;
                                case 1:
                                    name1.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    name1.gameObject.SetActive(true);
                                    token1.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    token1.gameObject.SetActive(true);
                                    break;
                                case 2:
                                    name2.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    name2.gameObject.SetActive(true);
                                    token2.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    token2.gameObject.SetActive(true);
                                    break;
                                case 3:
                                    name3.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Name;
                                    name3.gameObject.SetActive(true);
                                    token3.text = matchRoom.UIdUserDict[matchRoom.SeatUId[i]].Token.ToString();
                                    token3.gameObject.SetActive(true);
                                    break;

                            }
                        }
                    }
                    break;
                }
            default:
                break;
        }
    }

    void displayDice(int[] dice)
    {

        promptMsg.Change("骰子的点数为", Color.gray);
        Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);

        dice1.gameObject.SetActive(true);
        dice2.gameObject.SetActive(true);

        dice1.sprite = Resources.Load<Sprite>("UI/dice" + dice[0]);
        dice2.sprite = Resources.Load<Sprite>("UI/dice" + dice[1]);
    }

}
