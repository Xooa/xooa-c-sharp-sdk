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

    public class XooaApiException : System.Exception {

        /// <value>Error Code.</value>
        private int errorCode;

        /// <value>Error Message.</value>
        private string errorMessage;

        /// <summary>
        /// Get the value of Error Code.
        /// </summary>
        /// <value>Error Code.</value>
        public int getErrorCode() {
            return this.errorCode;
        }

        /// <summary>
        /// Get the value of Error Message.
        /// </summary>
        /// <value>Error Message.</value>
        public string getErrorMessage() {
            return this.errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XooaApiException"/> class.
        /// </summary>
        /// <param name="ErrorCode">Error Code given by the API.</param>
        /// <param name="ErrorMessage">Error Message given by the API.</param>
        public XooaApiException(int errorCode, string errorMessage) {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }
        
        /// <summary>
        /// Display the Data about the Exception.
        /// </summary>
        public void display() {
            Console.WriteLine("Error Code - {0}", errorCode);
            Console.WriteLine("Error Message - {0}", errorMessage);
        }
    }
}