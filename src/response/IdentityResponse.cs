using System;
using System.Collections;
using System.Collections.Generic;
using XooaSDK.Client.Response;

namespace XooaSDK.Client.Response {

    public class IdentityResponse {

        /// <value>Identity Name.</value>
        private string identityName;

        /// <value>Access.</value>
        private string access;

        /// <value>CanManageIdentities.</value>
        private bool canManageIdentities;

        /// <value>CreatedAt.</value>
        private string createdAt;

        /// <value>API Token.</value>
        private string apiToken;

        /// <value>Id.</value>
        private string id;

        /// <value>List of Attributes.</value>
        private List<attrs> attrsList;

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
        /// Get the value of createdAt.
        /// </summary>
        /// <value>createdAt.</value>
        public string getCreatedAt() {
            return this.createdAt;
        }

        /// <summary>
        /// Get the value of API Token.
        /// </summary>
        /// <value>API Token.</value>
        public string getApiToken() {
            return this.apiToken;
        }

        /// <summary>
        /// Get the value of Id.
        /// </summary>
        /// <value>Id.</value>
        public string getId() {
            return this.id;
        }

        /// <summary>
        /// Get the List of Attributes.
        /// </summary>
        /// <value>List of Attributes.</value>
        public List<attrs> getAttrsList() {
            return this.attrsList;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityResponse"/> class.
        /// </summary>
        public IdentityResponse() {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityResponse"/> class.
        /// </summary>
        /// <param name="identityName">Identity Name.</param>
        /// <param name="access">Access.</param>
        /// <param name="canManageIdentities">CanManageIdentities.</param>
        /// <param name="createdAt">CreatedAt.</param>
        /// <param name="apiToken">API Token.</param>
        /// <param name="id">Id.</param>
        /// <param name="attrsList">List of Attributes.</param>
        public IdentityResponse(string identityName, string access, bool canManageIdentities, string createdAt,
            string apiToken, string id, List<attrs> attrsList) {

                this.identityName = identityName;
                this.access = access;
                this.canManageIdentities = canManageIdentities;
                this.createdAt = createdAt;
                this.apiToken = apiToken;
                this.id = id;
                this.attrsList = attrsList;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityResponse"/> class.
        /// </summary>
        /// <param name="identityName">Identity Name.</param>
        /// <param name="canManageIdentities">CanManageIdentities.</param>
        /// <param name="createdAt">CreatedAt.</param>
        /// <param name="apiToken">API Token.</param>
        /// <param name="id">Id.</param>
        public IdentityResponse(string identityName, bool canManageIdentities, string createdAt,
            string apiToken, string id) {
                this.identityName = identityName;
                this.canManageIdentities = canManageIdentities;
                this.createdAt = createdAt;
                this.apiToken = apiToken;
                this.id = id;
        }

        /// <summary>
        /// Display the Data about the Response.
        /// </summary>
        public void display() {
            Console.WriteLine("Id - {0}", id);
            Console.WriteLine("Identity Name - {0}", identityName);
            Console.WriteLine("API Token - {0}", apiToken);
            Console.WriteLine("Access - {0}", access);
            Console.WriteLine("canManageIdentities - {0}", canManageIdentities);
            Console.WriteLine("createdAt - {0}", createdAt);

            if (attrsList != null) {
                foreach (attrs attr in attrsList) {
                    attr.display();
                }
            }
        }
    }

    public class attrs {
        
        /// <value>Attribute Name.</value>
        private string name;

        /// <value>Attribute Value.</value>
        private string value;

        /// <value>Attribute certificate.</value>
        private bool ecert;

        /// <summary>
        /// Get the value of Attribute Name.
        /// </summary>
        /// <value>Attribute Name.</value>
        public string getName() {
            return this.name;
        }

        /// <summary>
        /// Get the value of Attribute Value.
        /// </summary>
        /// <value>Attribute Value.</value>
        public string getValue() {
            return this.value;
        }

        /// <summary>
        /// Get the value of Ecert.
        /// </summary>
        /// <value>Attribute Certificate.</value>
        public bool isEcert() {
            return this.ecert;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="attrs"/> class.
        /// </summary>
        public attrs() {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="attrs"/> class.
        /// </summary>
        /// <param name="name">Attribute Name.</param>
        /// <param name="value">Attribute Value.</param>
        /// <param name="ecert">Attribute Certificate.</param>
        public attrs(string name, string value, bool ecert) {
            this.name = name;
            this.value = value;
            this.ecert = ecert;
        }

        /// <summary>
        /// Display the Data about the Response.
        /// </summary>
        public void display() {
            Console.WriteLine("Name - {0}", name);
            Console.WriteLine("Value - {0}", value);
            Console.WriteLine("ecert - {0}", ecert);
        }

        /// <summary>
        /// Overriding of toString method to give json representation.
        /// </summary>
        public string toString() {
            string attributString = "{\"name\" : \"" + this.name 
                + "\", \"ecert\" : " + this.ecert + ", \"value\" : \"" + this.value + "\"}" ;

            return null;
        }
    }
}