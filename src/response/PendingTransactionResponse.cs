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