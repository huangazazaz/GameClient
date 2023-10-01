using Protocol.Code;
using Protocol.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistPanel : UIBase
{
    private void Awake()
    {
        Bind(UIEvent.REGIST_PANEL_ACTIVE, UIEvent.REGISTER_SUCCESS_R);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REGIST_PANEL_ACTIVE:
                setPanelActive((bool)message);
                inputAccount.ActivateInputField();
                break;
            case UIEvent.REGISTER_SUCCESS_R:
                Dispatch(AreaCode.UI, UIEvent.REGISTER_SUCCESS_L, inputAccount.text);
                inputAccount.text = "";
                inputPassword.text = "";
                inputConfirm.text = "";
                setPanelActive(false);
                break;
            default:
                break;
        }
    }

    private Button registerBtn;
    private Button backBtn;
    private TMP_InputField inputAccount;
    private TMP_InputField inputPassword;
    private TMP_InputField inputConfirm;

    private PromptMsg promptMsg;
    private SocketMsg socketMsg;

    // Use this for initialization
    void Start()
    {
        registerBtn = transform.Find("registerBtn").GetComponent<Button>();
        backBtn = transform.Find("backBtn").GetComponent<Button>();
        inputAccount = transform.Find("inputAccountReg").GetComponent<TMP_InputField>();
        inputPassword = transform.Find("inputPasswordReg").GetComponent<TMP_InputField>();
        inputConfirm = transform.Find("inputConfirm").GetComponent<TMP_InputField>();

        backBtn.onClick.AddListener(closeClick);
        registerBtn.onClick.AddListener(registClick);

        inputAccount.ActivateInputField();
        promptMsg = new PromptMsg();
        socketMsg = new SocketMsg();

        setPanelActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        backBtn.onClick.RemoveListener(closeClick);
        registerBtn.onClick.RemoveListener(registClick);
    }

    //AccountDto dto = new AccountDto();
    //SocketMsg socketMsg = new SocketMsg();

    /// <summary>
    /// 注册按钮的点击事件处理
    /// </summary>
    private void registClick()
    {
        if (string.IsNullOrEmpty(inputAccount.text))
        {
            promptMsg.Change("注册的用户名不能为空！", Color.red);
            Console.WriteLine("账号不合法");
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        if (string.IsNullOrEmpty(inputPassword.text)
            || inputPassword.text.Length < 4
            || inputPassword.text.Length > 16)
        {
            Console.WriteLine("密码不合法");
            promptMsg.Change("注册的密码不合法！", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        if (string.IsNullOrEmpty(inputConfirm.text)
            || inputConfirm.text != inputPassword.text)
        {
            promptMsg.Change("请确保两次输入的密码一致！", Color.red);
            Console.WriteLine("密码不合法");
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }

        AccountDto dto = new AccountDto(inputAccount.text, inputPassword.text);
        socketMsg.Change(OpCode.ACCOUNT, AccountCode.REGIST_CREQ, dto);
        //dto.Account = inputAccount.text;
        //dto.Password = inputPassword.text;
        //socketMsg.OpCode = OpCode.ACCOUNT;
        //socketMsg.SubCode = AccountCode.REGIST_CREQ;
        //socketMsg.Value = dto;
        Dispatch(AreaCode.NET, 0, socketMsg);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputAccount.isFocused)
            {
                inputPassword.ActivateInputField();
            }
            else if (inputPassword.isFocused)
            {
                inputConfirm.ActivateInputField();
            }
            else
            {
                inputAccount.ActivateInputField();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            registClick();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            closeClick();
        }

    }

    private void closeClick()
    {
        setPanelActive(false);
    }
}
