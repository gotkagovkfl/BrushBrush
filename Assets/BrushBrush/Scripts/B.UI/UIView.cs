using UnityEngine;

/// <summary>
/// ui의 기본 클래스
/// </summary>
public abstract class UIView : MonoBehaviour
{
    // 생성 기본값
    public virtual UILayer Layer => UILayer.LV_2;

    //
    public abstract void OnShow();
    public abstract void OnHide();

    protected virtual void OnDestroy() { }
}
