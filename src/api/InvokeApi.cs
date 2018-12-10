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
using XooaSDK.Client.Exception;
using XooaSDK.Client.Response;
using XooaSDK.Client.Util;
using RestSharp;
using Newtonsoft.Json.Linq;
using Common.Logging;

namespace XooaSDK.Client.Api {

    public interface IInvokeApi {
        
        /// <summary>
        /// The invoke API endpoint is used for submitting transaction for processing by the blockchain smart contract app 
        /// when the transaction payload need to be persisted into the Ledger (new block is mined).
        /// The endpoint must call a function already defined in your smart contract app which will process the invoke request.
        /// The function name is part of the endpoint URL, or can be entered as the fcn parameter when testing using the Sandbox.
        /// For example, if testing the sample get-set smart contract app, use ‘set’ (without quotes) as the value for fcn. 
        /// The function arguments (number of arguments and type) is determined by the smart contract.
        /// The smart contract is also responsible for arguments validation and exception management.
        /// In case of error the smart contract is responsible for returning the proper http error code.
        /// When exception happens, and it is not caught by smart contract or if caught and no http status code is returned,
        /// the API gateway will return http-status-code 500 to the client app.
        /// 
        /// The payload of Invoke Transaction Response in case of final response is determined by the smart contract app.
        /// 
        /// A success response may be either 200 or 202.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="functionName">Function Name to invoke.</param>
        /// <param name="args">Arguments to invoke in transaction.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>InvokeResponse giving the payload for the argument.</returns>
        InvokeResponse invoke(String functionName, string[] args, string timeout);

        /// <summary>
        /// The invoke API endpoint is used for submitting transaction for processing by the blockchain smart contract app 
        /// when the transaction payload need to be persisted into the Ledger (new block is mined).
        /// The endpoint must call a function already defined in your smart contract app which will process the invoke request.
        /// The function name is part of the endpoint URL, or can be entered as the fcn parameter when testing using the Sandbox.
        /// For example, if testing the sample get-set smart contract app, use ‘set’ (without quotes) as the value for fcn. 
        /// The function arguments (number of arguments and type) is determined by the smart contract.
        /// The smart contract is also responsible for arguments validation and exception management.
        /// In case of error the smart contract is responsible for returning the proper http error code.
        /// When exception happens, and it is not caught by smart contract or if caught and no http status code is returned,
        /// the API gateway will return http-status-code 500 to the client app.
        /// 
        /// The payload of Invoke Transaction Response in case of final response is determined by the smart contract app.
        /// 
        /// A success response may be either 200 or 202.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="functionName">Function Name to invoke.</param>
        /// <param name="args">Arguments for the transaction.</param>
        /// <returns>PendingTransactionResponse giving the resultId and resultUrl.</returns>
        PendingTransactionResponse invokeAsync(String functionName, string[] args);
    }

    public class InvokeApi : IInvokeApi {

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
        /// Initializes a new instance of the <see cref="InvokeApi"/> class.
        /// </summary>
        /// <param name="RestClient">RestClient to connect with the API.</param>
        /// <param name="ApiToken">ApiToken to Authenticate the user.</param>
        public InvokeApi(RestClient RestClient, string ApiToken) {
            this.RestClient = RestClient;
            this.ApiToken = ApiToken;
        }

        /// <summary>
        /// The invoke API endpoint is used for submitting transaction for processing by the blockchain smart contract app 
        /// when the transaction payload need to be persisted into the Ledger (new block is mined).
        /// The endpoint must call a function already defined in your smart contract app which will process the invoke request.
        /// The function name is part of the endpoint URL, or can be entered as the fcn parameter when testing using the Sandbox.
        /// For example, if testing the sample get-set smart contract app, use ‘set’ (without quotes) as the value for fcn. 
        /// The function arguments (number of arguments and type) is determined by the smart contract.
        /// The smart contract is also responsible for arguments validation and exception management.
        /// In case of error the smart contract is responsible for returning the proper http error code.
        /// When exception happens, and it is not caught by smart contract or if caught and no http status code is returned,
        /// the API gateway will return http-status-code 500 to the client app.
        /// 
        /// The payload of Invoke Transaction Response in case of final response is determined by the smart contract app.
        /// 
        /// A success response may be either 200 or 202.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="functionName">Function Name to invoke.</param>
        /// <param name="args">Arguments to invoke in transaction.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>InvokeResponse giving the payload for the argument.</returns>
        public InvokeResponse invoke(string functionName, string[] args = null, string timeout = "3000") {
            
            Log.Info("Invoking URL - " + XooaConstants.INVOKE_URL);

            if (args.Length != 2)
                throw new ArgumentException("Incorrect arguments. Expecting a key and a value.");
            
            var localVarPath = XooaConstants.INVOKE_URL;
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

            // string jsonData = "[";

            // foreach ( string argument in args) {
            //     jsonData += "\"" + argument + "\", ";
            // }

            // jsonData = jsonData.Substring(0, jsonData.Length-2) + "]";

            string jsonData = "[\"";

            for (int i = 0; i < args.Length; i++) {
                jsonData += args[i];

                if (i != args.Length -1) {
                    jsonData += "\", \"";
                }
            }
            jsonData += "\"]";

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.POST, localVarQueryParameters, jsonData, localVarHeaderParams,
                    null, localVarPathParams, contentType);
                
                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                InvokeResponse invokeResponse = new InvokeResponse(
                    details["txId"].ToString(), details["payload"].ToString());
                    
                return invokeResponse;
                
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
        /// The invoke API endpoint is used for submitting transaction for processing by the blockchain smart contract app 
        /// when the transaction payload need to be persisted into the Ledger (new block is mined).
        /// The endpoint must call a function already defined in your smart contract app which will process the invoke request.
        /// The function name is part of the endpoint URL, or can be entered as the fcn parameter when testing using the Sandbox.
        /// For example, if testing the sample get-set smart contract app, use ‘set’ (without quotes) as the value for fcn. 
        /// The function arguments (number of arguments and type) is determined by the smart contract.
        /// The smart contract is also responsible for arguments validation and exception management.
        /// In case of error the smart contract is responsible for returning the proper http error code.
        /// When exception happens, and it is not caught by smart contract or if caught and no http status code is returned,
        /// the API gateway will return http-status-code 500 to the client app.
        /// 
        /// The payload of Invoke Transaction Response in case of final response is determined by the smart contract app.
        /// 
        /// A success response may be either 200 or 202.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="functionName">Function Name to invoke.</param>
        /// <param name="args">Arguments for the transaction.</param>
        /// <returns>PendingTransactionResponse giving the resultId and resultUrl.</returns>
        public PendingTransactionResponse invokeAsync(string functionName, string[] args = null) {
            
            Log.Info("Invoking URL - " + XooaConstants.INVOKE_URL);

            if (args.Length != 2)
                throw new ArgumentException("Incorrect arguments. Expecting a key and a value.");
            
            var localVarPath = XooaConstants.INVOKE_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.TRUE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, "3000"));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("fcn", functionName);

            // string jsonData = "[";

            // foreach ( string argument in args) {
            //     jsonData += "\"" + argument + "\", ";
            // }

            // jsonData = jsonData.Substring(0, jsonData.Length-2) + "]";

            string jsonData = "[\"";

            for (int i = 0; i < args.Length; i++) {
                jsonData += args[i];

                if (i != args.Length -1) {
                    jsonData += "\", \"";
                }
            }
            jsonData += "\"]";

            int statusCode = 0;

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