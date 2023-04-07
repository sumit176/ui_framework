using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    public class ButtonClickViewNavigation : MonoBehaviour
    {
        [SerializeField] private string m_View;
        private Button btn;

        private void Awake()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(OpenView);
        }

        private void OpenView()
        {
            Type viewType = Type.GetType("Zoolana.UI." + m_View);
            if (viewType == null)
            {
                Debug.LogError($"View type {m_View} is not available");
                return;
            }

            ViewHandler.GetView(viewType).Show();
        }
    }
}
