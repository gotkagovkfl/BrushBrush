using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

/// <summary>
/// 사용할 캔버스 - 값이 클수록 위에 표시
/// </summary>
public enum UILayer
{
    LV_1 = 1,
    LV_2 = 2,
    LV_3 = 3,
    LV_4 = 4,
}

/// <summary>
/// Game의 UI 담당
/// </summary>
public class UIManager : MonoSingleton<UIManager>
{
    // 캔버스
    Dictionary<UILayer, Canvas> _canvasMap = new();
    
    // uiViews
    const string PATH_BASE = "UI/";
    Dictionary<Type, UIView> _uiViews = new();

    //================================================================
    protected async override UniTask InitImpl()
    {
        BuildCanvases(); // 캔버스 생성

        await UniTask.WaitForEndOfFrame();  // 캔버스 rect 
    }

    //================================================================
    #region [ Canvas ]
    //================================================================
    /// <summary>
    /// UILayer 타입에 해당하는 모든 캔버스 생성
    /// </summary>
    void BuildCanvases()
    {
        // 
        if (_canvasMap.Count > 0)
        {
            Debug.Log("!! [ UI Manager ] 캔버스 중복 생성 금지");
            return;
        }

        //            
        foreach (UILayer layer in System.Enum.GetValues(typeof(UILayer)))
        {
            var go = new GameObject(layer.ToString());
            go.transform.SetParent(transform);

            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
            canvas.sortingOrder = (int)layer;

            go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();

            _canvasMap[layer] = canvas;
        }
    }

    public Transform GetLayer(UILayer layer)
        => _canvasMap[layer].transform;


    #endregion
    //================================================================
    #region [ Get ]
    //================================================================
    
    /// <summary>
    /// 해당 타입의 UI 반환
    /// </summary>
    TView GetOrCreate<TView>() where TView: UIView
    {
        var type = typeof(TView);

        // 1. 이미 존재하면 그대로 반환
        if (_uiViews.TryGetValue(type, out var view))
        {
            // 딕셔너리에 있는데, 파괴된 경우 예외 처리
            if (view != null)
            {
                view.gameObject.SetActive(true);
                return view as TView;
            }

            _uiViews.Remove(type);
        }

        // 2. 프리팹 로드 - 프리팹 이름을 커스텀 하는 방법을 생각해보자
        var prefab = Resources.Load<TView>($"{PATH_BASE}{type.Name}");
        
        if (prefab == null)
        {
            Debug.LogError($"{type.Name} 프리팹을 찾을 수 없습니다!");
            return null;
        }

        // 3. 생성 및 캐싱
        var ret = Instantiate(prefab, GetLayer(prefab.Layer) , false);
        _uiViews[type] = ret;

        Canvas.ForceUpdateCanvases(); // 좌표, 해상도 바로 세팅
        return ret;
    }

    #endregion
    //================================================================
    #region [ Show ]
    //================================================================

    /// <summary>
    /// UI 생성 - 일반
    /// </summary>
    public TView Show<TView>() where TView : UIView
    {
        var view = GetOrCreate<TView>();
        view.OnShow();

        return view;
    }

    /// <summary>
    /// UI 생성 - 파라미터
    /// </summary>
    public TView ShowWith<TView, TParam>(TParam param)
        where TView : UIView, IParamReceiver<TParam>
    {
        var view = GetOrCreate<TView>();
        view.Init(param);
        view.OnShow();
        
        return view;
    }

    /// <summary>
    /// UI 생성 - 모델
    /// </summary>
    public TView BindShow<TView, TModel>(TModel model)
        where TView : UIView, IModelBinder<TModel>
        where TModel : IUIModel
    {
        var view = GetOrCreate<TView>();
        view.Bind(model);
        view.OnShow();

        return view;
    }

    /// <summary>
    /// UI 생성 - 파라미터 & 모델
    /// </summary>
    public TView BindShowWith<TView, TParam, TModel>(TParam param, TModel model)
        where TView : UIView, IParamReceiver<TParam>, IModelBinder<TModel>
        where TModel : IUIModel
    {
        var view = GetOrCreate<TView>();
        view.Init(param);
        view.Bind(model);
        view.OnShow();

        return view;
    }

    #endregion
    //================================================================
    #region [ Close ]
    //================================================================

    // TODO : Close  

    #endregion
}
