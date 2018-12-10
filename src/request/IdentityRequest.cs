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
using System.Collections;
using System.Collections.Generic;
using XooaSDK.Client.Response;

namespace XooaSDK.Client.Request {

    public class IdentityRequest {
        
        /// <value>Identity Name.</value>
        private string identityName;

        /// <value>Access.</value>
        private string access;

        /// <value>CanManageIdentities.</value>
        private bool canManageIdentities;

        /// <value>List of Attributes.</value>
        private List<XooaSDK.Client.Response.attrs> attributes;

        /// <summary>
        /// Get the value of Identity Name.
        /// </summary>
        /// <value>Identity Name.</value>
        public string getIdentityName() {
            return this.identityName;
        }

        /// <summary>
        /// Get the value of Access available.
        /// </summary>
        /// <value>Access.</value>
        public string getAccess() {
            return this.access;
        }

        /// <summary>
        /// Get the value of CanManageIdentities.
        /// </summary>
        /// <value>CanManageIdentities.</value>
        public bool getCanManageIdentities() {
            return this.canManageIdentities;
        }

        /// <summary>
        /// Get the List of Attributes.
        /// </summary>
        /// <value>List of Attributes.</value>
        public List<attrs> getAttrsList() {
            return this.attributes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityRequest"/> class.
        /// </summary>
        /// <param name="identityName">Identity Name.</param>
        /// <param name="access">Access.</param>
        /// <param name="canManageIdentities">CanManageIdentities.</param>
        /// <param name="attrsList">List of Attributes.</param>
        public IdentityRequest(string identityName, string access, bool canManageIdentities, List<attrs> attributes) {

            this.identityName = identityName;
            this.access = access;
            this.canManageIdentities = canManageIdentities;
            this.attributes = attributes;
        }

        /// <summary>
        /// Overriding of toString method to give json representation.
        /// </summary>
        public string toString() {
            
            string json = "{\"IdentityName\" : \"" 
                + this.identityName + "\", \"access\" : \"" 
                + access + "\", \"canManageIdentities\" : " + canManageIdentities.ToString().ToLower() 
                + ", \"Attrs\" : [";

            foreach(attrs attribute in this.attributes) {
                json = string.Concat(json, attribute.toString());
                //Console.WriteLine(attribute.toString());
                json = string.Concat(json, ",");
            }

            json = json.Substring(0, json.Length - 1);
            
            json = string.Concat(json, "]}");
            
            return json;
        }
    }
}