/// Xooa C# SDK usage example
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
using XooaSDK.Client;
using XooaSDK.Client.Exception;
using XooaSDK.Client.Util;
using XooaSDK.Client.Request;
using XooaSDK.Client.Response;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace XooaSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            //WebSocket socket = new WebSocket();
            //socket.subscribeAllEvents();

            XooaClient client = new XooaClient();
            client.setApiToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcGlLZXkiOiJIU0E0SkZRLUFHUjQ0NUstSkcwOUMzMi1CUDgyVDRZIiwiQXBpU2VjcmV0IjoiS2JZSzdDVHVaWThZSk9aIiwiUGFzc3BocmFzZSI6IjA1N2M2ODM3NjgyZDBjODBkMTllYTk0NjliYzI0MzczIiwiaWF0IjoxNTQzOTkzNjIzfQ.BkNZ6N5FfjCdYsAOYisFSelUDftDhnY3f8OYf4EgXYc");

            //client.subscribeAllEvents((data) => {
            //    Console.WriteLine("data - ", data.ToString());
            //});

            try {
                Console.WriteLine("----- Start -----");

                Console.WriteLine("----- Validate -----");

                client.validate().display();

                Console.WriteLine();

                Console.WriteLine("----- Current Block -----");

                client.getCurrentBlock().display();

                Console.WriteLine();

                Console.WriteLine("----- Current Block Async -----");

                PendingTransactionResponse currentBlockResponse = client.getCurrentBlockAsync();
                currentBlockResponse.display();

                Console.WriteLine();

                Console.WriteLine("----- Current Block From Result Id -----");

                client.getResultForCurrentBlock(currentBlockResponse.getResultId()).display();

                Console.WriteLine();

                Console.WriteLine("----- Block by Number -----");

                client.getBlockByNumber("10").display();

                Console.WriteLine();

                Console.WriteLine("----- Block by NUmber Async -----");

                PendingTransactionResponse blockByNumber = client.getBlockByNumberAsync("10");
                blockByNumber.display();

                Console.WriteLine();

                Console.WriteLine("----- Block Data from Result Id -----");

                client.getResultForBlockByNumber(blockByNumber.getResultId()).display();

                Console.WriteLine();

                Console.WriteLine("----- Invoke -----");

                string[] invokeargs = {"argsx", "200"};

                client.invoke("set", invokeargs).display();

                Console.WriteLine();

                Console.WriteLine("----- Query -----");

                string[] queryArgs = {"argsx"};

                client.query("get", queryArgs).display();

                Console.WriteLine();

                Console.WriteLine("----- Invoke Async -----");

                string[] invokeargs2 = {"argsx", "400"};

                PendingTransactionResponse invokeResponse = client.invokeAsync("set", invokeargs2);
                invokeResponse.display();
                Thread.Sleep(4000);

                Console.WriteLine();

                Console.WriteLine("----- Invoke from Result Id -----");

                client.getResultForInvoke(invokeResponse.getResultId()).display();

                Console.WriteLine();

                Console.WriteLine("----- Query Async -----");

                PendingTransactionResponse queryResponse = client.queryAsync("get", queryArgs);
                queryResponse.display();

                Console.WriteLine();

                Console.WriteLine("----- Query from Result ID -----");

                client.getResultForQuery(queryResponse.getResultId()).display();

                Console.WriteLine();

                Console.WriteLine("----- Current Identity -----");

                client.currentIdentity().display();

                Console.WriteLine();

                Console.WriteLine("----- Get All Identities -----");

                List<IdentityResponse> identities = client.getIdentities();
                foreach(IdentityResponse identity in identities) {
                    identity.display();
                    Console.WriteLine();
                }

                Console.WriteLine("----- Enroll Identity -----");

                attrs Attrib = new attrs("Name", "Value", false);

                List<attrs> attributes = new List<attrs>();
                attributes.Add(Attrib);

                IdentityRequest idReq = new IdentityRequest("Kavi", "r", false, attributes);

                IdentityResponse newIdentity1 = client.enrollIdentity(idReq);
                newIdentity1.display();

                Console.WriteLine();

                Console.WriteLine("----- Enroll Identity Async -----");

                PendingTransactionResponse pendingIdentity = client.enrollIdentityAsync(idReq);
                pendingIdentity.display();

                Console.WriteLine();

                Console.WriteLine("----- New Identity from Result Id -----");

                IdentityResponse newIdentity2 = client.getResultForIdentity(pendingIdentity.getResultId());
                newIdentity2.display();

                Console.WriteLine();

                Console.WriteLine("----- Regenerate New API Token -----");

                IdentityResponse newTokenId = client.regenerateIdentityApiToken(newIdentity1.getId());
                newTokenId.display();

                Console.WriteLine();

                Console.WriteLine("----- Get Identity -----");

                client.getIdentity(newTokenId.getId()).display();

                Console.WriteLine();

                Console.WriteLine("----- Delete Identity -----");

                string deleted1 = client.deleteIdentity(newIdentity2.getId());
                Console.WriteLine(deleted1);
                string deleted2 = client.deleteIdentity(newIdentity1.getId());
                Console.WriteLine(deleted2);
                
                Console.WriteLine();

                Console.WriteLine("----- End -----");
            } catch (XooaApiException xae) {
                xae.display();
            } catch (XooaRequestTimeoutException xrte) {
                xrte.display();
            }
           //Console.ReadLine();
        }
    }
}
