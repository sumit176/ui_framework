using System;
using UnityEngine;

namespace Framework.UI
{
    [CreateAssetMenu(fileName= "ScaleInOutAnimationSO", menuName = "Scriptable/ScaleInOutAnimationSO")]
    public class ScaleInOutAnimationSO : AnimationSOBase
    {
        [SerializeField] private ScaleInOutProperties properties;

        protected void Awake()
        {
            behaviour = new ScaleInOutBehaviourAnim(properties);
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

