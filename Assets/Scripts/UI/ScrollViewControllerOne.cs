using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//滑动方式：根据拖拽结束时ScrollView的HorizontalNormalizedPosition值确定最近的页面
public class ScrollViewControllerOne : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private ScrollRect scrollRect;
    private GridLayoutGroup layout;
    //每个页面的normalizedPosition值
    private float[] page;
    //拖动是否结束
    private bool isDrag = false;
    //目标位置
    private float targetHorizontalPosition = 0.0f;
    //滑动速度
    public float smoothingSpeed = 10.0f;

    private int curIndex;
    private int totalIndex;

    public Text txt_Page;

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        layout = GetComponentInChildren<GridLayoutGroup>();
        scrollRect.onValueChanged.AddListener((vector2) => { Debug.Log(scrollRect.horizontalNormalizedPosition); });

        Init();
    }

    void Update()
    {
        if(!isDrag)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetHorizontalPosition, smoothingSpeed * Time.deltaTime);
        }
    }

    public void Init()
    {
        int length = scrollRect.content.childCount;
        totalIndex = length;
        curIndex = 0;
        SetTxtPage();
        targetHorizontalPosition = 0.0f;
        scrollRect.horizontalNormalizedPosition = 0;

        //初始化content的宽度
        scrollRect.content.sizeDelta = new Vector2((layout.cellSize.x + layout.spacing.x) * (length - 1),scrollRect.GetComponent<RectTransform>().sizeDelta.y);
        //初始化数组

        page = new float[length];
        for(int i = 0; i < length; i++)
        {
            page[i] = i * 1.0f / (length - 1);
        }
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        float posX = scrollRect.horizontalNormalizedPosition;
        int index = 0;
        float offsetX = Mathf.Abs(posX - page[index]);
        for(int i = 1; i < page.Length; i++)
        {
            float tempX = Mathf.Abs(posX - page[i]);
            if(tempX < offsetX)
            {
                offsetX = tempX;
                index = i;
            }
        }
        targetHorizontalPosition = page[index];
        curIndex = index;
        SetTxtPage();
    }

    public void SetTxtPage()
    {
        if(txt_Page != null)
        {
            string msg = (curIndex + 1).ToString() + "/" + totalIndex;
            txt_Page.text = msg;
        }
    }

    public void MoveNext()
    {
        if(curIndex < page.Length - 1)
        {
            curIndex++;
            targetHorizontalPosition = page[curIndex];
            SetTxtPage();
        }
    }

    public void MovePrev()
    {
        if(curIndex > 0)
        {
            curIndex--;
            targetHorizontalPosition = page[curIndex];
            SetTxtPage();
        }
    }
}
