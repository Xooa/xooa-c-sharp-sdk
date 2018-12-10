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
using System.Net;
using System.Collections;
using System.Collections.Generic;   
using NUnit.Framework;
using XooaSDK.Client;
using XooaSDK.Client.Api;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Response;
using XooaSDK.Client.Request;
using XooaSDK.Client.Util;
using RestSharp;
using Moq;

namespace XooaSDK.Test.Api {

    [TestFixture]
    public class IdentitiesApiTest {
        
        [Test]
        public void testCurrentIdentity() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {

                IdentityResponse currentIdentity = xooaClient.currentIdentity();

                Assert.IsNotEmpty(currentIdentity.getId());

                Assert.IsNotEmpty(currentIdentity.getIdentityName());

                Assert.IsNotNull(currentIdentity.getCanManageIdentities());

                Assert.IsNotEmpty(currentIdentity.getCreatedAt());

                Assert.IsNotEmpty(currentIdentity.getAccess());

                Assert.IsNotEmpty(currentIdentity.getAttrsList());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());
            }
        }

        [Test]
        public void testGetIdentities() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {

                List<IdentityResponse> identities = xooaClient.getIdentities();

                IdentityResponse identity = identities[0];

                Assert.IsNotEmpty(identities);

                Assert.IsNotEmpty(identity.getId());

                Assert.IsNotEmpty(identity.getIdentityName());

                Assert.IsNotNull(identity.getCanManageIdentities());

                Assert.IsNotEmpty(identity.getCreatedAt());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());
            }
        }

        [Test]
        public void testEnrollIdentity() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                var attrs = new List<attrs>();
                attrs.Add(new attrs("sample", "value", false));

                IdentityRequest request = new IdentityRequest("Kavi", "r", true, attrs);

                IdentityResponse identityResponse = xooaClient.enrollIdentity(request);

                identityResponse.display();

                Assert.IsNotEmpty(identityResponse.getId());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotNull(identityResponse.getCanManageIdentities());

                Assert.IsNotEmpty(identityResponse.getCreatedAt());

                Assert.IsNotEmpty(identityResponse.getAccess());

                Assert.IsNotEmpty(identityResponse.getAttrsList());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());
            }
        }

        [Test]
        public void testEnrollIdentityAsync() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            var attrs = new List<attrs>();
            attrs.Add(new attrs("sample", "value", false));

            IdentityRequest request = new IdentityRequest("Kavi", "r", true, attrs);

            PendingTransactionResponse response = xooaClient.enrollIdentityAsync(request);
            
            response.display();
            
            Assert.IsNotEmpty(response.getResultId());

            Assert.IsNotEmpty(response.getResultUrl());
        }

        [Test]
        public void testRegenerateIdentityApiToken() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {

                IdentityResponse identityResponse = xooaClient.regenerateIdentityApiToken("3c7d983f-6f9c-4599-802f-cd07dc977a7d");

                Assert.IsNotEmpty(identityResponse.getId());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotNull(identityResponse.getCanManageIdentities());

                Assert.IsNotEmpty(identityResponse.getCreatedAt());

                Assert.IsNotEmpty(identityResponse.getAccess());

                Assert.IsNotEmpty(identityResponse.getAttrsList());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());
            }
        }

        [Test]
        public void testGetIdentity() {

            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {

                IdentityResponse identityResponse = xooaClient.getIdentity("3c7d983f-6f9c-4599-802f-cd07dc977a7d");

                Assert.IsNotEmpty(identityResponse.getId());

                Assert.IsNotEmpty(identityResponse.getIdentityName());

                Assert.IsNotNull(identityResponse.getCanManageIdentities());

                Assert.IsNotEmpty(identityResponse.getCreatedAt());

                Assert.IsNotEmpty(identityResponse.getAccess());

                Assert.IsNotEmpty(identityResponse.getAttrsList());

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());
            }
        }

        [Test]
        public void testDeleteIdentity() {
            XooaClient xooaClient = new XooaClient();
            xooaClient.setApiToken(XooaConstants.API_TOKEN);

            try {
                
                string deleteResponse = xooaClient.deleteIdentity("3c7d983f-6f9c-4599-802f-cd07dc977a7d");

                Assert.AreEqual("True", deleteResponse);

            } catch (XooaRequestTimeoutException xrte) {

                Assert.IsNotEmpty(xrte.getResultId());

                Assert.IsNotEmpty(xrte.getResultUrl());
            }
        }
    }
}