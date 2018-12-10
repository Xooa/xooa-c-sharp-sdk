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
using XooaSDK.Client.Response;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Util;
using RestSharp;
using Newtonsoft.Json.Linq;
using Common.Logging;
using System.Threading.Tasks;

namespace XooaSDK.Client.Api {

    public interface IQueryApi {

        /// <summary>
        /// The query API endpoint is used for querying (reading) a blockchain ledger using smart contract function.
        /// The endpoint must call a function already defined in your smart contract app which will process the query request.
        /// The function name is part of the endpoint URL, or can be entered as the fcn parameter when testing using the Sandbox.
        /// The function arguments (number of arguments and type) is determined by the smart contract.
        /// The smart contract is responsible for validation and exception management.
        /// In case of error the smart contract is responsible for returning the proper http error code.
        /// When exception happens, and it is not caught by smart contract or if caught and no http status code is returned,
        /// the API gateway will return http-status-code 500 to the client app. 
        ///
        /// For example, if testing the sample get-set smart contract app, enter ‘get’ (without quotes) as the value for fcn. 
        /// 
        /// The response body is also determined by the smart contract app, and that’s also the reason why a consistent 
        /// response sample is unavailable for this endpoint. A success response may be either 200 or 202. 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="functionName">Function to Query.</param>
        /// <param name="args">Arguments to query.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>QueryResponse giving the payload for the argument.</returns>
        QueryResponse query(string functionName, string[] args, string timeout);

        /// <summary>
        /// The query API endpoint is used for querying (reading) a blockchain ledger using smart contract function.
        /// The endpoint must call a function already defined in your smart contract app which will process the query request.
        /// The function name is part of the endpoint URL, or can be entered as the fcn parameter when testing using the Sandbox.
        /// The function arguments (number of arguments and type) is determined by the smart contract.
        /// The smart contract is responsible for validation and exception management.
        /// In case of error the smart contract is responsible for returning the proper http error code.
        /// When exception happens, and it is not caught by smart contract or if caught and no http status code is returned,
        /// the API gateway will return http-status-code 500 to the client app. 
        ///
        /// For example, if testing the sample get-set smart contract app, enter ‘get’ (without quotes) as the value for fcn. 
        /// 
        /// The response body is also determined by the smart contract app, and that’s also the reason why a consistent 
        /// response sample is unavailable for this endpoint. A success response may be either 200 or 202. 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="functionName">Function Name to Query.</param>
        /// <param name="args">Arguments for the Query.</param>
        /// <returns>PendingTransactionResponse giving the resultId and resultUrl.</returns>
        PendingTransactionResponse queryAsync(string functionName, string[] args);
    }

    public class QueryApi : IQueryApi {

        /// <value>Rest Client Object.</value>
        private RestClient RestClient;

        /// <value>Api Token.</value>
        private string ApiToken;

        /// <summary>
        /// Apache Common Logger
        /// </summary>
        /// <value>Commons Logger.</value>
        private static readonly ILog Log = LogManager.GetLogger<Program>();

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryApi"/> class.
        /// </summary>
        /// <param name="RestClient">RestClient to connect with the API.</param>
        /// <param name="ApiToken">ApiToken to Authenticate the user.</param>
        public QueryApi(RestClient RestClient, string ApiToken) {
            this.RestClient = RestClient;
            this.ApiToken = ApiToken;
        }

        /// <summary>
        /// The query API endpoint is used for querying (reading) a blockchain ledger using smart contract function.
        /// The endpoint must call a function already defined in your smart contract app which will process the query request.
        /// The function name is part of the endpoint URL, or can be entered as the fcn parameter when testing using the Sandbox.
        /// The function arguments (number of arguments and type) is determined by the smart contract.
        /// The smart contract is responsible for validation and exception management.
        /// In case of error the smart contract is responsible for returning the proper http error code.
        /// When exception happens, and it is not caught by smart contract or if caught and no http status code is returned,
        /// the API gateway will return http-status-code 500 to the client app. 
        ///
        /// For example, if testing the sample get-set smart contract app, enter ‘get’ (without quotes) as the value for fcn. 
        /// 
        /// The response body is also determined by the smart contract app, and that’s also the reason why a consistent 
        /// response sample is unavailable for this endpoint. A success response may be either 200 or 202. 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="functionName">Function to Query.</param>
        /// <param name="args">Arguments to query.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>QueryResponse giving the payload for the argument.</returns>
        public QueryResponse query(string functionName, string[] args, string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.QUERY_URL);

