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

    public class BlockResponse {

        /// <value>Previous Block Hash.</value>
        private string previousHash;

        /// <value>Current Data Hash.</value>
        private string dataHash;

        /// <value>Current Block Number.</value>
        private int blockNumber;

        /// <value>Total Number of Transactions in the block.</value>
        private int numberOfTransactions;

        /// <summary>
        /// Get the value of Previous Block Hash.
        /// </summary>
        /// <value>Previous Block Hash.</value>
        public string getPreviousHash() {
            return this.previousHash;
        }

        /// <summary>
        /// Get the value of Current Data Hash.
        /// </summary>
        /// <value>Current Data Hash.</value>
        public string getDataHash() {
            return this.dataHash;
        }

        /// <summary>
        /// Get the value of Current Block Number.
        /// </summary>
        /// <value>Current Block Number.</value>
        public int getBlockNumber() {
            return this.blockNumber;
        }

        /// <summary>
        /// Get the value of Total Number of Transactions in the block.
        /// </summary>
        /// <value>Total Number of Transactions.</value>
        public int getNumberOfTransactions() {
            return this.numberOfTransactions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XooaApiException"/> class.
        /// </summary>
        public BlockResponse() {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockResponse"/> class.
        /// </summary>
        /// <param name="PreviousHash">Previous Block Hash.</param>
        /// <param name="DataHash">Current Data Hash.</param>
        /// <param name="BlockNumber">Current Block Number.</param>
        /// <param name="NumberOfTransactions">Total Number of Transactions in the block.</param>
        public BlockResponse(string previousHash, string dataHash, int blockNumber, int numberOfTransactions) {
            this.previousHash = previousHash;
            this.dataHash = dataHash;
            this.blockNumber = blockNumber;
            this.numberOfTransactions = numberOfTransactions;
        }

        /// <summary>
        /// Display the Data about the Response.
        /// </summary>
        public void display() {
            Console.WriteLine("Block Number - {0}", blockNumber);
            Console.WriteLine("Number of Transactions - {0}", numberOfTransactions);
            Console.WriteLine("Data Hash - {0}", dataHash);
            Console.WriteLine("Previous Hash - {0}", previousHash);
        }
    }
}