using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Framework.UI
{
    public enum TransitionType
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }
    [Serializable]
    public struct PositionTransitionProperties
    {
        public TransitionType transition;
        public Vector2 InPosition => Vector2.zero;
        public Vector2 OutPosition => GetOutPosition();
        public float Duration;

        private Vector2 GetOutPosition()
        {
            //TODO::Change screen width and Height to Canvas.width and height
            if (transition == TransitionType.LeftToRight)
                return new Vector2(-Screen.width, 0);
            else if (transition == TransitionType.RightToLeft)
                return new Vector2(Screen.width, 0);
            else if (transition == TransitionType.TopToBottom)
                return new Vector2(0, Screen.height);
            else
                return new Vector2(0, -Screen.height);
        }
    }

    public class PositionTransitionBehaviourAnim : IAnimBehaviour
    {
        private readonly PositionTransitionProperties properties;

        public PositionTransitionBehaviourAnim(PositionTransitionProperties properties)
        {
            this.properties = properties;
        }

        public void In(IView view, Action callback)
        {
            Transform t = (view as MonoBehaviour).transform;

            t.DOLocalMove(properties.OutPosition, 0f);
            t.DOLocalMove(properties.InPosition, properties.Duration).SetEase(Ease.OutBack).
                OnComplete(() => { callback?.Invoke(); });
        }

        public void Out(IView view, Action callback)
        {
            (view as MonoBehaviour).transform.DOLocalMove(properties.OutPosition, properties.Duration).SetEase(Ease.InBack)
                .OnComplete(() => { callback?.Invoke(); });
        }
    }
}
