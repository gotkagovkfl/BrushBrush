using UnityEngine;

/// <summary>
/// 시스템에 의해 설정된 엔티티의 현재 상태 정보
/// </summary>
public class EntityState
{
    public bool CanControlMovement { get; private set; } = true;    // 스스로 움직임 제어가 가능한지
}
