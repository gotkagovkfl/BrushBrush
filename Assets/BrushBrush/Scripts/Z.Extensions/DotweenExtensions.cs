using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;


public static class DotweenExtentsions
{
        public static UniTask PlayAsync(this Tween t, GameObject go, bool updateFlag = false )
        {
            return DOTween.Sequence()
            .Append(t)
            .SetUpdate(updateFlag)
            .Play()
            .ToUniTask(  tweenCancelBehaviour : TweenCancelBehaviour.Kill, cancellationToken: go.GetCancellationTokenOnDestroy());
        }
}
