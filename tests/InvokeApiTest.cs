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
    public class InvokeApiTest {

        [Test]
        public void testInvoke() {

            string functionName = "set";

            string[] args = {"args1", "9000"};

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                InvokeResponse invokeResponse = xooaClient.invoke(functionName, args);

                Assert.IsNotEmpty(invokeResponse.getTxnId());

                Assert.IsNotEmpty(invokeResponse.getPayload());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            }
        }

        [Test]
        public void testInvokeAsync() {

            string functionName = "set";

            string[] args = {"args1", "350"};

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            PendingTransactionResponse pendingResponse = xooaClient.invokeAsync(functionName, args);

            pendingResponse.display();
            
            Assert.IsNotEmpty(pendingResponse.getResultId());

            Assert.IsNotEmpty(pendingResponse.getResultUrl());
        }
    }
}