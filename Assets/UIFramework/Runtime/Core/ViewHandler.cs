using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.UI
{
    public class ViewHandler : MonoBehaviour
    {
        static ViewHandler mIntance;

        [SerializeField] private List<View> views;
        [SerializeField] private View defaultView;

        //Holds the view type info
        private Dictionary<Type, View> viewMap = new Dictionary<Type, View>();

        //holds the current stacked views
        private Stack<View> stack = new Stack<View>();

        private void Awake()
        {
            mIntance = this;
            viewMap.Clear();
            stack.Clear();
            MapViewTypes();

            if (defaultView != null)
            {
                defaultView.Show();
                stack.Push(defaultView);
            }
        }

        private void OnValidate()
        {
            views = new List<View>();
            var seneViews = FindObjectsOfType<View>(true);
            views.AddRange(seneViews);
        }

        // Adds the views to dictionary
        private void MapViewTypes()
        {
            foreach (var item in views)
            {
                if(!viewMap.ContainsKey(item.GetType()))
                    viewMap.Add(item.GetType(), item);
                else
                {
                    Debug.LogError($"View {item.GetType()} is duplicate! on the UI {item.gameObject.name}");
                }
            }
        }

        // Gets the Type of view
        public static T GetView<T>() where T : IView
        {
            if(mIntance.viewMap.ContainsKey(typeof(T)))
            {
                return (T)(object)mIntance.viewMap[typeof(T)];
            }
            else
            {
                Debug.LogError($"View {typeof(T).Name} does not exists! ");
                return default(T);
            }
        }

        // Gets the Type of view
        public static T GetView<T>(string viewName)
        {
            var view = mIntance.views.Find(ele => ele.gameObject.name.Equals(viewName));
            if (view != null)
            {
                return (T)(object)view;
            }
            else
            {
                Debug.LogError($"View {typeof(T).Name} does not exists! ");
                return default(T);
            }
        }

        // Gets the view type
        public static View GetView(Type type)
        {
            if (mIntance.viewMap.ContainsKey(type))
            {
                return mIntance.viewMap[type];
            }
            else
            {
                Debug.LogError($"View {type.Name} does not exists! ");
                return null;
            }
        }

        // Make any view visible
        public static T ShowByName<T>(string viewName, object info = default) where T : IView
        {
            var viewT = GetView<T>(viewName);
            var view = viewT as View;
            if (view != null)
            {
                view.SetInfo(info);
                view.Show();
                mIntance.stack.Push(view);
            }
            else
            {
                Debug.LogError($"View {typeof(T).Name} does not exists! ");
                return default(T);
            }
            return viewT;
        }


        // Make any view visible
        public static T Show<T>(object info = default) where T : IView
        {
            var viewT = GetView<T>();
            var view = viewT as View;
            if(view != null)
            {
                view.SetInfo(info);
                view.Show();
                mIntance.stack.Push(view);
            }
            else
            {
                Debug.LogError($"View {typeof(T).Name} does not exists! ");
                return default(T);
            }
            return viewT;
        }

        //Push any view on top of previous view, and hides the previous view
        public static T Push<T>(object info = default) where T : IView
        {
            if(mIntance.stack.Count > 0)
            {
                var top = mIntance.stack.Peek();
                top.Hide();
            }
            return Show<T>(info);
        }

        //It replaces all the view with the current view
        public static T Replace<T>(object info = default) where T : IView
        {
            ClearStacks();
            return Show<T>(info);
        }

        // Backtracks the view one by one
        public static void Back()
        {
            if(mIntance.stack.Count == 0)
            {
                if(mIntance.defaultView != null && !mIntance.defaultView.pVisible)
                {
                    mIntance.defaultView.Show();
                }
                return;
            }

            //hide top
            var top = mIntance.stack.Pop();
            top.Hide();

            // show last one
            if(mIntance.stack.Count > 0)
            {
                var view = mIntance.stack.Peek();
                view.Show();
            }
        }

        private static void ClearStacks()
        {
            while (mIntance.stack.Count > 0)
            {
                var view = mIntance.stack.Pop();
                view.Hide();
            }
        }
    }
}
