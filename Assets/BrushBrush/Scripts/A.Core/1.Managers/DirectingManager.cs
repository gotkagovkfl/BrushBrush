using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 인게임 연출
/// </summary>
public class DirectingManager : MonoSingleton<DirectingManager>
{
    [SerializeField] UIManager _uiManager;    // UI 매니저 캐시(주입)
    [SerializeField] UIScreenFader _screenFader;


    /// <summary>
    /// 필요한 연출 요소들 가져오기
    /// </summary>
    protected async override UniTask InitImpl()
    {
        // UIManager로 부터 해상도 받아와서 캔버스 설정
        _uiManager = UIManager.Instance;

        // screen fade 초기화
        _screenFader = _uiManager.Show<UIScreenFader>();
        _screenFader.SetTransparentImmediatly();
        _screenFader.Hide();

    }
    
    //============================================================
    #region [ 페이드 인/아웃 ]
    //============================================================

    // <summary>
    ///  페이드인 : 화면 까매짐
    /// </summary>
    public async UniTask FadeInAsync(float duration = 1f)
    {
        _screenFader.gameObject.SetActive(true);
        await _screenFader.FadeInAsync(duration);
        // 비활성화는 fade out에서 진행
    }

    /// <summary>s
    /// 페이드 아웃 : 화면 밝아짐 - 씬 전환되고 
    /// </summary>
    public async UniTask FadeOutAsync(float duration = 1f)
    {
        // 활성화는 fade in에서 진행
        await _screenFader.FadeOutAsync(duration);
        _screenFader.gameObject.SetActive(false);
    }

    #endregion
}
