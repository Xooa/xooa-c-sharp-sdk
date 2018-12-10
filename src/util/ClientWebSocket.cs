using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;
using XooaSDK.Client.Request;
using Newtonsoft.Json.Linq;


namespace XooaSDK.Client.Util {

    public class WebSocket {

        /// <value>Api Token.</value>
        private string apiToken;

        /// <value>Number of Retries.</value>
        private int retries = 10;

        /// <value>Socket IO.</value>
        private Socket socket = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocket"/> class.
        /// </summary>
        /// <param name="ApiToken">ApiToken to Authenticate the user.</param>
        public WebSocket(string apiToken) {

            if(string.IsNullOrEmpty(apiToken))
                throw new ArgumentException("Invalid Api Token/ Api Token needed");
            
            this.apiToken = apiToken;
        }

        /// <summary>
        /// Set the value of Number of Retries.
        /// </summary>
        /// <value>Retries.</value>
        public void setRetries(int retries) {
            this.retries = retries;
        }

        /// <summary>
        /// Subscribe to the events emitted by the block chain.
        /// </summary>
        /// <param name="regex">Regular Expression to define which events to subscribe to.</param>
        /// <param name="callback">Callback function to define the action on the received event.</param>
        public void subscribeEvents(String regex, Action<object> callback) {

            int retryCount = 0;

            string json = "{\"token\" : \"" + apiToken + "\"}";

            var opts = new IO.Options {Path = "/subscribe"};
            socket = IO.Socket("https://api.ci.xooa.io", opts);

            Console.WriteLine(socket.GetHashCode());

            socket.On(Socket.EVENT_CONNECT, (data) => {
                
                Console.WriteLine("Connected");
                Console.WriteLine(data);
                
                socket.Emit("authenticate", JObject.Parse(json));
            });

            socket.On("authenticated",(data) => {
                Console.WriteLine("Authorized");
            });

            socket.On(Socket.EVENT_CONNECT_ERROR, (data) => {   
                
                Console.WriteLine("Connection Error");
		        Console.WriteLine(data);
                
                socket.Emit(Socket.EVENT_RECONNECT);
	        });

            socket.On(Socket.EVENT_RECONNECT, () => {
                
                if (retryCount <= retries) {
                    Console.WriteLine("---Reconnecting---");
                    retryCount++;
                    socket.Connect();
                } else {
                    Console.WriteLine("Exceeded maximum number of retries allowed");
                }
            });

            socket.On("event", (data) =>
            {   
                JObject jsonData = JObject.Parse(data.ToString());

                if (regex == "-1") {
                    Console.WriteLine(data);
                    callback(jsonData);
                } else {
                    Regex re = new Regex(regex);
                    MatchCollection matches = re.Matches(jsonData["eventName"].ToString());

                    if (matches.Count > 0) {
                        Console.WriteLine(data);
                        callback(jsonData);
                    }   
                }
            });
        }

        /// <summary>
        /// UnSubscribe from all the events from the block chain.
        /// </summary>
        public void unsubscribe() {
            socket.Disconnect();
        }
    }
}