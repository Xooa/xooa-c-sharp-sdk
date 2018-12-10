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

    public class PendingTransactionResponse {

        /// <value>Result URL.</value>
        private string resultUrl;

        /// <value>Result Id.</value>
        private string resultId;

        /// <summary>
        /// Get the value of Result URL.
        /// </summary>
        /// <value>Result URL.</value>
        public string getResultUrl() {
            return this.resultUrl;
        }

        /// <summary>
        /// Get the value of Result Id.
        /// </summary>
        /// <value>Result Id.</value>
        public string getResultId() {
            return this.resultId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PendingTransactionResponse"/> class.
        /// </summary>
        public PendingTransactionResponse() {
            this.resultId = null;
            this.resultUrl = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PendingTransactionResponse"/> class.
        /// </summary>
        /// <param name="resultId">Result Id.</param>
        /// <param name="resultUrl">Result URL.</param>
        public PendingTransactionResponse(string resultId, string resultUrl) {
            this.resultId = resultId;
            this.resultUrl = resultUrl;
        }

        /// <summary>
        /// Display the Data about the Response.
        /// </summary>
        public void display() {
            Console.WriteLine("Result Url - {0}", resultUrl);
            Console.WriteLine("Result Id - {0}", resultId);
        }
    }
}