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

namespace XooaSDK.Client.Response {
    public class TransactionResponse {
        
        private string transactionId;

        private string smartContract;

        private string creatorMspId;

        private List<string> endorserMspId;

        private string type;

        private string createdAt;

        private List<ReadSet> readSets;

        private List<WriteSet> writeSets;

        public void setTransactionId(string transactionId) {
            this.transactionId = transactionId;
        }

        public string getTransactionId() {
            return transactionId;
        }

        public void setSmartContract(string smartContract) {
            this.smartContract = smartContract;
        }

        public string getSmartContract() {
            return smartContract;
        }

        public void setCreatorMspId(string creatorMspId) {
            this.creatorMspId = creatorMspId;
        }

        public string getCreatorMspId() {
            return creatorMspId;
        }

        public void setEndorserMspId(List<string> endorserMspId) {
            this.endorserMspId = endorserMspId;
        }

        public List<string> getEndorserMspId() {
            return endorserMspId;
        }

        public void setType(string type) {
            this.type = type;
        }

        public string getType() {
            return type;
        }

        public void setCreatedAt(string createdAt) {
            this.createdAt = createdAt;
        }

        public string getCreatedAt() {
            return createdAt;
        }

        public void setReadSet(List<ReadSet> readSets) {
            this.readSets = readSets;
        }

        public List<ReadSet> getReadSets() {
            return readSets;
        }

        public void setWriteSets(List<WriteSet> writeSets) {
            this.writeSets = writeSets;
        }

        public List<WriteSet> getWriteSets() {
            return writeSets;
        }

        public TransactionResponse() {

        }

        public TransactionResponse(string transactionId, string smartContract, string creatorMspId, 
                List<string> endorserMspId, string type, string createdAt, List<ReadSet> readSets, List<WriteSet> writeSets) {
            
            this.transactionId = transactionId;
            this.smartContract = smartContract;
            this.creatorMspId = creatorMspId;
            this.endorserMspId = endorserMspId;
            this.type = type;
            this.createdAt = createdAt;
            this.readSets = readSets;
            this.writeSets = writeSets;
        }

        public void display() {
            Console.WriteLine("Transaction Id - " + transactionId);
            Console.WriteLine("SmartContract - " + smartContract);
            Console.WriteLine("Creator MSP ID - " + creatorMspId);
            Console.WriteLine("Endorser MSP IDs - ");

            foreach (string id in endorserMspId) {
                Console.WriteLine(id);
            }

            Console.WriteLine("Type - " + type);
            Console.WriteLine("Created At - " + createdAt);
            Console.WriteLine("Read Sets - ");

            foreach (ReadSet set in readSets) {
                set.display();
            }

            Console.WriteLine("Write Sets - ");

            foreach (WriteSet set in writeSets) {
                set.display();
            }
        }
    }

    public class ReadSet {
        
        private string chaincode;

        private List<ReadSubSet> sets;

        public void setChaincode(string chaincode) {
            this.chaincode = chaincode;
        }

        public string getChaincode() {
            return chaincode;
        }

        public void setSets(List<ReadSubSet> sets) {
            this.sets = sets;
        }

        public List<ReadSubSet> getSets() {
            return sets;
        }

        public ReadSet() {

        }

        public ReadSet(string chaincode, List<ReadSubSet> sets) {
            this.chaincode = chaincode;
            this.sets = sets;
        }

        public void display() {
            Console.WriteLine("\t Chaincode - " + chaincode);
            Console.WriteLine("\t Sets - ");

            foreach (ReadSubSet set in sets) {
                set.display();
            }
        }
    }

    public class ReadSubSet {

        private string key;

        private Version version;

        public void setKey(string key) {
            this.key = key;
        }

        public string getKey() {
            return key;
        }

        public void setVersion(Version version) {
            this.version = version;
        }

        public Version getVersion() {
            return version;
        }

        public ReadSubSet() {

        }

        public ReadSubSet(string key, Version version) {
            this.key = key;
            this.version = version;
        }

        public void display() {
            Console.WriteLine("\t\t Key - " + key);
            Console.WriteLine("\t\t Version - ");
            version.display();
        }
    }

    public class Version {

        private string transactionNumber;

        private string blockNumber;

        public void setTransactionNumber(string transactionNumber) {
            this.transactionNumber = transactionNumber;
        }

        public string getTransactionNumber() {
            return transactionNumber;
        }

        public void setBlockNumber(string blockNumber) {
            this.blockNumber = blockNumber;
        }

        public string getBlockNumber() {
            return blockNumber;
        }

        public Version() {

        }

        public Version(string transactionNumber, string blockNumber) {
            this.transactionNumber = transactionNumber;
            this.blockNumber = blockNumber;
        }

        public void display() {
            Console.WriteLine("\t\t\t Transaction Number - " + transactionNumber);
            Console.WriteLine("\t\t\t BlockNumber - " + blockNumber);
        }
    }

    public class WriteSet {

        private string chaincode;

        private List<WriteSubSet> sets;

        public void setChaincode(string chaincode) {
            this.chaincode = chaincode;
        }

        public string getChaincode() {
            return chaincode;
        }

        public void setSets(List<WriteSubSet> sets) {
            this.sets = sets;
        }

        public List<WriteSubSet> getSets() {
            return sets;
        }

        public WriteSet() {
            
        }

        public WriteSet(string chaincode, List<WriteSubSet> sets) {
            this.chaincode = chaincode;
            this.sets = sets;
        }

        public void display() {
            Console.WriteLine("\t Chaincode - " + chaincode);
            Console.WriteLine("\t Sets - ");

            foreach (WriteSubSet set in sets) {
                set.display();
            }
        }
    }

    public class WriteSubSet {

        private string key;

        private string value;

        private bool isDelete;

        public void setKey(string key) {
            this.key = key;
        }

        public string getKey() {
            return this.key;
        }

        public void setValue(string value) {
            this.value = value;
        }

        public string getValue() {
            return this.value;
        }

        public void setIsDelete(bool isDelete) {
            this.isDelete = isDelete;
        }

        public bool getIsDelete() {
            return this.isDelete;
        }

        public WriteSubSet() {

        }

        public WriteSubSet(string key, string value, bool isDelete) {

            this.key = key;
            this.value = value;
            this.isDelete = isDelete;
        }

        public void display() {
            Console.WriteLine("\t\t Key - " + key);
            Console.WriteLine("\t\t Value - " + value);
            Console.WriteLine("\t\t IsDelted - " + isDelete);
        }
    }
}