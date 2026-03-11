using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Cysharp.Threading.Tasks;

/// <summary>
/// 
/// </summary>
public class InputManager : MonoSingleton<InputManager>
{
    /// <summary> 입력 가능 상태인지 </summary>
    public static bool InputDisabled;
    Dictionary<InputType, BBInput> _inputDic;
    List<BBInput> _inputList;   // eval 하기 위해 캐싱한 리스트

    //================================================================
    #region [ 초기화 ]
    //==================================================================
    /// <summary>
    /// 
    /// </summary>
    protected override async UniTask InitImpl()
    {
        // 기본 설정
        _inputDic = new()
        {
            {InputType.ESCAPE, new BBInput(InputType.ESCAPE, new KeyboardBinding(KeyCode.Escape))},
            {InputType.CONFIRM, new BBInput(InputType.CONFIRM, new KeyboardBinding(KeyCode.Space))},

            {InputType.MOVE_U, new BBInput(InputType.MOVE_U, new KeyboardBinding(KeyCode.UpArrow))},
            {InputType.MOVE_D, new BBInput(InputType.MOVE_D, new KeyboardBinding(KeyCode.DownArrow))},
            {InputType.MOVE_L, new BBInput(InputType.MOVE_L, new KeyboardBinding(KeyCode.LeftArrow))},
            {InputType.MOVE_R, new BBInput(InputType.MOVE_R, new KeyboardBinding(KeyCode.RightArrow))},

            {InputType.SKILL_1, new BBInput(InputType.SKILL_1, new KeyboardBinding(KeyCode.Space))},
            {InputType.SKILL_2, new BBInput(InputType.SKILL_2, new KeyboardBinding(KeyCode.LeftControl))},
            {InputType.SKILL_3, new BBInput(InputType.SKILL_3, new KeyboardBinding(KeyCode.LeftAlt))},

        };

        //
        _inputList = new();
        foreach (var kv in _inputDic)
        {
            _inputList.Add(kv.Value);
        }
    }
    #endregion

    //===================================================================
    #region [ 입력 ]
    //===============================================================
    void Update()
    {
        //
        if (InputDisabled)
        {
            return;
        }

        //
        foreach (var input in _inputList)
        {
            input.Evaluate();
        }

        // todo : 호출 순서에 따라 씹힐 수도 있는 문제 해결해야함
    }


    /// <summary>
    /// 외부에서 입력에 이벤트 달아 놓을 때, 
    /// </summary>
    public BBInput GetInput(InputType type)
    {
        return _inputDic[type];
    }

    #endregion
}