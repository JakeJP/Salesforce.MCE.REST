using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yokinsoft.Salesforce.MCE
{
    public class EventNotification : APIClientBase
    {
        public EventNotification(AccessToken accessToken) : base(accessToken)
        {
        }
        public EventNotificationCreateCallbackResult CreateCallback( string callbackName, string url, int maxBatchSize = -1)
        {
            var obj = new Dictionary<string, object>
            {
                { "callbackName", callbackName  },
                { "url", url }
            };
            if(maxBatchSize > 0)
            {
                obj["maxBatchSize"] = maxBatchSize;
            }
            return Post<List<EventNotificationCreateCallbackResult>>("/platform/v1/ens-callbacks", new[] { obj }).FirstOrDefault();
        }
        public void VerifyCallback( string callbackId, string verificationKey)
        {
            Post<object>("/platform/v1/ens-verify", new {
                callbackId = callbackId,
                verificationKey = verificationKey
            });
        }

        public EventNotificationCallback GetCallback( string callbackId )
        {
            return Get<EventNotificationCallback>($"/platform/v1/ens-callbacks/{callbackId}");
        }

        public EventNotificationCallback UpdateCallback( string callbackId, string callbackName, int maxBatchSize = 0)
        {
            var o = new Dictionary<string, object>
            {
                {"callbackId", callbackId },
                {"callbackName", callbackName }
            };
            if (maxBatchSize > 0)
                o["maxBatchSize"] = maxBatchSize;

            return Put<EventNotificationCallback>("/platform/v1/ens-callbacks", o);
        }

        public void DeleteCallback( string callbackId)
        {
            Delete<object>($"/platform/v1/ens-callbacks/{callbackId}");
        }

        public List<EventNotificationCallback> GetAllCallbacks()
        {
            return Get<List<EventNotificationCallback>>("/platform/v1/ens-callbacks");
        }

        public EventNotificationSubscriptionCreated CreateSubscription( string callbackId, string subscriptionName, IEnumerable<string> eventCategoryTypes, IEnumerable<string> filters )
        {
            return Post<List<EventNotificationSubscriptionCreated>>("/platform/v1/ens-subscriptions",
                new[]
                {
                    new EventNotificationSubscriptionToCreate
                    {
                        CallbackId = callbackId,
                        SubscriptionName = subscriptionName,
                        EventCategoryTypes = eventCategoryTypes.ToList(),
                        Filters = filters.ToList()
                    }
                }).FirstOrDefault();
        }
        public void DeleteSubscription( string subscriptionId)
        {
            Delete<object>($"/platform/v1/ens-subscriptions/{subscriptionId}");
        }
        public EventNotificationSubscription GetSubscription( string subscriptionId)
        {
            return Get<EventNotificationSubscription>($"/platform/v1/ens-subscriptions/{subscriptionId}");
        }
        public List<EventNotificationSubscription> GetAllSubscriptions( string callbackId )
        {
            return Get<List<EventNotificationSubscription>>($"/platform/v1/ens-subscriptions-by-cb/{callbackId}");
        }
        public EventNotificationSubscription UpdateSubscription( EventNotificationSubscription subscription)
        {
            return Put<EventNotificationSubscription>("/platform/v1/ens-subscriptions", subscription);
        }
        public List<EventNotificationCreateCallbackResult> RegenerateSignatureKey( string callbackId)
        {
            return Put<List<EventNotificationCreateCallbackResult>>("/platform/v1/ens-regenerate", new object[] { new { callbackId } });
        }
    }
}
