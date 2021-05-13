using Framework.Network.HttpMoudle;
using UI.Framework;

namespace UI.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        /// <summary>
        /// HttpHandler
        /// </summary>
        private HttpHandler httpHandler = null;
        public HttpHandler HttpHandler
        {
            get => httpHandler;
        }

        /// <summary>
        /// ProtocolHandler
        /// </summary>
        private ProtocolHandler protocolHandler = null;

        public ProtocolHandler ProtocolHandler
        {
            get => protocolHandler;
        }

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            InitManagers();
        }

        public override void OnSingletonDispose()
        {
            base.OnSingletonDispose();
            DisposeManager();
        }

        /// <summary>
        /// 初始化所有的Manager
        /// </summary>
        private void InitManagers()
        {
            if(httpHandler == null) httpHandler = new HttpHandler();
            if(protocolHandler == null) protocolHandler = new ProtocolHandler();
        }

        /// <summary>
        /// 释放所有的Manager
        /// </summary>
        private void DisposeManager()
        {
            if (httpHandler != null)
            {
                httpHandler.Dispose();
                httpHandler = null;
            }

            if (protocolHandler != null)
            {
                protocolHandler.Dispose();
                protocolHandler = null;
            }
        }
    }
}