using System;

namespace XooaSDK.Client.Response {

    public class CurrentBlockResponse {

        /// <value>Current Block Hash.</value>
        private string currentBlockHash;

        /// <value>Previous Block Hash.</value>
        private string previousBlockHash;

        /// <value>Block Number.</value>
        private int blockNumber;

        /// <summary>
        /// Get the value of Current Block Hash.
        /// </summary>
        /// <value>Current Block Hash.</value>
        public string getCurrentBlockHash() {
            return this.currentBlockHash;
        }

        /// <summary>
        /// Get the value of Previous Block Hash.
        /// </summary>
        /// <value>Previous Block Hash.</value>
        public string getPreviousBlockHash() {
            return this.previousBlockHash;
        }
    
        /// <summary>
        /// Get the value of Block Number.
        /// </summary>
        /// <value>Block Number.</value>
        public int getBlockNumber() {
            return this.blockNumber;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentBlockResponse"/> class.
        /// </summary>
        public CurrentBlockResponse() {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentBlockResponse"/> class.
        /// </summary>
        /// <param name="CurrentBlockHash">Current Block Hash.</param>
        /// <param name="PreviousBlockHash">Previous Block Hash.</param>
        /// <param name="BlockNumber">Block Number.</param>
        public CurrentBlockResponse(string currentBlockHash, string previousBlockHash, int blockNumber) {
            this.currentBlockHash = currentBlockHash;
            this.previousBlockHash = previousBlockHash;
            this.blockNumber = blockNumber;
        }

        /// <summary>
        /// Display the Data about the Response.
        /// </summary>
        public void display() {
            Console.WriteLine("Current Block Number - {0}", blockNumber);
            Console.WriteLine("Current Block Hash - {0}", currentBlockHash);
            Console.WriteLine("Previous Block Hash - {0}", previousBlockHash);
        }
    }
}