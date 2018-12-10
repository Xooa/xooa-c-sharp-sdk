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