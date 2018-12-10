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
        /// Use this endpoint to Get the block number and hashes of current (highest) block in the network.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>CurrentBlockResponse giving the data about the block.</returns>
        CurrentBlockResponse getCurrentBlock(string timeout);

        /// <summary>
        /// Use this endpoint to Get the block number and hashes of current (highest) block in the network
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        PendingTransactionResponse getCurrentBlockAsync();

        /// <summary>
        /// Use this endpoint to Get the number of transactions and hashes of a specific block in the network parameters
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="blockNumber">Block number to fetch data</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>BlockResponse giving the data about the block.</returns>
        BlockResponse getBlockByNumber(string blockNumber, string timeout);

        /// <summary>
        /// Use this endpoint to Get the number of transactions and hashes of a specific block in the network parameters
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="blockNumber">Block number to fetch data</param>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        PendingTransactionResponse getBlockByNumberAsync(string blockNumber);

        /// <summary>
        /// Use this endpoint to Get transaction by transaction id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="transactionId">Transaction Id to fetch data</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>TransactionResponse giving the data about the transaction.</returns>
        TransactionResponse getTransactionByTransactionId(string transactionId, string timeout);

        /// <summary>
        /// Use this endpoint to Get transaction by transaction id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionId">Transaction Id to fetch data</param>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        PendingTransactionResponse getTransactionByTransactionIdAsync(string transactionId);
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
        /// Use this endpoint to Get the block number and hashes of current (highest) block in the network.
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
                
                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                CurrentBlockResponse currentBlockResponse = new CurrentBlockResponse
                    (details["currentBlockHash"].ToString(), details["previousBlockHash"].ToString(),
                    (int) details["blockNumber"]);

                return currentBlockResponse;

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
        /// Use this endpoint to Get the block number and hashes of current (highest) block in the network
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
        /// Use this endpoint to Get the number of transactions and hashes of a specific block in the network parameters
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

                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);
                BlockResponse blockResponse = new BlockResponse(
                    details["previous_hash"].ToString(), details["data_hash"].ToString(),
                    (int) details["blockNumber"], (int) details["numberOfTransactions"]);

                return blockResponse;

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
        /// Use this endpoint to Get the number of transactions and hashes of a specific block in the network parameters
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
        /// Use this endpoint to Get transaction by transaction id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <exception cref="Xooa.Client.Exception.XooaRequestTimeoutException">Thrown when a 202 response is recieved.</exception>
        /// <param name="transactionId">Transaction Id to fetch data</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>TransactionResponse giving the data about the transaction.</returns>
        public TransactionResponse getTransactionByTransactionId(string transactionId, string timeout = "3000") {

            Log.Info("Invoking URL - " + XooaConstants.TRANSACTION_URL);

            var localVarPath = XooaConstants.TRANSACTION_URL;
            var contentType = XooaConstants.CONTENT_TYPE;
            
            var localVarQueryParameters = new List<KeyValuePair<string,string>>();
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.ASYNC, XooaConstants.FALSE));
            localVarQueryParameters.Add(new KeyValuePair<string, string>(XooaConstants.TIMEOUT, timeout));

            var localVarHeaderParams = new Dictionary<string, string>();
            localVarHeaderParams.Add(XooaConstants.ACCEPT, XooaConstants.CONTENT_TYPE);
            localVarHeaderParams.Add(XooaConstants.AUTHORIZATION, XooaConstants.TOKEN + apiToken);

            var localVarPathParams = new Dictionary<string, string>();
            localVarPathParams.Add("TransactionId", transactionId);

            int statusCode = 0;

            try {

                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath, RestSharp.Method.GET,
                    localVarQueryParameters, null, localVarHeaderParams, null, localVarPathParams, contentType);

                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                string txnId = details["txid"].ToString();
                string smartContract = details["smartcontract"].ToString();
                string creatorMspId = details["creator_msp_id"].ToString();
                string createdAt = details["createdt"].ToString();
                string type = details["type"].ToString();
                var endorserIds = details["endorser_msp_id"];
                var readsets = details["read_set"];
                var writesets = details["write_set"];

                List<string> endorserMspIds = new List<string>();
                List<ReadSet> readSetsList = new List<ReadSet>();
                List<WriteSet> writeSetsList = new List<WriteSet>();

                foreach (var id in endorserIds) {
                    endorserMspIds.Add(id.ToString());
                }

                foreach (var set in readsets) {
                    
                    string chaincode = set["chaincode"].ToString();
                    var readsubsets = set["set"];

                    List<ReadSubSet> subSetsList = new List<ReadSubSet>();

                    foreach (var subset in readsubsets) {

                        string key = subset["key"].ToString();
                        var vrsn = subset["version"];

                        string blockNumber = vrsn["block_num"].ToString();
                        string transactionNumber = vrsn["tx_num"].ToString();

                        Response.Version version = new Response.Version(blockNumber, transactionNumber);

                        ReadSubSet readSubSet = new ReadSubSet(key, version);

                        subSetsList.Add(readSubSet);
                    }

                    ReadSet readSet = new ReadSet(chaincode, subSetsList);

                    readSetsList.Add(readSet);
                }


                foreach(var set in writesets) {

                    string chaincode = set["chaincode"].ToString();
                    var writesubsets = set["set"];

                    List<WriteSubSet> writeSubSetsList = new List<WriteSubSet>();

                    foreach (var writesubset in writesubsets) {
                        
                        string key = set["key"].ToString();
                        string value = set["value"].ToString();
                        bool isDelete = (bool) set["is_delete"];

                        WriteSubSet writeSubSet = new WriteSubSet(key, value, isDelete);

                        writeSubSetsList.Add(writeSubSet);
                    }

                    WriteSet writeSet = new WriteSet(chaincode, writeSubSetsList);
                    writeSetsList.Add(writeSet);
                }

                TransactionResponse transactionResponse = new TransactionResponse(txnId, smartContract, creatorMspId, endorserMspIds,
                    type, createdAt, readSetsList, writeSetsList);

                return transactionResponse;

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
        /// Use this endpoint to Get transaction by transaction id.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="transactionId">Transaction Id to fetch data</param>
        /// <returns>PendingTransactionResponse giving the resultId for the transaction.</returns>
        public PendingTransactionResponse getTransactionByTransactionIdAsync(string transactionId) {
            
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
            localVarPathParams.Add("TransactionId", transactionId);

            int statusCode = 0;

            try {

                RestRequest request = XooaSDK.Client.Util.Request.PrepareRequest(localVarPath, RestSharp.Method.GET,
                    localVarQueryParameters, null, localVarHeaderParams, null, localVarPathParams, contentType);

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