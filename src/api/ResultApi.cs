using System;
using System.Collections;
using System.Collections.Generic;
using XooaSDK.Client.Response;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Util;
using RestSharp;
using Newtonsoft.Json.Linq;
using Common.Logging;

namespace XooaSDK.Client.Api {

    public interface IResultApi {

        /// <summary>
        /// Get QueryResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>QueryResponse giving the data about the Query.</returns>
        QueryResponse getResultForQuery(string resultId, string timeout);

        /// <summary>
        /// Get InvokeResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>InvokeResponse giving the data about the Invoke request.</returns>
        InvokeResponse getResultForInvoke(string resultId, string timeout);

        /// <summary>
        /// Get IdentityResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the Identity request.</returns>
        IdentityResponse getResultForIdentity(string resultId, string timeout);

        /// <summary>
        /// Get CurrentBlockResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>CurrentBlockResponse giving the data about the CurrentBlock request.</returns>
        CurrentBlockResponse getResultForCurrentBlock(string resultId, string timeout);

        /// <summary>
        /// Get BlockResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>BlockResponse giving the data about the BlockByNumber request.</returns>
        BlockResponse getResultForBlockByNumber(string resultId, string timeout);
    }

    public class ResultApi : IResultApi {

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
        /// Initializes a new instance of the <see cref="ResultApi"/> class.
        /// </summary>
        /// <param name="RestClient">RestClient to connect with the API.</param>
        /// <param name="ApiToken">ApiToken to Authenticate the user.</param>
        public ResultApi(RestClient RestClient, string ApiToken) {
            this.RestClient = RestClient;
            this.ApiToken = ApiToken;
        }

        /// <summary>
        /// Get QueryResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>QueryResponse giving the data about the Query.</returns>
        public QueryResponse getResultForQuery(string resultId, string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.RESULT_URL);

            var localVarPath = XooaConstants.RESULT_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("ResultId", resultId);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath, 
                    RestSharp.Method.GET, localVarQueryParameters, null, localVarHeaderParams, 
                    null, localVarPathParams, contentType);
                
                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);
                    var payload = details["payload"];

                    QueryResponse queryResponse = new QueryResponse(payload["payload"].ToString());

                    return queryResponse;
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

                    } catch(System.Exception e) {
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
        /// Get InvokeResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>InvokeResponse giving the data about the Invoke request.</returns>
        public InvokeResponse getResultForInvoke(string resultId, string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.RESULT_URL);

            var localVarPath = XooaConstants.RESULT_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("ResultId", resultId);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.GET, localVarQueryParameters, null, localVarHeaderParams,
                    null, localVarPathParams, contentType);
                
                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);
                    var payload = details["payload"];

                    InvokeResponse invokeResponse = new InvokeResponse(payload["txId"].ToString(),
                        payload["payload"].ToString());
                    
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
                    
                    } catch(System.Exception e) {

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
        /// Get IdentityResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the Identity request.</returns>
        public IdentityResponse getResultForIdentity(string resultId, string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.RESULT_URL);

            var localVarPath = XooaConstants.RESULT_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("ResultId", resultId);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.GET, localVarQueryParameters, null, localVarHeaderParams,
                    null, localVarPathParams, contentType);

                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);
                    var payload = details["payload"];
                    var Attrs = payload["Attrs"];

                    List<attrs> attributes = new List<attrs>();

                    foreach(var attrObject in Attrs) {

                        attrs attr = new attrs(attrObject["name"].ToString(), 
                            attrObject["value"].ToString(), (bool) attrObject["ecert"]);

                        attributes.Add(attr);
                    }

                    IdentityResponse identityResponse = new IdentityResponse(
                        payload["IdentityName"].ToString(),
                        payload["Access"].ToString(),
                        (bool) payload["canManageIdentities"],
                        payload["createdAt"].ToString(),
                        payload["ApiToken"].ToString(),
                        payload["Id"].ToString(),
                        attributes);
                    
                    return identityResponse;

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
        /// Get CurrentBlockResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>CurrentBlockResponse giving the data about the CurrentBlock request.</returns>
        public CurrentBlockResponse getResultForCurrentBlock(string resultId, string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.RESULT_URL);

            var localVarPath = XooaConstants.RESULT_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("ResultId", resultId);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.GET, localVarQueryParameters, null, localVarHeaderParams,
                    null, localVarPathParams, contentType);
                
                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);
                    var payload = details["payload"];

                    CurrentBlockResponse currentBlockResponse = new CurrentBlockResponse(
                        payload["currentBlockHash"].ToString(),
                        payload["previousBlockHash"].ToString(),
                        (int) payload["blockNumber"]);

                    return currentBlockResponse;
                    
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
            } catch (XooaApiException xae) {
                Log.Error(xae);
                throw xae;
            } catch(XooaRequestTimeoutException xrte) {
                Log.Error(xrte);
                throw xrte;
            } catch(System.Exception e) {
                Log.Error(e);
                throw new XooaApiException(statusCode, e.Message);
            }
        }

        /// <summary>
        /// Get BlockResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>BlockResponse giving the data about the BlockByNumber request.</returns>
        public BlockResponse getResultForBlockByNumber(string resultId, string timeout) {

            Log.Info("Invoking URL - " + XooaConstants.RESULT_URL);

            var localVarPath = XooaConstants.RESULT_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("ResultId", resultId);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.GET, localVarQueryParameters, null, localVarHeaderParams,
                    null, localVarPathParams, contentType);

                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);
                    var payload = details["payload"];

                    BlockResponse blockResponse = new BlockResponse(
                        payload["previous_hash"].ToString(),
                        payload["data_hash"].ToString(),
                        (int) payload["blockNumber"],
                        (int) payload["numberOfTransactions"]);
                    
                    return blockResponse;

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
            } catch(System.Exception e) {
                Log.Error(e);
                throw new XooaApiException(statusCode, e.Message);
            }
        }
    }
}