            var localVarPath = XooaConstants.QUERY_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            //localVarQueryParameters.Add(new KeyValuePair<string, string>("args", args));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("fcn", functionName);

            int statusCode = 0;

            string jsonData = "[\"";

            for (int i = 0; i < args.Length; i++) {
                jsonData += args[i];

                if (i != args.Length -1) {
                    jsonData += "\", \"";
                }
            }
            jsonData += "\"]";

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.POST, localVarQueryParameters, jsonData, localVarHeaderParams, 
                    null, localVarPathParams, contentType);

                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                QueryResponse queryResponse = new QueryResponse(details["payload"].ToString());

                return queryResponse;
                
            } catch (XooaRequestTimeoutException xrte) {
                Log.Error(xrte);
                throw xrte;
            } catch (XooaApiException xae) {
                Log.Error(xae);
                throw xae;
            } catch (System.Exception e) {
                Log.Error(e);
                throw new XooaApiException(statusCode, e.Message);
            }
        }

        /// <summary>
        /// The query API endpoint is used for querying (reading) a blockchain ledger using smart contract function.
        /// The endpoint must call a function already defined in your smart contract app which will process the query request.
        /// The function name is part of the endpoint URL, or can be entered as the fcn parameter when testing using the Sandbox.
        /// The function arguments (number of arguments and type) is determined by the smart contract.
        /// The smart contract is responsible for validation and exception management.
        /// In case of error the smart contract is responsible for returning the proper http error code.
        /// When exception happens, and it is not caught by smart contract or if caught and no http status code is returned,
        /// the API gateway will return http-status-code 500 to the client app. 
        ///
        /// For example, if testing the sample get-set smart contract app, enter ‘get’ (without quotes) as the value for fcn. 
        /// 
        /// The response body is also determined by the smart contract app, and that’s also the reason why a consistent 
        /// response sample is unavailable for this endpoint. A success response may be either 200 or 202. 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="functionName">Function Name to Query.</param>
        /// <param name="args">Arguments for the Query.</param>
        /// <returns>PendingTransactionResponse giving the resultId and resultUrl.</returns>
        public PendingTransactionResponse queryAsync(string functionName, string[] args) {

            Log.Info("Invoking URL - " + XooaConstants.QUERY_URL);

            var localVarPath = XooaConstants.QUERY_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.TRUE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, "1000"));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("fcn", functionName);

            int statusCode = 0;

            string jsonData = "[\"";

            for (int i = 0; i < args.Length; i++) {
                jsonData += args[i];

                if (i != args.Length -1) {
                    jsonData += "\", \"";
                }
            }
            jsonData += "\"]";

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath, 
                    RestSharp.Method.POST, localVarQueryParameters, jsonData, localVarHeaderParams,
                    null, localVarPathParams, contentType);
                
                IRestResponse response = RestClient.ExecuteTaskAsync(request).Result;

                JObject details = XooaSDK.Client.Util.Request.getDataAsync(response);

                PendingTransactionResponse pendingTransactionResponse = new PendingTransactionResponse(
                        details["resultId"].ToString(), details["resultURL"].ToString());
                    
                return pendingTransactionResponse;
                
            } catch (XooaApiException xae) {
                Log.Error(xae);
                throw xae;
            } catch (System.Exception e) {
                Log.Error(e);
                throw new XooaApiException(statusCode, e.Message);
            }
        }
    }
}