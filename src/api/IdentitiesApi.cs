using System;
using System.Collections;
using System.Collections.Generic;
using XooaSDK.Client.Util;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Response;
using XooaSDK.Client.Request;
using RestSharp;
using Newtonsoft.Json.Linq;
using Common.Logging;

namespace XooaSDK.Client.Api {

    public interface IIdentitiesApi {
        
        /// <summary>
        /// Get the current identity data with which the user is logged in.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the current Identity.</returns>
        IdentityResponse currentIdentity(string timeout);

        /// <summary>
        /// Get the list of all the identities available.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>List of IdentityResponse giving the data about all the Identities.</returns>
        List<IdentityResponse> getIdentities(string timeout);

        /// <summary>
        /// Enroll a new Identity with the app.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="identityRequest">Identity to enroll.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the new Identity.</returns>
        IdentityResponse enrollIdentity(IdentityRequest identityRequest, string timeout);

        /// <summary>
        /// Enroll a new Identity with the app in async mode.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="IdentityRequest">Identity to enroll.</param>
        /// <returns>PendingTransactionResponse giving the resultId of the pending transaction.</returns>
        PendingTransactionResponse enrollIdentityAsync(IdentityRequest identityRequest);

        /// <summary>
        /// Regenerate the API token for the identity Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="identityId">Identity Id to regenerate API Token for.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the new Api Token for the Identity.</returns>
        IdentityResponse regenerateIdentityApiToken(string identityId, string timeout);

        /// <summary>
        /// Get the identity data.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityId">Identity Id to get data about.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the Identity.</returns>
        IdentityResponse getIdentity(string identityId, string timeout);

        /// <summary>
        /// Delete the identity.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityId">Identity Id to delete.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>String message telling the identity is deleted.</returns>
        string deleteIdentity(string identityId, string timeout);
    }

    public class IdentitiesApi : IIdentitiesApi {
        
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
        /// Initializes a new instance of the <see cref="IdentitiesApi"/> class.
        /// </summary>
        /// <param name="RestClient">RestClient to connect with the API.</param>
        /// <param name="ApiToken">ApiToken to Authenticate the user.</param>
        public IdentitiesApi(RestClient RestClient, string ApiToken) {
            this.RestClient = RestClient;
            this.ApiToken = ApiToken;
        }

        /// <summary>
        /// Get the current identity data with which the user is logged in.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the current Identity.</returns>
        public IdentityResponse currentIdentity(string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.CURRENT_IDENTITY_URL);

            var localVarPath = XooaConstants.CURRENT_IDENTITY_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.GET, localVarQueryParameters, null, localVarHeaderParams,
                    null, null, contentType);

                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");
                    
                    var details = JObject.Parse(data);
                    var Attrs = details["Attrs"];

                    List<attrs> attributes = new List<attrs>();

                    foreach(var attrObject in Attrs) {

                        attrs attr = new attrs(attrObject["name"].ToString(),
                            attrObject["value"].ToString(), (bool) attrObject["ecert"]);
                        
                        attributes.Add(attr);
                    }

                    IdentityResponse identityResponse = new IdentityResponse(
                        details["IdentityName"].ToString(), details["Access"].ToString(),
                        (bool) details["canManageIdentities"], details["createdAt"].ToString(),
                        details["ApiToken"].ToString(), details["Id"].ToString(),
                        attributes);
                    
