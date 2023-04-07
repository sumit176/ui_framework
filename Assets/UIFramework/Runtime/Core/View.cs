using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    public interface IView
    {
        void Show();
        void Hide();
    }

    public class View : MonoBehaviour, IView
    {
        //Events
        public event Action OnShowEvent;
        public event Action OnHideEvent;

        //Animation data
        [Header("Animation Data")]
        [SerializeField] private AnimationSOBase animationSO;
        private object data;
        protected Action<object>[] customCallbacks;

        protected T GetData<T>()
        {
            if (data != null)
            {
                return (T)data;
            }
            return default(T);
        }

        public bool pVisible { get; private set; }

        public void SetInfo(object data)
        {
            this.data = data;
        }

        public void SetInfo(Hashtable data)
        {
            this.data = data;
        }
        public void SetCustomCallback(params Action<object>[] callbacks){
            customCallbacks = callbacks;
		}

        public virtual void Show()
        {
            gameObject.SetActive(true);
            if (animationSO != null)
            {
                animationSO.Open(this, () =>
                {
                    pVisible = true;
                    OnShowEvent?.Invoke();
                });
            }
            else
            {
                pVisible = true;
                OnShowEvent?.Invoke();
            }
            
        }

        public virtual void Hide()
        {
            if (animationSO != null)
            {
                animationSO.Close(this, () =>
                {
                    gameObject.SetActive(false);
                    pVisible = false;
                    OnHideEvent?.Invoke();
                });
            }
            else
            {
                gameObject.SetActive(false);
                pVisible = false;
                OnHideEvent?.Invoke();
            }
        }

        public virtual void Escape()
        {
            Hide();
        }
    }
}
