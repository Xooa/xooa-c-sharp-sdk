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
                throw new ArgumentException("The provided basePath is invalid.", "basePath");

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

            appUrl  = "https://api.ci.xooa.io/api/v1/";

            // Setting Timeout has side effects (forces ApiClient creation).
            timeout = 1000;

            this.restClient = new RestClient(appUrl);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XooaClient"/> class.
        /// </summary>
        /// <param name="apiToken">API Token for the Identity.</param>
        /// <param name="appUrl">App URl to connect to.</param>
        public XooaClient(string apiToken, string appUrl = "https://api.ci.xooa.io") {
            
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
        public void subscribeAllEvents(Action<object> callback) {
            
            this.webSocket = new WebSocket(apiToken);
            webSocket.subscribeEvents("-1", callback);
        }

        /// <summary>
        /// Subscribe to the events matching the regex on the blockchain.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="regex">Regex to check for the events to subscribe to.</param>
        /// <param name="callback">Callback function to apply to the response.</param>
        public void subscribeEvents(string regex, Action<object> callback) {
            
            this.webSocket = new WebSocket(apiToken);
            webSocket.subscribeEvents(regex, callback);
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
        /// Get block data for the latestblock.
        /// Get specific block information such as previous block hash, data hash, # of transactions
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>CurrentBlockResponse giving the data about the block.</returns>
        public CurrentBlockResponse getCurrentBlock(string timeout = "3000") {

            return new BlockchainApi(restClient, apiToken).getCurrentBlock(timeout);
        }

        /// <summary>
        /// Get block data for the latest block in async mode.
        /// Get specific block information such as previous block hash, data hash, # of transactions
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        public PendingTransactionResponse getCurrentBlockAsync() {

            return new BlockchainApi(restClient, apiToken).getCurrentBlockAsync();
        }

        /// <summary>
        /// Get block data for the given block number.
        /// Get specific block information such as previous block hash, data hash, # of transactions
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
        /// Get block data for the given block number.
        /// Get specific block information such as previous block hash, data hash, # of transactions
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="blockNumber">Block number to fetch data</param>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        public PendingTransactionResponse getBlockByNumberAsync(string blockNumber) {
            
            return new BlockchainApi(restClient, apiToken).getBlockByNumberAsync(blockNumber);
        }

		/// <summary>
        /// Get the current identity data with which the user is logged in.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the current Identity.</returns>
        public IdentityResponse currentIdentity(string timeout = "3000") {

            return new IdentitiesApi(restClient, apiToken).currentIdentity(timeout);
        }
		
		/// <summary>
        /// Get the list of all the identities available.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>List of IdentityResponse giving the data about all the Identities.</returns>
        public List<IdentityResponse> getIdentities(string timeout = "3000") {

            return new IdentitiesApi(restClient, apiToken).getIdentities(timeout);
        }

		/// <summary>
        /// Enroll a new Identity with the app.
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
        /// Enroll a new Identity with the app in async mode.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="IdentityRequest">Identity Data to enroll.</param>
        /// <returns>PendingTransactionResponse giving the resultId of the pending transaction.</returns>
        public PendingTransactionResponse enrollIdentityAsync(IdentityRequest identityRequest) {

            return new IdentitiesApi(restClient, apiToken).enrollIdentityAsync(identityRequest);
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

            return new IdentitiesApi(restClient, apiToken).regenerateIdentityApiToken(identityId, timeout);
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

            return new IdentitiesApi(restClient, apiToken).getIdentity(identityId, timeout);
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

            return new IdentitiesApi(restClient, apiToken).deleteIdentity(identityId, timeout);
        }

        /// <summary>
        /// Get QueryResponse for the function and arguments.
        /// Get payload information 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="functionName">Function to Query.</param>
        /// <param name="args">Arguments to query.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>QueryResponse giving the payload for the argument.</returns>
        public QueryResponse query(string functionName, string args = null, string timeout = "3000") {

            return new QueryApi(restClient, apiToken).query(functionName, args, timeout);
        }

        /// <summary>
        /// Get QueryResponse for the function and arguments.
        /// Get payload information 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="functionName">Function Name to Query.</param>
        /// <param name="args">Arguments for the Query.</param>
        /// <returns>PendingTransactionResponse giving the resultId and resultUrl.</returns>
        public PendingTransactionResponse queryAsync(string functionName, string args = null) {

            return new QueryApi(restClient, apiToken).queryAsync(functionName, args);
        }

        /// <summary>
        /// Get InvokeResponse for the function and arguments.
        /// Get payload information 
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
        /// Get InvokeResponse for the function and arguments.
        /// Get payload information 
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="functionName">Function Name to invoke.</param>
        /// <param name="args">Arguments for the transaction.</param>
        /// <returns>PendingTransactionResponse giving the resultId and resultUrl.</returns>
        public PendingTransactionResponse invokeAsync(string functionName, string[] args = null) {

            return new InvokeApi(restClient, apiToken).invokeAsync(functionName, args);
        }

        /// <summary>
        /// Get QueryResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>QueryResponse giving the data about the Query.</returns>
        public QueryResponse getResultForQuery(string resultId, string timeout = "3000") {

            return new ResultApi(restClient, apiToken).getResultForQuery(resultId, timeout);
        }

        /// <summary>
        /// Get InvokeResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>InvokeResponse giving the data about the Invoke request.</returns>
        public InvokeResponse getResultForInvoke(string resultId, string timeout = "3000") {

            return new ResultApi(restClient, apiToken).getResultForInvoke(resultId, timeout);
        }

        /// <summary>
        /// Get IdentityResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the Identity request.</returns>
        public IdentityResponse getResultForIdentity(string resultId, string timeout = "3000") {
            
            return new ResultApi(restClient, apiToken).getResultForIdentity(resultId, timeout);
        }

        /// <summary>
        /// Get CurrentBlockResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>CurrentBlockResponse giving the data about the CurrentBlock request.</returns>
        public CurrentBlockResponse getResultForCurrentBlock(string resultId, string timeout = "3000") {

            return new ResultApi(restClient, apiToken).getResultForCurrentBlock(resultId, timeout);
        }

        /// <summary>
        /// Get BlockResponse data for the Result Id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>BlockResponse giving the data about the BlockByNumber request.</returns>
        public BlockResponse getResultForBlockByNumber(string resultId, string timeout = "3000") {
			
            return new ResultApi(restClient, apiToken).getResultForBlockByNumber(resultId, timeout);
        }
    }
}