                    return identityResponse;

                } else if (statusCode == 202) {

                    Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");

                    var details = JObject.Parse(data);

                    throw new XooaRequestTimeoutException(details["resultId"].ToString(), details["resultURL"].ToString());

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
        /// Get the list of all the identities available.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>List of IdentityResponse giving the data about all the Identities.</returns>
        public List<IdentityResponse> getIdentities(string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.IDENTITIES_URL);
            
            var localVarPath = XooaConstants.IDENTITIES_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.GET, localVarQueryParameters, null, localVarHeaderParams,
                    null, null, contentType);
                
                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var identities = JArray.Parse(data);

                    List<IdentityResponse> identityResponses = new List<IdentityResponse>();

                    foreach (var identity in identities) {

                        IdentityResponse identityResponse = new IdentityResponse(
                            identity["IdentityName"].ToString(),
                            (bool) identity["canManageIdentities"],
                            identity["createdAt"].ToString(),
                            identity["ApiToken"].ToString(),
                            identity["Id"].ToString());
                        
                        identityResponses.Add(identityResponse);
                    }

                    return identityResponses;
                
                } else if (statusCode == 202) {

                    Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");

                    var details = JObject.Parse(data);

                    throw new XooaRequestTimeoutException(details["resultId"].ToString(), details["resultURL"].ToString());

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
        /// Enroll a new Identity with the app.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityRequest">Identity to enroll.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the new Identity.</returns>
        public IdentityResponse enrollIdentity(IdentityRequest identityRequest, string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.IDENTITIES_URL);
            
            var localVarPath = XooaConstants.IDENTITIES_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            string jsonBody = identityRequest.toString();

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.POST, localVarQueryParameters, jsonBody, localVarHeaderParams,
                    null, null, contentType);
                
                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");
                    
                    var details = JObject.Parse(data);
                    var Attrs = details["Attrs"];

                    List<attrs> attributes = new List<attrs>();

                    foreach(var attrObject in Attrs) {

                        attrs attr = new attrs(attrObject["name"].ToString(),
                            attrObject["value"].ToString(), (bool) attrObject["ecert"]);

                        attributes.Add(attr);
                    }

                    IdentityResponse identityResponse = new IdentityResponse(
                        details["IdentityName"].ToString(),
                        details["Access"].ToString(),
                        (bool) details["canManageIdentities"],
                        details["createdAt"].ToString(),
                        details["ApiToken"].ToString(),
                        details["Id"].ToString(),
                        attributes);
                    
                    return identityResponse;
                
                } else if (statusCode == 202) {

                    Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");

                    var details = JObject.Parse(data);

                    throw new XooaRequestTimeoutException(details["resultId"].ToString(), details["resultURL"].ToString());

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
        /// Enroll a new Identity with the app in async mode.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="IdentityId">Identity to enroll.</param>
        /// <returns>PendingTransactionResponse giving the resultId of the pending transaction.</returns>
        public PendingTransactionResponse enrollIdentityAsync(IdentityRequest identityRequest) {

            Log.Info("Invoking URL - " + XooaConstants.IDENTITIES_URL);
            
            var localVarPath = XooaConstants.IDENTITIES_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            //localVarQueryParameters.Add(new KeyValuePair<string, string>("args", args));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.TRUE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, "1000"));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            // var localVarPathParams = new Dictionary<string, string>();
            //localVarPathParams.Add("fcn", functionName);

            string jsonBody = identityRequest.toString();

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.POST, localVarQueryParameters, jsonBody, localVarHeaderParams,
                    null, null, contentType);
                
                var response = RestClient.Execute(request);
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

        /// <summary>
        /// Regenerate the API token for the identity Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityId">Identity Id to regenerate API Token for.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the new Api Token for the Identity.</returns>
        public IdentityResponse regenerateIdentityApiToken(string identityId, string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.API_TOKEN_REGENERATE_URL);
            
            var localVarPath = XooaConstants.API_TOKEN_REGENERATE_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("IdentityId", identityId);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.POST, localVarQueryParameters, null, localVarHeaderParams,
                    null, localVarPathParams, contentType);

                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);
                    var Attrs = details["Attrs"];

                    List<attrs> attributes = new List<attrs>();

                    foreach(var attrObject in Attrs) {

                        attrs attr = new attrs(attrObject["name"].ToString(), 
                            attrObject["value"].ToString(), (bool) attrObject["ecert"]);

                        attributes.Add(attr);
                    }

                    IdentityResponse identityResponse = new IdentityResponse(
                        details["IdentityName"].ToString(), details["Access"].ToString(),
                        (bool) details["canManageIdentities"], details["createdAt"].ToString(),
                        details["ApiToken"].ToString(), details["Id"].ToString(),
                        attributes);
                    
                    return identityResponse;
                
                } else if (statusCode == 202) {

                    Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");

                    var details = JObject.Parse(data);

                    throw new XooaRequestTimeoutException(details["resultId"].ToString(), details["resultURL"].ToString());

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
        /// Get the identity data.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityId">Identity Id to get data for.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the Identity.</returns>
        public IdentityResponse getIdentity(string identityId, string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.IDENTITY_URL);
            
            var localVarPath = XooaConstants.IDENTITY_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("IdentityId", identityId);

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
                    var Attrs = details["Attrs"];

                    List<attrs> attributes = new List<attrs>();

                    foreach(var attrObject in Attrs) {

                        attrs attr = new attrs(attrObject["name"].ToString(),
                            attrObject["value"].ToString(), (bool) attrObject["ecert"]);
                        
                        attributes.Add(attr);
                    }

                    IdentityResponse identityResponse = new IdentityResponse(
                        details["IdentityName"].ToString(), details["Access"].ToString(),
                        (bool) details["canManageIdentities"], details["createdAt"].ToString(),
                        details["ApiToken"].ToString(), details["Id"].ToString(),
                        attributes);

                    return identityResponse;

                } else if (statusCode == 202) {

                    Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");

                    var details = JObject.Parse(data);

                    throw new XooaRequestTimeoutException(details["resultId"].ToString(), details["resultURL"].ToString());

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
        /// Delete the identity.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityId">Identity Id to delete.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>String message telling the identity is deleted.</returns>
        public string deleteIdentity(string identityId, string timeout = "3000") {
            
            Log.Info("Invoking URL - " + XooaConstants.IDENTITY_URL);

            var localVarPath = XooaConstants.IDENTITY_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("IdentityId", identityId);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.DELETE, localVarQueryParameters, null, localVarHeaderParams,
                    null, localVarPathParams, contentType);
                
                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);

                    return details["deleted"].ToString();

                } else if (statusCode == 202) {

                    Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");

                    var details = JObject.Parse(data);

                    throw new XooaRequestTimeoutException(details["resultId"].ToString(), details["resultURL"].ToString());

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
    }
}