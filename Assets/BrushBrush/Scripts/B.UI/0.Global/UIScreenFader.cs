using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 씬 전환간 페이드 인/아웃
/// </summary>
public class UIScreenFader :UIView
{
    [SerializeField] Image img_fade;
    [SerializeField] Color fadeColor = Color.black;

    //
    private CancellationTokenSource _fadeCts;

    //==========================================================================
    public override UILayer Layer => UILayer.TOP; // 연출용 레이어 사용 (최상단)


    public override void OnShow()
    {
        
    }

    public override void OnHide()
    {
        
    }
    //==========================================================================
    /// <summary>
    /// 즉시 투명
    /// </summary>
    public void SetTransparentImmediatly()
    {
        img_fade.color = new Color(0,0,0,0);
        img_fade.gameObject.SetActive(false);
    }

    /// <summary>
    /// 즉시 검정
    /// </summary>
    public void SetBlackImmediatly()
    {
        img_fade.gameObject.SetActive(true);
        img_fade.color = fadeColor;
    }

    // <summary>
    ///  페이드인 : 화면 까매짐
    /// </summary>
    public async UniTask FadeInAsync(float duration = 1f)
    {
        gameObject.SetActive(true);
        img_fade.gameObject.SetActive(true);
        await img_fade.DOFade(1, duration).PlayAsync(gameObject, true);
    }

    /// <summary>s
    /// 페이드 아웃 : 화면 밝아짐 - 씬 전환되고 
    /// </summary>
    public async UniTask FadeOutAsync(float duration = 1f)
    {
        await img_fade.DOFade(0, duration).PlayAsync(gameObject, true);
        img_fade.gameObject.SetActive(false);
    }
}
