using System;
using UnityEngine;

namespace Framework.UI
{
    public class AnimationSOBase : ScriptableObject
    {
        protected IAnimBehaviour behaviour;

        public virtual void Open(View view, Action callback = null)
        {
            behaviour.In(view, callback);
        }

        public virtual void Close(View view, Action callback = null)
        {
            behaviour.Out(view, callback);
        }
    }
}
