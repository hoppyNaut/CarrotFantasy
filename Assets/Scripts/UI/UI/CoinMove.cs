using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinMove : MonoBehaviour
{
    private Text txt_Coin;
    private Image img_Coin;
    public Sprite[] coinSprites;


    private void Awake()
    {
        txt_Coin = transform.Find("Txt_Coin").GetComponent<Text>();
        img_Coin = transform.Find("Img_Coin").GetComponent<Image>();
        coinSprites = new Sprite[2];
        coinSprites[0] = GameController.Instance.GetSprite("NormalMordel/Game/Coin");
        coinSprites[1] = GameController.Instance.GetSprite("NormalMordel/Game/ManyCoin");
    }

    public void ShowCoin(int coin)
    {
        txt_Coin.text = coin.ToString();
        if(coin >= 500)
        {
            img_Coin.sprite = coinSprites[1];
        }
        else
        {
            img_Coin.sprite = coinSprites[0];
        }
        transform.DOLocalMoveY(70, 0.5f);
        DOTween.To(() => img_Coin.color, toColor => toColor = img_Coin.color, new Color(1, 1, 1, 0), 0.5f);
        Tween tween = DOTween.To(() => txt_Coin.color, toColor => toColor = txt_Coin.color, new Color(1, 1, 1, 0), 0.5f);
        tween.OnComplete(DestroyCoin);
    }

    private void DestroyCoin()
    {
        transform.localPosition = Vector3.zero;
        img_Coin.color = txt_Coin.color = new Color(1, 1, 1, 1);
        GameController.Instance.PushGameObjectToFactory("CoinCanvas", transform.parent.gameObject);
    }
}
