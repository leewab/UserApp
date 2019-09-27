using Framework.Network.HttpMoudle;
using UI.Framework;

namespace UI
{
    public class GameManager : Singleton<GameManager>
    {
        /// <summary>
        /// HttpHandler
        /// </summary>
        private HttpHandler httpHandler = null;

        public HttpHandler HttpHandler
        {
            get => httpHandler ?? (httpHandler = new HttpHandler());
        }
        
        
    }
}