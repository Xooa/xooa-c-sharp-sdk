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