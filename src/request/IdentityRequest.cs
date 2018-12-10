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
                + access + "\", \"canManageIdentities\" : " + canManageIdentities 
                + "\"Attrs\" : [";

            foreach(attrs attribute in this.attributes) {
                json = string.Concat(json, attribute.toString());
                json = string.Concat(json, ",");
            }

            json.Substring(0, json.Length - 1);
            
            string.Concat(json, "]}");
            
            return null;
        }
    }
}