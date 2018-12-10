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
using XooaSDK.Client.Api;
using XooaSDK.Client.Response;
using XooaSDK.Client.Request;
using XooaSDK.Client.Util;
using RestSharp;

namespace XooaSDK.Client {

    public class XooaClient {

        /// <value>Rest Client Object.</value>
        private RestClient restClient;

        /// <value>WebSocket Object.</value>
        private WebSocket webSocket;

        /// <value>Api Token.</value>
        private string apiToken;

        /// <summary>
        /// Gets or sets the Timeout.
        /// </summary>
        /// <value>Timeout.</value>
        private int timeout {get; set; }

        /// <value>AppUrl.</value>
        private string appUrl;

        /// <summary>
        /// Gets the AppUrl.
        /// </summary>
        public string getAppUrl() {
            
            return appUrl;
        }

        /// <summary>
        /// Sets the AppUrl.
        /// </summary>
        public void setAppUrl(string appUrl) {
            
            if (string.IsNullOrWhiteSpace(appUrl))
                throw new ArgumentException("The provided App URL is invalid.");

            this.appUrl = appUrl;

            this.restClient = new RestClient(this.appUrl);
        }

        /// <summary>
        /// Sets the API Token.
        /// </summary>
        public void setApiToken(string apiToken) {
            
            if (string.IsNullOrWhiteSpace(apiToken))
                throw new ArgumentException("The provided Api Token is invalid.", "apiToken");
            
            this.apiToken = apiToken;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XooaClient"/> class.
        /// </summary>
       public XooaClient() {

            appUrl  = "https://api.xooa.com/api/v1/";

            // Setting Timeout has side effects (forces ApiClient creation).
            timeout = 4000;

            this.restClient = new RestClient(appUrl);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XooaClient"/> class.
        /// </summary>
        /// <param name="apiToken">API Token for the Identity.</param>
        /// <param name="appUrl">App URl to connect to.</param>
        public XooaClient(string apiToken, string appUrl = "https://api.xooa.com/api/v1/") {
            
            if (string.IsNullOrWhiteSpace(appUrl))
                throw new ArgumentException("The provided basePath is invalid.", "basePath");

            this.appUrl = appUrl;
            this.apiToken = apiToken;

            this.restClient = new RestClient(this.appUrl);
        }

        /// <summary>
        /// Validate the Api Token.
        /// Get the Iddntity associated with the current API Token
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <returns>IdentityResponse validating the data about the current Identity.</returns>
        public IdentityResponse validate() {
            
            return new IdentitiesApi(restClient, apiToken).currentIdentity();
        }

        /// <summary>
        /// Subscribe to all the events on the blockchain.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="callback">Callback function to apply to the response.</param>
        public void subscribeEvents(Action<object> callback) {
            
            this.webSocket = new WebSocket(apiToken);
            webSocket.subscribeEvents(callback);
        }

        /// <summary>
        /// UnSubscribe from the events on the blockchain.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        public void unsubscribe() {
            
            if (webSocket != null)
                webSocket.unsubscribe();
        }


        /// <summary>
        /// Use this endpoint to Get the block number and hashes of current (highest) block in the network.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>CurrentBlockResponse giving the data about the block.</returns>
        public CurrentBlockResponse getCurrentBlock(string timeout = "3000") {

            return new BlockchainApi(restClient, apiToken).getCurrentBlock(timeout);
        }

        /// <summary>
        /// Use this endpoint to Get the block number and hashes of current (highest) block in the network
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        public PendingTransactionResponse getCurrentBlockAsync() {

            return new BlockchainApi(restClient, apiToken).getCurrentBlockAsync();
        }

        /// <summary>
        /// Use this endpoint to Get the number of transactions and hashes of a specific block in the network parameters
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="blockNumber">Block number to fetch data</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>BlockResponse giving the data about the block.</returns>
        public BlockResponse getBlockByNumber(string blockNumber, string timeout = "3000") {
            
            return new BlockchainApi(restClient, apiToken).getBlockByNumber(blockNumber, timeout);
        }

        /// <summary>
        /// Use this endpoint to Get the number of transactions and hashes of a specific block in the network parameters
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="blockNumber">Block number to fetch data</param>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        public PendingTransactionResponse getBlockByNumberAsync(string blockNumber) {
            
            return new BlockchainApi(restClient, apiToken).getBlockByNumberAsync(blockNumber);
        }

        /// <summary>
        /// Use this endpoint to Get transaction by transaction id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="transactionId">Transaction Id to fetch data</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>TransactionResponse giving the data about the transaction.</returns>
        public TransactionResponse getTransactionByTransactionId(string transactionId, string timeout = "3000") {

            new BlockchainApi(restClient, apiToken);
            return null;
        }

        /// <summary>
        /// Use this endpoint to Get transaction by transaction id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionId">Transaction Id to fetch data</param>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        public PendingTransactionResponse getTransactionByTransactionIdAsync(string transactionId) {

            new BlockchainApi(restClient, apiToken);
            return null;
        }

