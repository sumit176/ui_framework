using System;
using UnityEngine;

namespace Framework.UI
{
    [CreateAssetMenu(fileName= "PositionTransitionAnimationSO", menuName = "Scriptable/PositionTransitionAnimationSO")]
    public class PositionTransitionAnimationSO : AnimationSOBase
    {
        [SerializeField] private PositionTransitionProperties properties;

        protected void Awake()
        {
            behaviour = new PositionTransitionBehaviourAnim(properties);
        }

        public override void Open(View view, Action callback = null)
        {
            if (behaviour == null)
                Awake();
            base.Open(view, callback);
        }

        public override void Close(View view, Action callback = null)
        {
            if (behaviour == null)
                Awake();
            base.Close(view, callback);
        }
    }
}

