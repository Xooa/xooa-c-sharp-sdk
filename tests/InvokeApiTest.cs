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

                //Assert.AreEqual(typeof(InvokeResponse), invokeResponse.GetType());

                Assert.IsNotEmpty(invokeResponse.getTxnId());

                Assert.AreEqual("9000", invokeResponse.getPayload());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());
                
                Assert.IsNotEmpty(xae.getErrorMessage());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());
            }
        }

        [Test]
        public void testInvokeTimeout() {

            string functionName = "set";

            string[] args = {"args1", "9000"};

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                InvokeResponse invokeResponse = xooaClient.invoke(functionName, args, "200");

                //Assert.AreEqual(typeof(InvokeResponse), invokeResponse.GetType());

                Assert.IsNotEmpty(invokeResponse.getTxnId());

                Assert.AreEqual("9000", invokeResponse.getPayload());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());
                
                Assert.IsNotEmpty(xae.getErrorMessage());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());
            }
        }

        [Test]
        public void testInvokeAsync() {

            string functionName = "set";

            string[] args = {"args1", "350"};

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {

                PendingTransactionResponse pendingResponse = xooaClient.invokeAsync(functionName, args);

                //Assert.AreEqual(typeof(PendingTransactionResponse), pendingResponse.GetType());

                Assert.IsNotEmpty(pendingResponse.getResultId());

                Assert.IsNotEmpty(pendingResponse.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorMessage());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());
            }
        }
    }
}