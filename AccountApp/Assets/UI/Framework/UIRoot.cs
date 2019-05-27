using System;
using UnityEngine;

namespace UI.Framework
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Transform mCommonPanel;
        [SerializeField] private Camera mUICamera;

        
        public Transform CommonPanel => mCommonPanel;
        public Camera UICamera => mUICamera;

    }
}