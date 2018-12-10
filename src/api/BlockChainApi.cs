using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using XooaSDK.Client.Util;
using XooaSDK.Client.Response;
using XooaSDK.Client.Exception;
using RestSharp;
using Newtonsoft.Json.Linq;
using Common.Logging;

namespace XooaSDK.Client.Api {

    public interface IBlockchainApi {

        /// <summary>
        /// Get block data for the latestblock.
        /// Get specific block information such as previous block hash, data hash, # of transactions
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>CurrentBlockResponse giving the data about the block.</returns>
        CurrentBlockResponse getCurrentBlock(string timeout);

        /// <summary>
        /// Get block data for the latest block in async mode.
        /// Get specific block information such as previous block hash, data hash, # of transactions
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        PendingTransactionResponse getCurrentBlockAsync();

        /// <summary>
        /// Get block data for the given block number.
        /// Get specific block information such as previous block hash, data hash, # of transactions
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="blockNumber">Block number to fetch data</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>BlockResponse giving the data about the block.</returns>
        BlockResponse getBlockByNumber(string blockNumber, string timeout);

        /// <summary>
        /// Get block data for the given block number.
        /// Get specific block information such as previous block hash, data hash, # of transactions
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="blockNumber">Block number to fetch data</param>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        PendingTransactionResponse getBlockByNumberAsync(string blockNumber);
    }


    public class BlockchainApi : IBlockchainApi {

        /// <value>Rest Client Object.</value>
        private RestClient RestClient;
        
        /// <value>Api Token.</value>
        private string apiToken;

        /// <value>Commons Logger.</value>
        private static readonly ILog Log = LogManager.GetLogger<Program>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockchainApi"/> class.
        /// </summary>
        /// <param name="RestClient">RestClient to connect with the API.</param>
        /// <param name="ApiToken">ApiToken to Authenticate the user.</param>
        public BlockchainApi(RestClient RestClient, string apiToken) {
            this.RestClient = RestClient;
            this.apiToken = apiToken;
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
            
            Log.Info("Invoking URL - " + XooaConstants.CURRENT_BLOCK_URL);

            var localVarPath = XooaConstants.CURRENT_BLOCK_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + apiToken);

            int statusCode = 0;

            try {
                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath, RestSharp.Method.GET, 
                    localVarQueryParameters, null, localVarHeaderParams, null, null, contentType);

                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);

                    CurrentBlockResponse currentBlockResponse = new CurrentBlockResponse
                        (details["currentBlockHash"].ToString(), details["previousBlockHash"].ToString(),
                        (int) details["blockNumber"]);
                    
                    return currentBlockResponse;

                } else if (statusCode == 202) {

                    var details = JObject.Parse(data);

                    Log.Info("Received a PendingTransactionResponse, throwing XooaRequestTimeoutException");
                    
                    throw new XooaRequestTimeoutException(details["resultId"].ToString(),
                        details["resultUrl"].ToString());
                    
                } else {

                    Log.Info("Received an error response from Blockchain.");
                    //Log.Error("Error in processing transaction by blockchain.");

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
        /// Get block data for the latest block in async mode.
        /// Get specific block information such as previous block hash, data hash, # of transactions
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        public PendingTransactionResponse getCurrentBlockAsync() {
            
            Log.Info("Invoking URL - " + XooaConstants.CURRENT_BLOCK_URL);

            var localVarPath = XooaConstants.CURRENT_BLOCK_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.TRUE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, "1000"));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + apiToken);

            int statusCode = 0;
            
            try {

                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath, RestSharp.Method.GET,
                    localVarQueryParameters, null, localVarHeaderParams, null, null, contentType);
                
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
            
            Log.Info("Invoking URL - " + XooaConstants.BLOCK_DATA_URL);

            var localVarPath = XooaConstants.BLOCK_DATA_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + apiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("BlockNumber", blockNumber);

            int statusCode = 0;

            try {

                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath, RestSharp.Method.GET,
                    localVarQueryParameters, null, localVarHeaderParams, null, localVarPathParams, contentType);

                var response = RestClient.Execute(request);
                statusCode = (int) response.StatusCode;
                var data = response.Content;

                Log.Debug("Status Code - " + statusCode);
                Log.Debug("Response - " + data);

                if (statusCode == 200) {

                    Log.Info("Received a 200 Response from Blockchain. Processing...");

                    var details = JObject.Parse(data);

                    BlockResponse blockResponse = new BlockResponse(
                        details["previous_hash"].ToString(), details["data_hash"].ToString(),
                        (int) details["blockNumber"], (int) details["numberOfTransactions"]);

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
            } catch (System.Exception e) {
                Log.Error(e);
                throw new XooaApiException(statusCode, e.Message);
            }
        }


        /// <summary>
        /// Get block data for the given block number.
        /// Get specific block information such as previous block hash, data hash, # of transactions
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="blockNumber">Block number to fetch data</param>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        public PendingTransactionResponse getBlockByNumberAsync(string blockNumber) {
            
            Log.Info("Invoking URL - " + XooaConstants.BLOCK_DATA_URL);

            var localVarPath = XooaConstants.BLOCK_DATA_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.TRUE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, "1000"));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + apiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("BlockNumber", blockNumber);

            int statusCode = 0;

            try {

                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath, RestSharp.Method.GET,
                    localVarQueryParameters, null, localVarHeaderParams, null, localVarPathParams, contentType);

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