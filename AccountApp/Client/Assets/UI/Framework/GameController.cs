using UnityEngine;

namespace UI.Framework
{
    public class GameController : MonoBehaviour
    {
        
        private void Start()
        {
            UIManager.Instance.OpenUI<UILoginPanel>();
        }
    }
}