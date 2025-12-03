using System;
using System.Collections.Generic;
using System.Text;

namespace Yokinsoft.Salesforce.MCE
{
    public class Contacts : APIClientBase
    {
        public Contacts(AccessToken accessToken) : base(accessToken)
        {
        }
        SchemaItemListContainer GetSchemaCollection()
        {
            return Get<SchemaItemListContainer>("/contacts/v1/schema");
        }
        public object GetContact( string contactKey)
        {
            return Get<object>($"/contacts/v1/contacts/{contactKey}");
        }
        public ContactsOperationStatusResponse CreateContact( string contactKey, IEnumerable<ContactAttributeSet> attributeSets )
        {
            return Post<ContactsOperationStatusResponse>("/contacts/v1/contacts", new Dictionary<string, object>
            {
                { "attributeSets",attributeSets }, {"contactKey", contactKey}
            });
        }
        public ContactsOperationStatusResponse CreateContact(long contactID, IEnumerable<ContactAttributeSet> attributeSets)
        {
            return Post<ContactsOperationStatusResponse>("/contacts/v1/contacts", new Dictionary<string, object>
            {
                { "attributeSets",attributeSets }, {"contactID", contactID}
            });
        }

        public ContactsOperationStatusResponse UpdateContact( string contactKey, IEnumerable<ContactAttributeSet> attributeSets )
        {
            return Patch<ContactsOperationStatusResponse>("/contacts/v1/contacts", new Dictionary<string, object>
            {
                { "attributeSets",attributeSets }, {"contactKey", contactKey}
            });
        }
        public ContactsOperationStatusResponse UpdateContact(long contactID, IEnumerable<ContactAttributeSet> attributeSets)
        {
            return Patch<ContactsOperationStatusResponse>("/contacts/v1/contacts", new Dictionary<string, object>
            {
                { "attributeSets",attributeSets }, {"contactID", contactID}
            });
        }

        void GetContantCount(string queryFilter)
        {
            throw new NotImplementedException();
        }

        void InsertAttributeValues()
        {
            throw new NotImplementedException();
        }
        void UpdateAttributeValues()
        {
            throw new NotImplementedException();
        }

        public ContactKeyFromEmailAddressResult GetContactKeyForEmailAddress(IEnumerable<string> channelAddressList, int maximumCount = -1)
        {
            var data = new Dictionary<string, object>
            {
                {"channelAddressList", channelAddressList }
            };
            if (maximumCount > 0) data["maximumCount"] = maximumCount;
            return Post<ContactKeyFromEmailAddressResult>("/contacts/v1/addresses/email/search", data);
        }

        void GetContactDeleteOperations()
        {
            throw new NotImplementedException();
        }

        void SearchAttributeSetsByName(string name)
        {
            throw new NotImplementedException();
        }

        void SearchAttributeGroupsBySchema(string schemaId)
        {
            throw new NotImplementedException();
        }

        void GetCustomObjectInfo(string id)
        {
            throw new NotImplementedException();
        }

        void SearchAttributeGroupIDBySchema(string schemaId, string id, string key, string name)
        {
            throw new NotImplementedException();
        }

        void GetAllAttributeSetDefinitions()
        {
            throw new NotImplementedException();
        }

        void GetAttributeSetDefinitionById(string id)
        {
            throw new NotImplementedException();
        }

        void GetOrCreateContactsByKeyAndType()
        {
            throw new NotImplementedException();
        }

        void ConfigureSettingsForDeletingContacts()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactIds"></param>
        /// <param name="deleteOperationType">Type of delete operation to perform. Specify ContactAndAttributes to delete a contact from the entire account as long as no other channel references that contact. AttributesOnly is reserved for future use.</param>
        public ContactsOperationResponse DeleteContactsByID(IList<long> contactIds, string deleteOperationType = "ContactAndAttributes")
        {
            if( contactIds == null || contactIds.Count == 0)
                throw new ArgumentNullException( nameof( contactIds ), "Contact IDs list cannot be null or empty." );
            if( contactIds.Count > 50 )
                throw new ArgumentOutOfRangeException( nameof( contactIds ), "Maximum of 50 contact IDs can be deleted in a single request." );
            return Post<ContactsOperationResponse>("/contacts/v1/contacts/actions/delete?type=ids", new
            {
                values = contactIds,
                DeleteOperationType = deleteOperationType
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactKeys"></param>
        /// <param name="deleteOperationType"></param>
        public ContactsOperationResponse DeleteContactsByKey(IList<string> contactKeys, string deleteOperationType = "ContactAndAttributes")
        {
            if (contactKeys == null || contactKeys.Count == 0)
                throw new ArgumentNullException(nameof(contactKeys), "Contact IDs list cannot be null or empty.");
            if (contactKeys.Count > 50)
                throw new ArgumentOutOfRangeException(nameof(contactKeys), "Maximum of 50 contact IDs can be deleted in a single request.");
            return Post<ContactsOperationResponse>("/contacts/v1/contacts/actions/delete?type=keys", new
            {
                values = contactKeys,
                DeleteOperationType = deleteOperationType
            });

        }
        void DeleteContactsByListReference()
        {
            throw new NotImplementedException();
        }

        void GetDeleteOptions()
        {
            throw new NotImplementedException();
        }
        ContactsOperationResponse GetStatusOfContactDeleteOperation(long operationID)
        {
            return Get<ContactsOperationResponse>($"/contacts/v1/contacts/actions/delete/status?operationID={operationID}");
        }
        void GetContactDeleteRequestsDetails()
        {
            throw new NotImplementedException();
        }
        void GetContactDeleteRequestsSummary()
        {
            throw new NotImplementedException();
        }
        void RetryFailedDeleteRequest(long operationId)
        {
            throw new NotImplementedException();
        }
        void RestrictContactsByID(IEnumerable<long> contactIds)
        {
            throw new NotImplementedException();
        }
        void RestrictContactsByKey(IEnumerable<string> contactKeys)
        {
            throw new NotImplementedException();
        }
        void RestrictContactsBByListReference()
        {
            throw new NotImplementedException();
        }
        void GetStatusOfContactRestrictOperation(long operationId)
        {
            throw new NotImplementedException();
        }
        void RetryFailedRestrictRequest(long operationId)
        {
            throw new NotImplementedException();
        }
        void RemoveContactFromJourney(string contactKey, string definitionKey, IEnumerable<string> versions = null)
        {
            throw new NotImplementedException();
        }
        void GetContactExitStatus(string contactKey, string definitionKey, IEnumerable<string> versions = null)
        {
            throw new NotImplementedException();
        }
        void GetListOfJourneysContactIsIn(IEnumerable<string> contactKeyList)
        {
            throw new NotImplementedException();
        }
        void AddContactPreferences()
        {
            throw new NotImplementedException();
        }
        void CreatePopulation()
        {
            throw new NotImplementedException();
        }
        void AddContactPreferencesByContactID()
        {
            throw new NotImplementedException();
        }
        void GetContactPreferencesByContactID()
        {
            throw new NotImplementedException();
        }
        void SearchContactPreferencesByReferenceType()
        {
            throw new NotImplementedException();
        }
        void GetAllContactAndAssociatedAddress()
        {
            throw new NotImplementedException();
        }
        void GetRelationships(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
