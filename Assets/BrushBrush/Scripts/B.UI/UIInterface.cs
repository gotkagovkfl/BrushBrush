using System;


/// <summary>
/// ui 파라미터
/// </summary>
public interface IParamReceiver<TParam>
{
    void Init(TParam param);
}

/// <summary>
/// 
/// </summary>
public interface IModelBinder<TModel> where TModel : IUIModel
{
    void Bind(TModel model);
    void Unbind();
}

public interface IUIModel
{
    event Action OnChanged;
}