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
        /// Get InvokeResponse for the function and arguments.
        /// Get payload information 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="functionName">Function to Invoke.</param>
        /// <param name="args">Arguments for the transaction.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>InvokeResponse giving the payload for the argument.</returns>
        InvokeResponse invoke(String functionName, string[] args, string timeout);

        /// <summary>
        /// Get InvokeResponse for the function and arguments.
        /// Get payload information 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="functionName">Function to Invoke.</param>
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
        /// Get InvokeResponse for the function and arguments.
        /// Get payload information 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="functionName">Function to Invoke.</param>
        /// <param name="args">Arguments for the transaction.</param>
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

            string jsonData = "{\"args\":[\"" + args[0] + "\", \"" + args[1] + "\"]}";

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.POST, localVarQueryParameters, jsonData, localVarHeaderParams,
                    null, localVarPathParams, contentType);
                
                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);

                    InvokeResponse invokeResponse = new InvokeResponse(
                        details["txId"].ToString(), details["payload"].ToString());
                    
                    return invokeResponse;
                
                } else if (statusCode == 202) {

                    Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");

                    var details = JObject.Parse(data);

                    throw new XooaRequestTimeoutException(details["resultId"].ToString(),
                        details["resultURL"].ToString());
                    
                } else {

                    Log.Info("Received an error response from Blockchain - " + statusCode);

                    try {
                        var details = JObject.Parse(data);

                        throw new XooaApiException(statusCode, details["error"].ToString());

                    } catch (System.Exception e) {
                        e.ToString();
                        throw new XooaApiException(statusCode, data);
                    }
                }
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
        /// Get InvokeResponse for the function and arguments.
        /// Get payload information 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="functionName">Function to Invoke.</param>
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

            string jsonData = "{\"args\":[\"" + args[0] + "\", \"" + args[1] + "\"]}";

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.POST, localVarQueryParameters, jsonData, localVarHeaderParams,
                    null, localVarPathParams, contentType);
                
                var response = RestClient.ExecuteTaskAsync(request).Result;
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);

                    PendingTransactionResponse pendingTransactionResponse = new PendingTransactionResponse(
                        details["resultId"].ToString(), details["resultURL"].ToString());

                    return pendingTransactionResponse;
                
                } else {
                    
                    Log.Info("Received an error response from Blockchain - " + statusCode);
                    
                    try {
                        var details = JObject.Parse(data);

                        throw new XooaApiException(statusCode, details["error"].ToString());

                    } catch (System.Exception e) {
                        e.ToString();
                        throw new XooaApiException(statusCode, data);
                    }
                }
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