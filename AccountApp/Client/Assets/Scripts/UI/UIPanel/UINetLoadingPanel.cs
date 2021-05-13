using UI.Framework;
using UnityEngine;

namespace UI.UIPanel
{
    public class UINetLoadingPanel : BaseUI
    {
        [SerializeField] private Transform trans_Bg;

        private bool isStart = false;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            isStart = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            isStart = false;
        }

        private void Update()
        {
            if (trans_Bg == null) return;
            trans_Bg.Rotate(Vector3.back, Space.Self);
        }
    }
}