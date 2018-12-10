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
using System.Collections;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json.Linq;
using XooaSDK.Client.Exception;
using Common.Logging;

namespace XooaSDK.Client.Util {

    public class Request {

        /// <summary>
        /// Apache Common Logger
        /// </summary>
        /// <value>Commons Logger.</value>
        private static readonly ILog Log = LogManager.GetLogger<Program>();

        // Creates and sets up a RestRequest prior to a call.
        public static RestRequest PrepareRequest(
            String path, RestSharp.Method method, List<KeyValuePair<String, String>> queryParams, 
            Object postBody,
            Dictionary<String, String> headerParams, Dictionary<String, String> formParams,
            Dictionary<String, String> pathParams, String contentType)
        {
            var request = new RestRequest(path, method);

            request.AddParameter("content-type", contentType);
            // add path parameter, if any
            if (pathParams != null) {
                foreach (var param in pathParams)
                    request.AddParameter(param.Key, param.Value, ParameterType.UrlSegment);
            }
            
            // add header parameter, if any
            if (headerParams != null) {
                foreach(var param in headerParams)
                    request.AddHeader(param.Key, param.Value);
            }
            
            // add query parameter, if any
            if (queryParams != null) {
                foreach(var param in queryParams)
                    request.AddQueryParameter(param.Key, param.Value);
            }
            
            // add form parameter, if any
            if (formParams != null) {
                foreach(var param in formParams)
                    request.AddParameter(param.Key, param.Value);
            }

            // http body (model or byte[]) parameter
            if (postBody != null)
            {   
                request.AddParameter(contentType, postBody, ParameterType.RequestBody);
            }

            return request;
        }


        public static JObject  GetData(IRestResponse response) {

            int statusCode = (int) response.StatusCode;
            var data = response.Content;
            Console.WriteLine(data);
            if (statusCode == 200) {

                Log.Info("Received a 200 Response from Blockchain. Processing...");

                JObject details = JObject.Parse(data);

                return details;

            } else if (statusCode == 202) {

                Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");

                JObject details = JObject.Parse(data);

                Console.WriteLine(details);

                throw new XooaRequestTimeoutException(details["resultId"].ToString(),
                        details["resultURL"].ToString());
                
            } else {

                Log.Info("Received an error response from Blockchain - " + statusCode);

                try {

                    JObject details = JObject.Parse(data);

                    throw new XooaApiException(statusCode, details["error"].ToString());

                } catch (System.Exception e) {
                    e.ToString();
                    throw new XooaApiException(statusCode, data);
                }
            }
        }

        public static JObject  getDataAsync(IRestResponse response) {

            int statusCode = (int) response.StatusCode;
            var data = response.Content;

            try {

                if (statusCode == 202) {

                    Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");

                    JObject details = JObject.Parse(data);

                    return details;

                } else {

                    Log.Info("Received an error response from Blockchain - " + statusCode);

                    JObject details = JObject.Parse(data);

                    throw new XooaApiException(statusCode, details["error"].ToString());

                }
            } catch (XooaApiException xae) {
                
                throw xae;
                
            } catch (System.Exception e) {

                e.ToString();
                throw new XooaApiException(statusCode, data);
            }
        }
    }
}