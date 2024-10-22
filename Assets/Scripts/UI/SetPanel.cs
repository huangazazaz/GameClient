﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : UIBase
{
    private Button btnSet;
    private Image imgBg;
    private Button btnClose;
    private Text txtAudio;
    private Toggle togAudio;
    private Text txtVolume;
    private Slider sldVolume;

    void Start()
    {
        btnSet = transform.Find("btnSet").GetComponent<Button>();
        imgBg = transform.Find("imgBg").GetComponent<Image>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        txtAudio = transform.Find("txtAudio").GetComponent<Text>();
        togAudio = transform.Find("togAudio").GetComponent<Toggle>();
        txtVolume = transform.Find("txtVolume").GetComponent<Text>();
        sldVolume = transform.Find("sldVolume").GetComponent<Slider>();

        btnSet.onClick.AddListener(setClick);
        btnClose.onClick.AddListener(closeClick);
        togAudio.onValueChanged.AddListener(audioValueChanged);
        sldVolume.onValueChanged.AddListener(volumeValueChanged);
        audioValueChanged(false);

        //默认状态
        setObjectsActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        btnSet.onClick.RemoveListener(setClick);
        btnClose.onClick.RemoveListener(closeClick);
        togAudio.onValueChanged.RemoveListener(audioValueChanged);
        sldVolume.onValueChanged.RemoveListener(volumeValueChanged);
    }

    private void setObjectsActive(bool active)
    {
        imgBg.gameObject.SetActive(active);
        btnClose.gameObject.SetActive(active);
        togAudio.gameObject.SetActive(active);
        sldVolume.gameObject.SetActive(active);
        txtAudio.gameObject.SetActive(active);
        txtVolume.gameObject.SetActive(active);
    }


    private void setClick()
    {
        setObjectsActive(true);
    }

    private void closeClick()
    {
        setObjectsActive(false);
    }

    /// <summary>
    /// 开关点击的时候调用
    /// </summary>
    /// <param name="result">勾上了是true 勾掉了是false</param>
    private void audioValueChanged(bool value)
    {
        //操作声音
        if (value == true)
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_BG_AUDIO, null);
        }
        else
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.STOP_BG_AUDIO, null);
        }
    }

    /// <summary>
    /// 当滑动条滑动的时候会调用
    /// </summary>
    /// <param name="value">滑动条的值</param>
    private void volumeValueChanged(float value)
    {
        //操作声音
        Dispatch(AreaCode.AUDIO, AudioEvent.SET_BG_VOLUME, value);
    }


}
