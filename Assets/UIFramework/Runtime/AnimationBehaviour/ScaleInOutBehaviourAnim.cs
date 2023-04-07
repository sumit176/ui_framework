using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Framework.UI
{
    [Serializable]
    public struct ScaleInOutProperties
    {
        public Vector2 InScale;
        public Vector2 OutScale;
        public float Duration;
    }

    public class ScaleInOutBehaviourAnim : IAnimBehaviour
    {
        private readonly ScaleInOutProperties properties;

        public ScaleInOutBehaviourAnim(ScaleInOutProperties properties)
        {
            this.properties = properties;
        }

        public void In(IView view, Action callback)
        {
            Transform t = (view as MonoBehaviour).transform;

            t.DOScale(properties.OutScale, 0f);
            t.DOScale(properties.InScale, properties.Duration).SetEase(Ease.OutBack).
                OnComplete(() => { callback?.Invoke(); });
        }

        public void Out(IView view, Action callback)
        {
            (view as MonoBehaviour).transform.DOScale(properties.OutScale, properties.Duration).SetEase(Ease.InBack)
                .OnComplete(() => { callback?.Invoke(); });
        }
    }
}
