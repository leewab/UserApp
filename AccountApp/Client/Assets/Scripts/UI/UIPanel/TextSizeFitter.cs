using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanel
{
    public class TextSizeFitter : MonoBehaviour
    {
        [SerializeField] private Text father;
        [SerializeField] private Image bg;
        [SerializeField] private Text child;

        public void SetContent(string content)
        {
            father.text = content;
            child.text = content;
        }

        public void SetBgColor(Color color)
        {
            bg.color = color;
        }
    }
}