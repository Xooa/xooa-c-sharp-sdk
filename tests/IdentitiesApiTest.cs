using System;             
using System.Collections;
using System.Collections.Generic;   
using NUnit.Framework;
using XooaSDK.Client;
using XooaSDK.Client.Api;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Response;
using XooaSDK.Client.Request;
using XooaSDK.Client.Util;

namespace XooaSDK.Test.Api {

    [TestFixture]
    public class IdentitiesApiTest {
        
        [Test]
        public void testCurrentIdentity() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                IdentityResponse currentIdentity = xooaClient.currentIdentity();

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), currentIdentity.GetType());

                Assert.IsNotEmpty(currentIdentity.getId());

                Assert.IsNotEmpty(currentIdentity.getApiToken());

                Assert.IsNotEmpty(currentIdentity.getAccess());

                Assert.IsNotEmpty(currentIdentity.getIdentityName());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testCurrentIdentityTimeout() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                IdentityResponse currentIdentity = xooaClient.currentIdentity("200");

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), currentIdentity.GetType());

                Assert.IsNotEmpty(currentIdentity.getId());

                Assert.IsNotEmpty(currentIdentity.getApiToken());

                Assert.IsNotEmpty(currentIdentity.getAccess());

                Assert.IsNotEmpty(currentIdentity.getIdentityName());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetIdentities() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {

                List<IdentityResponse> identities = xooaClient.getIdentities();

                IdentityResponse identity = identities.GetEnumerator().Current;

                Assert.IsNotEmpty(identities);

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identity.GetType());

                Assert.IsNotEmpty(identity.getApiToken());

                Assert.IsNotEmpty(identity.getId());

                Assert.IsNotEmpty(identity.getIdentityName());

                Assert.IsNotEmpty(identity.getAccess());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetIdentitiesTimeout() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {

                List<IdentityResponse> identities = xooaClient.getIdentities("200");

                IdentityResponse identity = identities.GetEnumerator().Current;

                Assert.IsNotEmpty(identities);

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identity.GetType());

                Assert.IsNotEmpty(identity.getApiToken());

                Assert.IsNotEmpty(identity.getId());

                Assert.IsNotEmpty(identity.getIdentityName());

                Assert.IsNotEmpty(identity.getAccess());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testEnrollIdentity() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                IdentityRequest request = new IdentityRequest("Kavi", "r", true, new List<attrs>());

                IdentityResponse identityResponse = xooaClient.enrollIdentity(request);

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identityResponse.GetType());

                Assert.IsNotEmpty(identityResponse.getId());

                Assert.IsNotEmpty(identityResponse.getApiToken());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotEmpty(identityResponse.getAccess());
                
            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testEnrollIdentityTimeout() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                IdentityRequest request = new IdentityRequest("Kavi", "r", true, new List<attrs>());

                IdentityResponse identityResponse = xooaClient.enrollIdentity(request, "200");

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identityResponse.GetType());

                Assert.IsNotEmpty(identityResponse.getId());

                Assert.IsNotEmpty(identityResponse.getApiToken());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotEmpty(identityResponse.getAccess());
                
            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }
        
        [Test]
        public void testEnrollIdentityAsync() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                IdentityRequest request = new IdentityRequest("Kavi", "r", true, new List<attrs>());

                PendingTransactionResponse response = xooaClient.enrollIdentityAsync(request);

                //Assert.AreEqual(typeof(PendingTransactionResponse).ToString(), response.GetType());

                Assert.IsNotEmpty(response.getResultId());

                Assert.IsNotEmpty(response.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testRegenerateIdentityApiToken() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string identityId = XooaConstants.IDENTITY_ID;

            try {
                IdentityResponse identityResponse = xooaClient.regenerateIdentityApiToken(identityId, "3000");

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identityResponse.GetType());

                Assert.IsNotEmpty(identityResponse.getId());

                Assert.IsNotEmpty(identityResponse.getApiToken());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotEmpty(identityResponse.getAccess());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testRegenerateIdentityApiTokenTimeout() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string identityId = XooaConstants.IDENTITY_ID;

            try {
                IdentityResponse identityResponse = xooaClient.regenerateIdentityApiToken(identityId, "200");

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identityResponse.GetType());

                Assert.IsNotEmpty(identityResponse.getId());

                Assert.IsNotEmpty(identityResponse.getApiToken());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotEmpty(identityResponse.getAccess());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetIdentity() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string identityId = XooaConstants.IDENTITY_ID;

            try {
                IdentityResponse identityResponse = xooaClient.getIdentity(identityId, "3000");

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identityResponse.GetType());

                Assert.IsNotEmpty(identityResponse.getId());

                Assert.IsNotEmpty(identityResponse.getApiToken());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotEmpty(identityResponse.getAccess());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testGetIdentityTimeout() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string identityId = XooaConstants.IDENTITY_ID;

            try {
                IdentityResponse identityResponse = xooaClient.getIdentity(identityId, "200");

                //Assert.AreEqual(typeof(IdentityResponse).ToString(), identityResponse.GetType());

                Assert.IsNotEmpty(identityResponse.getId());

                Assert.IsNotEmpty(identityResponse.getApiToken());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotEmpty(identityResponse.getAccess());

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testDeleteIdentity() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string identityId = "d818ffe2-4e2a-4d4f-b4b0-a03775f88f79";

            try {
                string response = xooaClient.deleteIdentity(identityId, "3000");

                Assert.AreEqual("True", response);

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }

        [Test]
        public void testDeleteIdentityTimeout() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            string identityId = "d818ffe2-4e2a-4d4f-b4b0-a03775f88f79";

            try {
                string response = xooaClient.deleteIdentity(identityId, "200");

                Assert.AreEqual("True", response);

            } catch (XooaRequestTimeoutException xrte) {

                //Assert.AreEqual(typeof(XooaRequestTimeoutException).ToString(), xrte.GetType());

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());

            } catch (XooaApiException xae) {

                //Assert.AreEqual(typeof(XooaApiException).ToString(), xae.GetType());

                Assert.IsNotEmpty(xae.getErrorCode().ToString());

                Assert.IsNotEmpty(xae.getErrorMessage());
            }
        }
    }
}