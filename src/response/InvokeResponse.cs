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

namespace XooaSDK.Client.Response {

    public class InvokeResponse {

        /// <value>Transaction Id.</value>
        private string txnId;

        /// <value>Transaction Payload.</value>
        private string payload;

        /// <summary>
        /// Get the value of Transaction Id.
        /// </summary>
        /// <value>Transaction Id.</value>
        public string getTxnId() {
            return this.txnId;
        }

        /// <summary>
        /// Get the value of Transaction Payload.
        /// </summary>
        /// <value>Transaction Payload.</value>
        public string getPayload() {
            return this.payload;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeResponse"/> class.
        /// </summary>
        public InvokeResponse() {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeResponse"/> class.
        /// </summary>
        /// <param name="txnId">Transaction Id.</param>
        /// <param name="payload">Transaction Payload.</param>
        public InvokeResponse(string txnId, string payload) {
            this.txnId = txnId;
            this.payload = payload;
        }
        
        /// <summary>
        /// Display the Data about the Response.
        /// </summary>
        public void display() {
            Console.WriteLine("Transaction Id - {0}", txnId);
            Console.WriteLine("Payload - {0}", payload);
        }
    }
}