using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOutUI : UIBase
{
    [SerializeField]private Image rightImage;
    [SerializeField]private Image leftImage;

    protected override void Start()
    {
        base.Start();
        rightImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height);
        leftImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height);
        
        leftImage.transform.localPosition = new Vector2(Screen.width / 2,0);
        rightImage.transform.localPosition = new Vector2(-Screen.width / 2, 0);
    }

    public void SetFade(int num)
    {
        leftImage.transform.DOLocalMoveX(0, 1.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(1f, () => SceneManager.LoadScene(num));
        });
        rightImage.transform.DOLocalMoveX(0, 1.5f);
    }
}
