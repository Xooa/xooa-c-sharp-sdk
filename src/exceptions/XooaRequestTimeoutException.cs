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

namespace XooaSDK.Client.Exception {

    public class XooaRequestTimeoutException : System.Exception {

        /// <value>Result Url.</value>
        private string resultUrl;

        /// <value>Result Id.</value>
        private string resultId;

        /// <summary>
        /// Get the value of Result Url.
        /// </summary>
        /// <value>Result Url.</value>
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
        /// Initializes a new instance of the <see cref="XooaRequestTimeoutException"/> class.
        /// </summary>
        /// <param name="ResultId">Result Id given by the API.</param>
        /// <param name="ResultUrl">Result Url given by the API.</param>
        public XooaRequestTimeoutException(string resultId, string resultUrl) {
            this.resultId = resultId;
            this.resultUrl = resultUrl;
        }

        /// <summary>
        /// Display the Data about the Exception.
        /// </summary>
        public void display() {
            Console.WriteLine("Result Id - {0}", resultId);
            Console.WriteLine("Result Url - {0}", resultUrl);
        }
    }
}