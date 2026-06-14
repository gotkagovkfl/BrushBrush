using UnityEngine;

/// <summary>
/// '현재 프레임'의 이동 수정 (이동 입력, 스킬사용, cc기등으로 인한 강제 이동) - transform 수정 sys에 전달
/// </summary>
public struct EntityPositionModifier
{
    public Vector3 Origin { get; private set; }  // 시작 지점
    public Vector3 Dest { get; private set; } // 목표 지점
    public int Priority { get; private set; }    // 적용 우선순위
    public readonly bool IsValid;    // 사용가능한 데이터

    public EntityPositionModifier(Vector3 origin, Vector3 dest, int priority)
    {
        Origin = origin;
        Dest = dest;
        Priority = priority;
        IsValid = true;
    }

    public static readonly EntityPositionModifier None = new(); // IsValid 가 false 여서 사용하지 못하는 값
}
