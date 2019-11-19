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

        public static readonly List<int> MessageRange = new List<int> { 1, 2, 3, 4, 5 };
        public static readonly List<int> ChatRange = new List<int> { 6, 7, 8, 9 };
        public static readonly List<int> AuthRange = new List<int> { 10, 11, 12 };
        public static readonly List<int> AccountRange = new List<int> { 13 };

        public static string GetRangeResult(int type)
        {
            if (MessageRange.Contains(type)) { return "Message"; }
            if (ChatRange.Contains(type)) { return "Chat"; }
            if (AuthRange.Contains(type)) { return "Auth"; }
            if (AccountRange.Contains(type)) { return "AccountRange"; }
            return "";
        }

        public static string GetResponseType(int type)
        {
            ResponseTypeMap.TryGetValue(type, out string value);
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
            RequestTypeMap.TryGetValue(type, out string value);
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
