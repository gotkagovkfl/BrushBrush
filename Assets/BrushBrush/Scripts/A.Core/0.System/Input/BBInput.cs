using UnityEngine;
using System;


public enum InputType
{
    //
    ESCAPE,
    CONFIRM,

    //
    MOVE_U,     // 상
    MOVE_D,     // 하
    MOVE_L,     // 좌
    MOVE_R,     // 우

    // 게임 플레이
    SKILL_1,    // 기본 공격    
    SKILL_2,    // 공격 기술
    SKILL_3,    // 유틸 기술
}


/// <summary>
/// 게임 인풋 
/// </summary>
[Serializable]
public class BBInput
{
    public readonly InputType Type;

    IInputBinding _binding;

    public event Action OnKeyDown;
    public event Action OnKey;
    public event Action OnKeyUp;

    public void Evaluate()
    {
        if (_binding.GetKeyDown())
            OnKeyDown?.Invoke();

        if (_binding.GetKey())
            OnKey?.Invoke();

        if (_binding.GetKeyUp())
            OnKeyUp?.Invoke();
    }

    public BBInput(InputType type, IInputBinding binding)
    {
        Type = type;
        this._binding = binding;
    }
}