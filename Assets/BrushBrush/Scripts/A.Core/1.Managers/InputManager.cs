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

    // 이동 관련
    public Vector2 LastMoveInputVector { get; private set; } // 움직임 벡터 

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
        foreach (var input in _inputList)
        {
            input.Evaluate();
        }

        //
        MakeMoveInputVector(); // 움직임 벡터 생성
    }


    /// <summary>
    /// 외부에서 입력에 이벤트 달아 놓을 때, 
    /// </summary>
    public BBInput GetInput(InputType type)
    {
        return _inputDic[type];
    }

    //================================================================
    /// <summary>
    /// 이동 관련 인풋 감지 (캐릭터 이동, ui 네비게이션 등)
    /// </summary>
    void MakeMoveInputVector()
    {
        // 값 초기화 - 입력이 없다면 (0,0)
        int hAxis = 0;  // 좌우
        int vAxis = 0;  // 상하

        // 인풋 감지하여 값 생성
        if (GetInput(InputType.MOVE_L).OnKey) hAxis -= 1;
        if (GetInput(InputType.MOVE_R).OnKey) hAxis += 1;

        if (GetInput(InputType.MOVE_U).OnKey) vAxis += 1;
        if (GetInput(InputType.MOVE_D).OnKey) vAxis -= 1;

        // 벡터 갱신
        LastMoveInputVector = new Vector2(hAxis, vAxis);
    }


    #endregion
}