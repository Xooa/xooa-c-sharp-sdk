using System;
using NUnit.Framework;
using XooaSDK.Client;
using XooaSDK.Client.Api;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Response;
using XooaSDK.Client.Util;

namespace XooaSDK.Test.Api {

    [TestFixture]
    public class BlockChainApiTest {

        [Test]
        public void testGetCurrentBlockResponse() {
            
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                CurrentBlockResponse cbr = xooaClient.getCurrentBlock();

                //Assert.AreEqual(typeof(CurrentBlockResponse), cbr.GetType());

                Assert.IsNotEmpty(cbr.getBlockNumber().ToString());

                Assert.IsNotEmpty(cbr.getCurrentBlockHash());

                Assert.IsNotEmpty(cbr.getPreviousBlockHash());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetCurrentBlockResponseTimeout() {
            
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                CurrentBlockResponse cbr = xooaClient.getCurrentBlock("200");

                //Assert.AreEqual(typeof(CurrentBlockResponse), cbr.GetType());

                Assert.IsNotEmpty(cbr.getBlockNumber().ToString());

                Assert.IsNotEmpty(cbr.getCurrentBlockHash());

                Assert.IsNotEmpty(cbr.getPreviousBlockHash());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetCurrentBlockResponseAsync() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                PendingTransactionResponse ptr = xooaClient.getCurrentBlockAsync();

                //Assert.AreEqual(typeof(PendingTransactionResponse), ptr.GetType());

                Assert.IsNotEmpty(ptr.getResultId());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
            
        }

        [Test]
        public void testGetBlockByNumber() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string blockNumber = "1";

            try {
                BlockResponse br = xooaClient.getBlockByNumber(blockNumber);

                //Assert.AreEqual(typeof(BlockResponse), br.GetType());

                Assert.AreEqual(blockNumber, br.getBlockNumber());

                Assert.IsNotEmpty(br.getDataHash());

                Assert.IsNotEmpty(br.getPreviousHash());

                Assert.IsNotEmpty(br.getNumberOfTransactions().ToString());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {
                
                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetBlockByNumberTimeout() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string blockNumber = "1";

            try {
                BlockResponse br = xooaClient.getBlockByNumber(blockNumber, "200");

                //Assert.AreEqual(typeof(BlockResponse), br.GetType());

                Assert.AreEqual(blockNumber, br.getBlockNumber());

                Assert.IsNotEmpty(br.getDataHash());

                Assert.IsNotEmpty(br.getPreviousHash());

                Assert.IsNotEmpty(br.getNumberOfTransactions().ToString());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {
                
                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetBlockByNumberAsync() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string blockNumber = "1";

            try {
                PendingTransactionResponse ptr = xooaClient.getBlockByNumberAsync(blockNumber);

                //Assert.AreEqual(typeof(PendingTransactionResponse), ptr.GetType());

                Assert.IsNotEmpty(ptr.getResultId());

            } catch (XooaApiException xae) {
                
                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }
    }
}