using System;
using System.Collections.Generic;

/// <summary>
/// 이벤트 마킹
/// </summary>
public interface IEvent { }

/// <summary>
/// 타입을 모를 때도 접근할 수 있게 해주는 인터페이스
/// </summary>
internal interface IEventRegister
{
    int ListenerCount { get; }
    void Clear();
}

/// <summary>
/// 제네릭 홀더
/// </summary>
internal static class EventHolder<T> where T : IEvent
{
    public static Action<T> OnPublished;
    
    // 내부 클래스를 통해 중앙 매니저에 등록
    static EventHolder()
    {
        BBEventBus.RegisterHolder(new Registration());
    }

    // 실제 개수를 세어주는 구현체
    private class Registration : IEventRegister
    {
        // 델리게이트의 GetInvocationList()를 사용해 구독자 수를 확인
        public int ListenerCount => OnPublished?.GetInvocationList().Length ?? 0;
        public void Clear() => OnPublished = null;
    }

    // 정적 생성자를 강제로 호출시키기 위한 더미 메서드
    public static void Init() { }
}

/// <summary>
/// 중앙 이벤트 매니저
/// </summary>
public static class BBEventBus
{
    private static readonly List<IEventRegister> _allHolders = new List<IEventRegister>();

    //===========================================================================
    /// <summary>
    /// 활성화된 이벤트를 이벤트 목록에 추가 
    /// </summary>
    internal static void RegisterHolder(IEventRegister holder)
    {
        _allHolders.Add(holder);
    }

    /// <summary>
    /// 이벤트 구독
    /// </summary>
    public static void Subscribe<T>(Action<T> handler) where T : IEvent
    {
        EventHolder<T>.Init(); // 정적 생성자 호출 보장
        EventHolder<T>.OnPublished += handler;
    }

    /// <summary>
    /// 이벤트 구독 해제
    /// </summary>
    public static void Unsubscribe<T>(Action<T> handler) where T : IEvent
    {
        EventHolder<T>.OnPublished -= handler;
    }

    /// <summary>
    /// 이벤트 발행
    /// </summary>
    public static void Publish<T>(T eventArgs) where T : IEvent
    {
        EventHolder<T>.OnPublished?.Invoke(eventArgs);
    }

    //====================================================
    /// <summary>
    /// 이벤트 구독 개수
    /// </summary>
    public static int TotalListenerCount()
    {
        int count = 0;
        foreach (var holder in _allHolders)
        {
            count += holder.ListenerCount;
        }
        return count;
    }
}