/// C# SDK for Xooa
/// 
/// Copyright 2018 Xooa
///
/// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
/// in compliance with the License. You may obtain a copy of the License at:
/// http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed
/// on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License
/// for the specific language governing permissions and limitations under the License.
///
/// Author: Kavi Sarna

using System;

namespace XooaSDK.Client.Util {
    public class XooaConstants {

        public static string API_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcGlLZXkiOiJFMVpBQVNBLVBZU01WQkstS1BGM0JRUy1BMVQ1NVRFIiwiQXBpU2VjcmV0IjoibDVGS1pNanZUWHZlZkRWIiwiUGFzc3BocmFzZSI6IjMyNzNmNjg3MjE5MTM4ZjhlMmM1NzdiNzgwZmYzNjJhIiwiaWF0IjoxNTQ0NDMzNDQ4fQ.3s69b0wErmJe7LZC6zWISfbGQY4IR6gMODjPsgUYPyY";

        public static string IDENTITY_ID = "8c3d6fe6-03ec-4173-a0ce-5099f629c188";
        
        public static string CONTENT_TYPE = "application/json";
        
        public static string CURRENT_BLOCK_URL = "/block/current";
        public static string BLOCK_DATA_URL = "/block/{BlockNumber}";

        public static string TRANSACTION_URL = "/transactions/{TransactionId}";
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
        public static string TOKEN = "Bearer ";

        public static string ERROR = "error";
    }
}