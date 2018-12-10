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

namespace XooaSDK.Client.Api {

    public interface IResultApi {

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>QueryResponse giving the data about the Query.</returns>
        QueryResponse getResultForQuery(string resultId, string timeout);

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>InvokeResponse giving the data about the Invoke request.</returns>
        InvokeResponse getResultForInvoke(string resultId, string timeout);

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>IdentityResponse giving the data about the Identity request.</returns>
        IdentityResponse getResultForIdentity(string resultId, string timeout);

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>CurrentBlockResponse giving the data about the CurrentBlock request.</returns>
        CurrentBlockResponse getResultForCurrentBlock(string resultId, string timeout);

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>BlockResponse giving the data about the BlockByNumber request.</returns>
        BlockResponse getResultForBlockByNumber(string resultId, string timeout);

        /// <summary>
        /// This endpoint returns the result of previously submitted api request.
        /// </summary>
        /// <exception cref="Xooa.Client.Exception.XooaApiException">Thrown when fails to make API call</exception>
        /// <param name="resultId">Result Id of the transaction to fetch data.</param>
        /// <param name="timeout">Timeout interval for transaction.</param>
        /// <returns>TransactionResponse giving the data about the transaction.</returns>
        TransactionResponse getResultForTransaction(string resultId, string timeout);
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
                
                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                var payload = details["result"];

                QueryResponse queryResponse = new QueryResponse(payload.ToString());

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
                
                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                var payload = details["result"];

                InvokeResponse invokeResponse = new InvokeResponse(payload["txId"].ToString(),
                    payload["payload"].ToString());

                return invokeResponse;

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

                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                var payload = details["result"];
                var Attrs = payload["Attrs"];

                List<attrs> attributes = new List<attrs>();

                foreach(var attrObject in Attrs) {

                    attrs attr = new attrs(attrObject["name"].ToString(), 
                        attrObject["value"].ToString(), (bool) attrObject["ecert"]);

                    attributes.Add(attr);
                }

                IdentityResponse identityResponse = new IdentityResponse(
                    (payload["IdentityName"].ToString() != null) ? payload["IdentityName"].ToString() : "",
                    (payload["Access"].ToString() != null) ? payload["Access"].ToString() : "",
                    (bool) payload["canManageIdentities"],
                    (payload["createdAt"].ToString() != null) ? payload["createdAt"].ToString() : "",
                    (payload["ApiToken"].ToString() != null) ? payload["ApiToken"].ToString() : "",
                    (payload["Id"].ToString() != null) ? payload["Id"].ToString() : "",
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
                
                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);

                var payload = details["result"];

                CurrentBlockResponse currentBlockResponse = new CurrentBlockResponse(
                    payload["currentBlockHash"].ToString(),
                    payload["previousBlockHash"].ToString(),
                    (int) payload["blockNumber"]);

                return currentBlockResponse;

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

                IRestResponse response = RestClient.Execute(request);

                JObject details = XooaSDK.Client.Util.Request.GetData(response);
                
                var payload = details["result"];

                BlockResponse blockResponse = new BlockResponse(
                    payload["previous_hash"].ToString(),
                    payload["data_hash"].ToString(),
                    (int) payload["blockNumber"],
                    (int) payload["numberOfTransactions"]);

                return blockResponse;
                
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

        public TransactionResponse getResultForTransaction(string resultId, string timeout = "3000") {

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

                IRestResponse response = RestClient.Execute(request);

                JObject payload = XooaSDK.Client.Util.Request.GetData(response);
                
                var details = payload["result"];

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
            } catch(System.Exception e) {
                Log.Error(e);
                throw new XooaApiException(statusCode, e.Message);
            }
        }
    }
}