using System;                
using NUnit.Framework;
using XooaSDK.Client;
using XooaSDK.Client.Api;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Response;
using XooaSDK.Client.Util;

namespace XooaSDK.Test.Api {

    [TestFixture]
    public class QueryApiTest {

        [Test]
        public void testQuery() {

            string functionName = "get";

            string args = "args1";

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                QueryResponse queryResponse = xooaClient.query(functionName, args, "3000");

                //Assert.AreEqual(typeof(QueryResponse), queryResponse.GetType());

                Assert.IsNotEmpty(queryResponse.getPayload());

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
        public void testQueryTimeout() {

            string functionName = "get";

            string args = "args1";

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                QueryResponse queryResponse = xooaClient.query(functionName, args, "200");

                //Assert.AreEqual(typeof(QueryResponse), queryResponse.GetType());

                Assert.IsNotEmpty(queryResponse.getPayload());

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
        public void testQueryAsync() {

            string functionName = "set";

            string args = "args1";

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {

                PendingTransactionResponse pendingResponse = xooaClient.queryAsync(functionName, args);

                //Assert.AreEqual(typeof(PendingTransactionResponse), pendingResponse.GetType());

                Assert.IsNotEmpty(pendingResponse.getResultId());

                Assert.IsNotEmpty(pendingResponse.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());
                
                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }
    }
}