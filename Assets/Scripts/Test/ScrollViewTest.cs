using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollViewTest : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private ScrollRect scrollRect;
    private RectTransform contentRectTrans;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();

        //关于RectTransform的探究
        contentRectTrans = scrollRect.content;

        //当前UI的世界坐标
        Debug.Log("当前UI的世界坐标：" + contentRectTrans.position);
        //当前UI的局部坐标
        Debug.Log("当前UI的局部坐标：" + contentRectTrans.localPosition);

        //当前UI的宽度
        Debug.Log("当前UI的宽度：" + contentRectTrans.rect.width);
        //当前UI的高度
        Debug.Log("当前UI的高度：" + contentRectTrans.rect.height);

        //当前UI底部相对于顶部的相对长度，负数向下延展，同理则相反
        Debug.Log(contentRectTrans.rect.y);

        //当前transform的x轴轴向方向
        //就像是transform.right
        Debug.Log(contentRectTrans.right);

        contentRectTrans.sizeDelta = new Vector2(650, 436);

        //水平滚动位置为0到1的值 左侧为0 右侧为1
        scrollRect.horizontalNormalizedPosition = 0.5f;

        scrollRect.onValueChanged.AddListener(PrintValue);
    }

   
    void Update()
    {
        
    }

    private void PrintValue(Vector2 vector2)
    {
        Debug.Log("传递进来的参数值" + vector2);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
    }
}
