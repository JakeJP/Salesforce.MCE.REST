using System;
using System.Collections.Generic;
using System.Text;

namespace Yokins.Salesforce.MCE
{
    public class Journeys : APIClientBase
    {
        public Journeys(AccessToken accessToken) : base(accessToken)
        {
        }
        void FireEvent( string contactKey, string eventDefinitionKey, object data)
        {
            Post<object>($"/interaction/v1/events", new
            {
                ContactKey = contactKey,
                EventDefinitionKey = eventDefinitionKey,
                Data = data
            });
        }
    }
}
