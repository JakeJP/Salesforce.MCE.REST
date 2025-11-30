using System;
using System.Collections.Generic;
using System.Text;

namespace Yokins.Salesforce.MCE
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
        void CreateContacts()
        {
            throw new NotImplementedException();
        }

        public ContactUpdateResult UpdateContacts( string contactKey, string contactId, IEnumerable<ContactAttributeSet> attributeSets )
        {
            return Patch<ContactUpdateResult>("/contacts/v1/contacts",
                new
                {
                    contactKey = contactKey,
                    contactId = contactId,
                    attributeSets = attributeSets
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

        void GetContactKeyForEmailAddress(IEnumerable<string> channelAddressList, int maximumCount = -1)
        {
            throw new NotImplementedException();
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

        void DeleteContactsByID(IEnumerable<string> contactIds, string deleteOperationType)
        {
            throw new NotImplementedException();
        }
        void DeleteContactsByKey(IEnumerable<string> contactKeys, string deleteOperationType)
        {
            throw new NotImplementedException();
        }
        void DeleteContactsByListReference()
        {
            throw new NotImplementedException();
        }

        void GetDeleteOptions()
        {
            throw new NotImplementedException();
        }
        void GetStatusOfContactDeleteOperation(long operationId) { throw new NotImplementedException(); }
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
        void RestrictContactsByID(IEnumerable<string> contactIds)
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
