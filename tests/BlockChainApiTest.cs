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
using System.Threading.Tasks;
using System.Net;
using NUnit.Framework;
using XooaSDK.Client;
using XooaSDK.Client.Api;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Response;
using XooaSDK.Client.Util;
using RestSharp;
using Moq;

namespace XooaSDK.Test.Api {

    [TestFixture]
    public class BlockChainApiTest {

        [Test]
        public void testGetCurrentBlockResponse() {
            
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {

                CurrentBlockResponse cbr = xooaClient.getCurrentBlock();

                Assert.IsNotEmpty(cbr.getBlockNumber().ToString());

                Assert.IsNotEmpty(cbr.getCurrentBlockHash());

                Assert.IsNotEmpty(cbr.getPreviousBlockHash());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());
            }
        }

        [Test]
        public void testGetCurrentBlockResponseAsync() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            PendingTransactionResponse ptr = xooaClient.getCurrentBlockAsync();

            ptr.display();

            Assert.IsNotEmpty(ptr.getResultId());

            Assert.IsNotEmpty(ptr.getResultUrl());

        }

        [Test]
        public void testGetBlockByNumber() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                
                BlockResponse br = xooaClient.getBlockByNumber("10");

                Assert.IsNotNull(br.getBlockNumber());

                Assert.IsNotEmpty(br.getDataHash());

                Assert.IsNotEmpty(br.getPreviousHash());

                Assert.IsNotNull(br.getNumberOfTransactions());

                Assert.AreEqual(10, br.getBlockNumber(), "Block Numbers do not match");

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            }
        }

        [Test]
        public void testGetBlockByNumberAsync() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            PendingTransactionResponse ptr = xooaClient.getBlockByNumberAsync("10");
            ptr.display();
            Assert.IsNotEmpty(ptr.getResultId());

            Assert.IsNotEmpty(ptr.getResultUrl());

        }

        [Test]
        public void testGetTransactionByTransactionId() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                
                TransactionResponse br = xooaClient.getTransactionByTransactionId("1159c90618cc535338e8dfb39fc86800405ff9c082f7011808d4307a3104ef8d");

                Assert.IsNotEmpty(br.getTransactionId());

                Assert.IsNotEmpty(br.getCreatorMspId());

                Assert.IsNotEmpty(br.getSmartContract());

                Assert.IsNotEmpty(br.getType());

                Assert.AreEqual("10", br.getTransactionId(), "Transaction IDs do not match");

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            }
        }

        [Test]
        public void testGetTransactionByTransactionIdAsync() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            PendingTransactionResponse ptr = xooaClient.getTransactionByTransactionIdAsync("1159c90618cc535338e8dfb39fc86800405ff9c082f7011808d4307a3104ef8d");

            Assert.IsNotEmpty(ptr.getResultId());

            Assert.IsNotEmpty(ptr.getResultUrl());

        }
    }
}