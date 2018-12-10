using System;
using System.Collections;
using System.Collections.Generic;
using RestSharp;

namespace XooaSDK.Client.Util {

    public class Request {

        // Creates and sets up a RestRequest prior to a call.
        public static RestRequest PrepareRequest(
            String path, RestSharp.Method method, List<KeyValuePair<String, String>> queryParams, 
            Object postBody,
            Dictionary<String, String> headerParams, Dictionary<String, String> formParams,
            Dictionary<String, String> pathParams, String contentType)
        {
            var request = new RestRequest(path, method);

            request.AddParameter("content-type", contentType);
            // add path parameter, if any
            if (pathParams != null) {
                foreach (var param in pathParams)
                    request.AddParameter(param.Key, param.Value, ParameterType.UrlSegment);
            }
            
            // add header parameter, if any
            if (headerParams != null) {
                foreach(var param in headerParams)
                    request.AddHeader(param.Key, param.Value);
            }
            
            // add query parameter, if any
            if (queryParams != null) {
                foreach(var param in queryParams)
                    request.AddQueryParameter(param.Key, param.Value);
            }
            
            // add form parameter, if any
            if (formParams != null) {
                foreach(var param in formParams)
                    request.AddParameter(param.Key, param.Value);
            }

            // http body (model or byte[]) parameter
            if (postBody != null)
            {   
                request.AddParameter(contentType, postBody, ParameterType.RequestBody);
            }

            return request;
        }
    }
}