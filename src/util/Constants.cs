using System;

namespace XooaSDK.Client.Util {
    public class XooaConstants {

        public static string API_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcGlLZXkiOiJHMkNaOVNDLVkzMU0yMVQtUUZaVldWNS1IUEYyOEc5IiwiQXBpU2VjcmV0IjoiQVhDdWZMbnZma1pQRGN6IiwiUGFzc3BocmFzZSI6ImU5NjY1ZWJiMGY5NjA1OTIyNDA4ZjlkZDk2ZWVjNDdjIiwiaWF0IjoxNTQxMDU3NTA1fQ._xSDpPcXx_DD_BLCCicTlVQDjObV6ete0fyBV0om8OA";

        public static string IDENTITY_ID = "8c3d6fe6-03ec-4173-a0ce-5099f629c188";
        
        public static string CONTENT_TYPE = "application/json";
        
        public static string CURRENT_BLOCK_URL = "/block/current";
        public static string BLOCK_DATA_URL = "/block/{BlockNumber}";
        public static string CURRENT_IDENTITY_URL = "/identities/me";
        public static string IDENTITIES_URL = "/identities/";
        public static string API_TOKEN_REGENERATE_URL = "/identities/{IdentityId}/regeneratetoken";
        public static string IDENTITY_URL = "/identities/{IdentityId}";
        public static string INVOKE_URL = "/invoke/{fcn}";
        public static string QUERY_URL = "/query/{fcn}";
        public static string RESULT_URL = "/results/{ResultId}";

        public static string ACCEPT = "Accept";
        public static string AUTHORIZATION = "Authorization";
        public static string ASYNC = "async";
        public static string FALSE = "false";
        public static string TRUE = "true";
        public static string TIMEOUT = "timeout";
        public static string TOKEN = "Token ";

        public static string ERROR = "error";
    }
}