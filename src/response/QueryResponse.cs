using System;

namespace XooaSDK.Client.Response {

    public class QueryResponse {
        
        /// <value>Transaction Payload.</value>
        private String payload;

        /// <summary>
        /// Get the value of Transaction Payload.
        /// </summary>
        /// <value>Transaction Payload.</value>
        public string getPayload() {
            return this.payload;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResponse"/> class.
        /// </summary>
        public QueryResponse() {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResponse"/> class.
        /// </summary>
        /// <param name="payload">Transaction Payload.</param>
        public QueryResponse(string payload) {
            this.payload = payload;
        }
        
        /// <summary>
        /// Display the Data about the Response.
        /// </summary>
        public void display() {
            Console.WriteLine("Payload - {0}", payload);
        }
    }
}