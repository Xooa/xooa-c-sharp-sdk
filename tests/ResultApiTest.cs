using System;
using NUnit.Framework;
using XooaSDK.Client;
using XooaSDK.Client.Api;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Response;
using XooaSDK.Client.Util;

namespace XooaSDK.Test.Api {

    [TestFixture]
    public class ResultApiTest {

        [Test]
        public void testGetResultForQuery() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "";

            try {
                QueryResponse queryResponse = xooaClient.getResultForQuery(resultId);

                //Assert.AreEqual(typeof(QueryResponse).ToString(), queryResponse.GetType());
                
                Assert.IsNotEmpty(queryResponse.getPayload());

                Assert.AreEqual("", queryResponse.getPayload());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.AreEqual(resultId, xrte.getResultId());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorMessage());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());
            }
        }

        [Test]
        public void testGetResultForQueryTimeout() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "";

            try {
                QueryResponse queryResponse = xooaClient.getResultForQuery(resultId, "200");

                //Assert.AreEqual(typeof(QueryResponse).ToString(), queryResponse.GetType());
                
                Assert.IsNotEmpty(queryResponse.getPayload());

                Assert.AreEqual("", queryResponse.getPayload());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.AreEqual(resultId, xrte.getResultId());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorMessage());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());
            }
        }

        [Test]
        public void testGetResultForInvoke() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);
            
            string resultId = "ef6ed8da-ced1-4742-a0b0-8ffc8b11ba45";

            try {
                InvokeResponse invokeResponse = xooaClient.getResultForInvoke(resultId);

                //Assert.AreEqual(typeof(InvokeResponse).ToString(), invokeResponse.GetType());

                Assert.IsNotEmpty(invokeResponse.getPayload());

                Assert.IsNotEmpty(invokeResponse.getTxnId());

                Assert.AreEqual("800", invokeResponse.getPayload());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetResultForInvokeTimeout() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);
            
            string resultId = "ef6ed8da-ced1-4742-a0b0-8ffc8b11ba45";

            try {
                InvokeResponse invokeResponse = xooaClient.getResultForInvoke(resultId, "200");

                //Assert.AreEqual(typeof(InvokeResponse).ToString(), invokeResponse.GetType());

                Assert.IsNotEmpty(invokeResponse.getPayload());

                Assert.IsNotEmpty(invokeResponse.getTxnId());

                Assert.AreEqual("800", invokeResponse.getPayload());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetResultForIdentity() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "";

            try {
                IdentityResponse identityResponse = xooaClient.getResultForIdentity(resultId);

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identityResponse.GetType());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotEmpty(identityResponse.getAccess());

                Assert.IsNotEmpty(identityResponse.getCanManageIdentities().ToString());

                Assert.IsNotEmpty(identityResponse.getApiToken());

                Assert.IsNotEmpty(identityResponse.getId());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());
                 
            } catch (XooaApiException xae) {
                
                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());

            }
        }

        [Test]
        public void testGetResultForIdentityTimeout() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "";

            try {
                IdentityResponse identityResponse = xooaClient.getResultForIdentity(resultId, "200");

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identityResponse.GetType());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotEmpty(identityResponse.getAccess());

                Assert.IsNotEmpty(identityResponse.getCanManageIdentities().ToString());

                Assert.IsNotEmpty(identityResponse.getApiToken());

                Assert.IsNotEmpty(identityResponse.getId());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());
                 
            } catch (XooaApiException xae) {
                
                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());

            }
        }

        [Test]
        public void testGetResultForCurrentBlock() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "";

            try {
                CurrentBlockResponse currentBlock = xooaClient.getResultForCurrentBlock(resultId);

                //Assert.AreEqual(typeof(CurrentBlockResponse).ToString(), currentBlock.GetType());
            
                Assert.IsNotEmpty(currentBlock.getBlockNumber().ToString());

                Assert.IsNotEmpty(currentBlock.getCurrentBlockHash());

                Assert.IsNotEmpty(currentBlock.getPreviousBlockHash());
            
            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());

            } catch (XooaApiException xae) {
                
                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetResultForCurrentBlockTimeout() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "";

            try {
                CurrentBlockResponse currentBlock = xooaClient.getResultForCurrentBlock(resultId, "200");

                //Assert.AreEqual(typeof(CurrentBlockResponse).ToString(), currentBlock.GetType());
            
                Assert.IsNotEmpty(currentBlock.getBlockNumber().ToString());

                Assert.IsNotEmpty(currentBlock.getCurrentBlockHash());

                Assert.IsNotEmpty(currentBlock.getPreviousBlockHash());
            
            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());

            } catch (XooaApiException xae) {
                
                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetResultForBlockByNumber() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "";

            try {

                BlockResponse blockResponse = xooaClient.getResultForBlockByNumber(resultId);

                //Assert.AreEqual(typeof(BlockResponse).ToString(), blockResponse.GetType());

                Assert.IsNotEmpty(blockResponse.getBlockNumber().ToString());

                Assert.IsNotEmpty(blockResponse.getDataHash());

                Assert.IsNotEmpty(blockResponse.getNumberOfTransactions().ToString());

                Assert.IsNotEmpty(blockResponse.getPreviousHash());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetResultForBlockByNumberTimeout() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "";

            try {

                BlockResponse blockResponse = xooaClient.getResultForBlockByNumber(resultId, "200");

                //Assert.AreEqual(typeof(BlockResponse).ToString(), blockResponse.GetType());

                Assert.IsNotEmpty(blockResponse.getBlockNumber().ToString());

                Assert.IsNotEmpty(blockResponse.getDataHash());

                Assert.IsNotEmpty(blockResponse.getNumberOfTransactions().ToString());

                Assert.IsNotEmpty(blockResponse.getPreviousHash());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }
    }
}