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

            string resultId = "fc851032-ddad-4676-8be1-0025a4d251c3";

            try {
                QueryResponse queryResponse = xooaClient.getResultForQuery(resultId);

                Assert.IsNotEmpty(queryResponse.getPayload());

                Assert.IsNotEmpty(queryResponse.getPayload());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.AreEqual(resultId, xrte.getResultId());

            }
        }

        [Test]
        public void testGetResultForInvoke() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);
            
            string resultId = "63dc6290-053d-4469-8461-dcdf705d0943";

            try {
                InvokeResponse invokeResponse = xooaClient.getResultForInvoke(resultId);

                Assert.IsNotEmpty(invokeResponse.getPayload());

                Assert.IsNotEmpty(invokeResponse.getTxnId());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());

            }
        }

        [Test]
        public void testGetResultForIdentity() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "0a07e7e3-d87f-44d9-b9a8-d4bc3612b91e";

            try {
                IdentityResponse identityResponse = xooaClient.getResultForIdentity(resultId);

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotEmpty(identityResponse.getCanManageIdentities().ToString());

                Assert.IsNotEmpty(identityResponse.getId());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());
                 
            }
        }

        [Test]
        public void testGetResultForCurrentBlock() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "75b9f849-389e-4a2d-9717-5f47cc3a688b";

            try {
                CurrentBlockResponse currentBlock = xooaClient.getResultForCurrentBlock(resultId);

                Assert.IsNotEmpty(currentBlock.getBlockNumber().ToString());

                Assert.IsNotEmpty(currentBlock.getCurrentBlockHash());

                Assert.IsNotEmpty(currentBlock.getPreviousBlockHash());
            
            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());

            }
        }

        [Test]
        public void testGetResultForBlockByNumber() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string resultId = "07ce7042-67d8-431f-84a8-fdb3f4145225";

            try {

                BlockResponse blockResponse = xooaClient.getResultForBlockByNumber(resultId);

                Assert.IsNotEmpty(blockResponse.getBlockNumber().ToString());

                Assert.IsNotEmpty(blockResponse.getDataHash());

                Assert.IsNotEmpty(blockResponse.getNumberOfTransactions().ToString());

                Assert.IsNotEmpty(blockResponse.getPreviousHash());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultUrl());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.AreEqual(resultId, xrte.getResultId());

            }
        }
    }
}