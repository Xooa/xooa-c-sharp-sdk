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
        public void subscribeEvents(Action<object> callback) {

            int retryCount = 0;

            string json = "{\"token\" : \"" + apiToken + "\"}";

            var opts = new IO.Options {Path = "/subscribe"};
            socket = IO.Socket("https://api.staging.xooa.io", opts);

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

            socket.On(Socket.EVENT_MESSAGE, (data) => {
                Console.WriteLine("Message");
                Console.WriteLine(data);
            });

            socket.On("event", (data) =>
            {   
                Console.WriteLine("event");
                Console.WriteLine(data);
                JObject jsonData = JObject.Parse(data.ToString());
                
                    Console.WriteLine(data);
                    callback(jsonData);
                
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