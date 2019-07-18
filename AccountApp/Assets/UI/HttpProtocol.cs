namespace UI
{
    public class HttpProtocol
    {
        public enum ActionSort
        {
            C2S_START = 0,
            C2S_END = 1000,
            
            S2C_START = 1001,
            S2C_END = 2000
        }
        
        public enum C2S_Action
        {
            START = ActionSort.C2S_START,
            
            END = ActionSort.C2S_END
        }
        
        public enum S2C_Action
        {
            START = ActionSort.S2C_START,
            
            LOGIN_Start = 1,
            LOGIN_End = 20,
            
            REGISTER_Start = 21,
            REGISTER_End = 40,
            
            
            
            END = ActionSort.S2C_END
        }
        
        public enum LoginState
        {
            START = S2C_Action.LOGIN_Start,          //1
            LOGIN_LoginSuccess = 2,
            LOGIN_LoginFail = 3,
            LOGIN_UsernameError = 4,
            LOGIN_PassworkError = 5,
            LOGIN_None = 19,
            END = S2C_Action.LOGIN_End,              //20
        }
        
        public enum RegisterState
        {
            START = S2C_Action.REGISTER_Start,          //21
            REGISTER_RegisterSuccess = 22,
            REGISTER_RegisterFail = 23,
            REGISTER_HasUsername = 24,
            
            END = S2C_Action.REGISTER_End,              //40
        }
    }
}