		/// <summary>
        /// This endpoint returns authenticated identity information
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the current Identity.</returns>
        public IdentityResponse currentIdentity(string timeout = "3000") {

            return new IdentitiesApi(restClient, apiToken).currentIdentity(timeout);
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

            return new IdentitiesApi(restClient, apiToken).getIdentities(timeout);
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

            return new IdentitiesApi(restClient, apiToken).enrollIdentity(identityRequest, timeout);
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

            return new IdentitiesApi(restClient, apiToken).enrollIdentityAsync(identityRequest);
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

            return new IdentitiesApi(restClient, apiToken).regenerateIdentityApiToken(identityId, timeout);
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

            return new IdentitiesApi(restClient, apiToken).getIdentity(identityId, timeout);
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

            return new IdentitiesApi(restClient, apiToken).deleteIdentity(identityId, timeout);
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

            return new QueryApi(restClient, apiToken).query(functionName, args, timeout);
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

            return new QueryApi(restClient, apiToken).queryAsync(functionName, args);
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

            return new InvokeApi(restClient, apiToken).invoke(functionName, args, timeout);
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

            return new InvokeApi(restClient, apiToken).invokeAsync(functionName, args);
        }

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>QueryResponse giving the data about the Query.</returns>
        public QueryResponse getResultForQuery(string resultId, string timeout = "3000") {

            return new ResultApi(restClient, apiToken).getResultForQuery(resultId, timeout);
        }

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>InvokeResponse giving the data about the Invoke request.</returns>
        public InvokeResponse getResultForInvoke(string resultId, string timeout = "3000") {

            return new ResultApi(restClient, apiToken).getResultForInvoke(resultId, timeout);
        }

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the Identity request.</returns>
        public IdentityResponse getResultForIdentity(string resultId, string timeout = "3000") {
            
            return new ResultApi(restClient, apiToken).getResultForIdentity(resultId, timeout);
        }

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>CurrentBlockResponse giving the data about the CurrentBlock request.</returns>
        public CurrentBlockResponse getResultForCurrentBlock(string resultId, string timeout = "3000") {

            return new ResultApi(restClient, apiToken).getResultForCurrentBlock(resultId, timeout);
        }

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>BlockResponse giving the data about the BlockByNumber request.</returns>
        public BlockResponse getResultForBlockByNumber(string resultId, string timeout = "3000") {
			
            return new ResultApi(restClient, apiToken).getResultForBlockByNumber(resultId, timeout);
        }

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>TransactionResponse giving the data about the transaction.</returns>
        public TransactionResponse getResultForTransaction(string resultId, string timeout = "3000") {

            return new ResultApi(restClient, apiToken).getResultForTransaction(resultId, timeout);
        }
    }
}