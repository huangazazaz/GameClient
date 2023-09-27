using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIEventChange : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{

    public TMP_Text TextContent;

    //void Start()
    //{
    //    //HideText();
    //}

    //public void ShowText()
    //{
    //    TextContent.text = "This is " + name + "'s infomation!";
    //}

    //public void HideText()
    //{
    //    TextContent.text = "None infomation.";
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ShowText();
        //Debug.Log("OnPointerEnter call by " + name);
        TextContent.fontSize = TextContent.fontSize + 10;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //HideText();
        //Debug.Log("OnPointerExit call by" + name);
        TextContent.fontSize = TextContent.fontSize - 10;
    }
}
