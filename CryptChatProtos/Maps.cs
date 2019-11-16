using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CryptChatProtos
{
    class Maps
    {
        public static readonly Dictionary<int, string> RequestTypeMap = new Dictionary<int, string>()
        {
            { -1, "Error" },
            {  1, "MessageRequest" },
            {  2, "SendMessageRequest" },
            {  3, "EditMessageRequest" },
            {  4, "DeleteMessageRequest" },
            {  5, "QueueRequest" },
            {  6, "ChatRequest" },
            {  7, "ChatListRequest" },
            {  8, "NewRequest" },
            {  9, "SaltRequest" },
            { 10, "LoginRequest" },
            { 11, "UserRequest" },
            { 12, "RegisterRequest" },
            { 13, "ChangePasswordRequest" }
        };
        public static readonly Dictionary<int, string> ResponseTypeMap = new Dictionary<int, string>()
        {
            { -1, "Error" },
            {  1, "MessageResponse" },
            {  2, "MessageListResponse" },
            {  3, "SendMessageResponse" },
            {  4, "EditMessageResponse" },
            {  5, "DeleteMessageResponse" },
            {  6, "QueueResponse" },
            {  7, "ChatResponse" },
            {  8, "ChatListResponse" },
            {  9, "SaltResponse" },
            { 10, "LoginResponse" },
            { 11, "UserResponse" },
            { 12, "RegisterResponse" },
            { 13, "ChangePasswordResponse" }
        };

        public static string GetResponseType(int type)
        {
            string value = string.Empty;
            bool success = ResponseTypeMap.TryGetValue(type, out value);
            return value;
        }

        public static int GetResponseCode(string type)
        {
            int code = -1;
            if (ResponseTypeMap.ContainsValue(type))
            {
                code = ResponseTypeMap.First(x => x.Value == type).Key;
            }
            return code;
        }
        public static string GetRequestType(int type)
        {
            string value = string.Empty;
            bool success = RequestTypeMap.TryGetValue(type, out value);
            return value;
        }

        public static int GetRequestCode(string type)
        {
            int code = -1;
            if (RequestTypeMap.ContainsValue(type))
            {
                code = RequestTypeMap.First(x => x.Value == type).Key;
            }
            return code;
        }

    }
}
