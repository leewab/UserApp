using UnityEngine;

namespace UI.Framework
{
    public class Singleton<T> where T : class, new()
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }

        public virtual void OnSingletonInit()
        {
            Debug.Log("----------SingletonInit----------");
        }

        public virtual void OnSingletonDispose()
        {
            Debug.Log("----------SingletonDestory----------");
        }
    }
}