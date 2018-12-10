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
        /// This endpoint returns authenticated identity information
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the current Identity.</returns>
        IdentityResponse currentIdentity(string timeout);

        /// <summary>
        /// Get all identities from the identity registry
        /// Required permission: manage identities (canManageIdentities=true)
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>List of IdentityResponse giving the data about all the Identities.</returns>
        List<IdentityResponse> getIdentities(string timeout);

        /// <summary>
        /// The Enroll identity endpoint is used to enroll new identities for the smart contract app. 
        /// A success response includes the API Token generated for the identity. 
        /// This API Token can be used to call API End points on behalf of the enrolled identity.
        /// 
        /// This endpoint provides equivalent functionality to adding new identity manually using Xooa console,
        /// and identities added using this endpoint will appear, and can be managed,
        /// using Xooa console under the identities tab of the smart contract app.
        ///
        /// Required permission: manage identities (canManageIdentities=true)
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityRequest">Identity to Enroll.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the new Identity.</returns>
        IdentityResponse enrollIdentity(IdentityRequest identityRequest, string timeout);

        /// <summary>
        /// The Enroll identity endpoint is used to enroll new identities for the smart contract app.
        /// A success response includes the API Token generated for the identity.
        /// This API Token can be used to call API End points on behalf of the enrolled identity.
        ///
        /// This endpoint provides equivalent functionality to adding new identity manually using Xooa console,
        /// and identities added using this endpoint will appear, and can be managed,
        /// using Xooa console under the identities tab of the smart contract app
        ///
        /// Required permission: manage identities (canManageIdentities=true)
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="IdentityRequest">Identity Data to enroll.</param>
        /// <returns>PendingTransactionResponse giving the resultId of the pending transaction.</returns>
        PendingTransactionResponse enrollIdentityAsync(IdentityRequest identityRequest);

        /// <summary>
        /// Generates new identity API Token.
        /// 
        /// Required permission: manage identities (canManageIdentities=true)
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityId">Identity Id to regenerate API Token for.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the new Api Token for the Identity.</returns>
        IdentityResponse regenerateIdentityApiToken(string identityId, string timeout);

        /// <summary>
        /// Get the specified identity from the identity registry.
        /// 
        /// Required permission: manage identities (canManageIdentities=true)
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityId">Identity Id to get data for.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the Identity.</returns>
        IdentityResponse getIdentity(string identityId, string timeout);

        /// <summary>
        /// Deletes an identity.
        /// 
        /// Required permission: manage identities (canManageIdentities=true)
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
        /// This endpoint returns authenticated identity information
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
                
                IRestResponse response = RestClient.Execute(request);
                Console.WriteLine(response.Content);
                JObject details = XooaSDK.Client.Util.Request.GetData(response);
                
                var Attrs = details["Attrs"];
                
                List<attrs> attributes = new List<attrs>();
                foreach(var attrObject in Attrs) {
                    
                    attrs attr = new attrs(attrObject["name"].ToString(),
                        attrObject["value"].ToString(), (bool) attrObject["ecert"]);
                    
                    attributes.Add(attr);
                }
                
                IdentityResponse identityResponse = new IdentityResponse(
                    (details["IdentityName"] != null) ? details["IdentityName"].ToString() : "",
                    (details["Access"] != null) ? details["Access"].ToString() : "",
                    (bool) details["canManageIdentities"],
                    (details["createdAt"] != null) ? details["createdAt"].ToString() : "",
                    (details["ApiToken"] != null) ? details["ApiToken"].ToString() : "",
                    (details["Id"] != null) ? details["Id"].ToString() : "",
                    attributes);
                
                return identityResponse;

            } catch (XooaRequestTimeoutException xrte) {
                Log.Error(xrte);
                throw xrte;
            } catch (XooaApiException xae) {
                Log.Error(xae);
                throw xae;
            } catch (System.Exception e) {
                Console.WriteLine(e.StackTrace);
                Log.Error(e);
                throw new XooaApiException(statusCode, e.Message);
            }
        }

        /// <summary>
        /// Get all identities from the identity registry
        /// Required permission: manage identities (canManageIdentities=true)
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
                
                IRestResponse response = RestClient.Execute(request);
                
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
        /// The Enroll identity endpoint is used to enroll new identities for the smart contract app. 
        /// A success response includes the API Token generated for the identity. 
        /// This API Token can be used to call API End points on behalf of the enrolled identity.
        /// 
        /// This endpoint provides equivalent functionality to adding new identity manually using Xooa console,
        /// and identities added using this endpoint will appear, and can be managed,
        /// using Xooa console under the identities tab of the smart contract app.
        ///
        /// Required permission: manage identities (canManageIdentities=true)
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="IdentityRequest">Identity to Enroll.</param>
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
                
                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);
                
                var Attrs = details["Attrs"];
                
                List<attrs> attributes = new List<attrs>();

                
                foreach(var attrObject in Attrs) {

                    attrs attr = new attrs(attrObject["name"].ToString(),
                        attrObject["value"].ToString(), (bool) attrObject["ecert"]);

                    attributes.Add(attr);
                }

                IdentityResponse identityResponse = new IdentityResponse(
                    (details["IdentityName"] != null) ? details["IdentityName"].ToString() : "",
                    (details["Access"] != null) ? details["Access"].ToString() : "",
                    (bool) details["canManageIdentities"],
                    (details["createdAt"] != null) ? details["createdAt"].ToString() : "",
                    (details["ApiToken"] != null) ? details["ApiToken"].ToString() : "",
                    (details["Id"] != null) ? details["Id"].ToString() : "",
                    attributes);

                return identityResponse;
                
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
        /// The Enroll identity endpoint is used to enroll new identities for the smart contract app.
        /// A success response includes the API Token generated for the identity.
        /// This API Token can be used to call API End points on behalf of the enrolled identity.
        ///
        /// This endpoint provides equivalent functionality to adding new identity manually using Xooa console,
        /// and identities added using this endpoint will appear, and can be managed,
        /// using Xooa console under the identities tab of the smart contract app
        ///
        /// Required permission: manage identities (canManageIdentities=true)
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="IdentityRequest">Identity Data to enroll.</param>
        /// <returns>PendingTransactionResponse giving the resultId of the pending transaction.</returns>
        public PendingTransactionResponse enrollIdentityAsync(IdentityRequest identityRequest) {

            Log.Info("Invoking URL - " + XooaConstants.IDENTITIES_URL);
            
            var localVarPath = XooaConstants.IDENTITIES_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.TRUE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, "1000"));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + ApiToken);

            string jsonBody = identityRequest.toString();

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath,
                    RestSharp.Method.POST, localVarQueryParameters, jsonBody, localVarHeaderParams,
                    null, null, contentType);
                
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

        /// <summary>
        /// Generates new identity API Token.
        /// 
        /// Required permission: manage identities (canManageIdentities=true)
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

                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                var Attrs = details["Attrs"];

                List<attrs> attributes = new List<attrs>();

                foreach(var attrObject in Attrs) {

                    attrs attr = new attrs(attrObject["name"].ToString(), 
                        attrObject["value"].ToString(), (bool) attrObject["ecert"]);

                    attributes.Add(attr);
                }

                IdentityResponse identityResponse = new IdentityResponse(
                    (details["IdentityName"] != null) ? details["IdentityName"].ToString() : "",
                    (details["Access"] != null) ? details["Access"].ToString() : "",
                    (bool) details["canManageIdentities"],
                    (details["createdAt"] != null) ? details["createdAt"].ToString() : "",
                    (details["ApiToken"] != null) ? details["ApiToken"].ToString() : "",
                    (details["Id"] != null) ? details["Id"].ToString() : "",
                    attributes);
                    
                return identityResponse;
                
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
        /// Get the specified identity from the identity registry.
        /// 
        /// Required permission: manage identities (canManageIdentities=true)
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

                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                Console.WriteLine(details);

                var Attrs = details["Attrs"];

                List<attrs> attributes = new List<attrs>();

                foreach(var attrObject in Attrs) {

                    attrs attr = new attrs(attrObject["name"].ToString(),
                        attrObject["value"].ToString(), (bool) attrObject["ecert"]);

                    attributes.Add(attr);
                }

                IdentityResponse identityResponse = new IdentityResponse(
                    (details["IdentityName"] != null) ? details["IdentityName"].ToString() : "",
                    (details["Access"] != null) ? details["Access"].ToString() : "",
                    (bool) details["canManageIdentities"],
                    (details["createdAt"] != null) ? details["createdAt"].ToString() : "",
                    (details["ApiToken"] != null) ? details["ApiToken"].ToString() : "",
                    (details["Id"] != null) ? details["Id"].ToString() : "",
                    attributes);

                return identityResponse;

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
        /// Deletes an identity.
        /// 
        /// Required permission: manage identities (canManageIdentities=true)
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
                
                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                return details["deleted"].ToString();
                
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