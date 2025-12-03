using System;
using System.Collections.Generic;
using System.Text;

namespace Yokinsoft.Salesforce.MCE
{
    public class JourneysAndEvents : APIClientBase
    {
        public JourneysAndEvents(AccessToken accessToken) : base(accessToken)
        {
        }

        /// <summary>
        /// Returns discovery document
        /// </summary>
        public DiscoveryResult Discover()
        {
            return Get<DiscoveryResult>("/interaction/v1/rest");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactKey">Required. The unique ID of a subscriber or a contact.</param>
        /// <param name="eventDefinitionKey">Required. The unique ID of the journey entry event. You can find the EventDefinitionKey in Event Administration after the event is created and saved. This key is present for both standard and custom events. Don’t include a period in the EventDefinitionKey.</param>
        /// <param name="data">Properties of the event. Required only if defined in a custom event or by the event.</param>
        /// <returns></returns>
        public FireEventResult FireEvent(string contactKey, string eventDefinitionKey, object data)
        {
            return Post<FireEventResult>($"/interaction/v1/events", new
            {
                ContactKey = contactKey,
                EventDefinitionKey = eventDefinitionKey,
                Data = data
            });
        }

        /// <summary>
        /// Create or save an interaction
        /// </summary>
        private TResponse InsertJourney<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions", content);
        }

        /// <summary>
        /// Update an interaction version
        /// </summary>
        private TResponse UpdateJourneyVersion<TResponse>(object content)
        {
            return Put<TResponse>("/interaction/v1/interactions", content);
        }

        /// <summary>
        /// Update an interaction version
        /// </summary>
        private TResponse UpdateInteractionByKey<TResponse>(string key, object content)
        {
            return Put<TResponse>($"/interaction/v1/interactions/key:{Uri.EscapeDataString(key)}", content);
        }

        /// <summary>
        /// Update an interaction version
        /// </summary>
        private TResponse UpdateInteractionById<TResponse>(string id, object content)
        {
            return Put<TResponse>($"/interaction/v1/interactions/{Uri.EscapeDataString(id)}", content);
        }

        /// <summary>
        /// Publish an interaction version asynchronously
        /// </summary>
        private TResponse PublishJourneyById<TResponse>(string id, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/interactions/publishAsync/{Uri.EscapeDataString(id)}", content);
        }

        /// <summary>
        /// Publish an interaction version asynchronously
        /// </summary>
        private TResponse PublishJourneyByKey<TResponse>(string key, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/interactions/publishAsync/key:{Uri.EscapeDataString(key)}", content);
        }

        /// <summary>
        /// Check the status of a publication
        /// </summary>
        private TResponse GetPublishStatus<TResponse>(string statusId)
        {
            return Get<TResponse>($"/interaction/v1/interactions/publishStatus/{Uri.EscapeDataString(statusId)}");
        }

        /// <summary>
        /// Validate an interaction version asynchronously
        /// </summary>
        private TResponse ValidateInteractionById<TResponse>(string id, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/interactions/validateAsync/{Uri.EscapeDataString(id)}", content);
        }

        /// <summary>
        /// Validate an interaction version asynchronously
        /// </summary>
        private TResponse ValidateInteractionByKey<TResponse>(string key, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/interactions/validateAsync/key:{Uri.EscapeDataString(key)}", content);
        }

        /// <summary>
        /// Check the status of a validation
        /// </summary>
        private TResponse ValidateStatus<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/interactions/validateStatus/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Stops a running interaction.
        /// </summary>
        private TResponse Stop<TResponse>(string definitionId, int versionNumber )
        {
            return Post<TResponse>($"/interaction/v1/interactions/stop/{Uri.EscapeDataString(definitionId)}?versionNumber={versionNumber.ToString()}", null);
        }

        /// <summary>
        /// Stops a running interaction.
        /// </summary>
        private TResponse StopByKey<TResponse>(string key, int versionNumber)
        {
            return Post<TResponse>($"/interaction/v1/interactions/stop/key:{Uri.EscapeDataString(key)}?versionNumber={versionNumber.ToString()}", null);
        }

        /// <summary>
        /// Stops a running interaction asynchronously.
        /// </summary>
        private TResponse StopAsyncById<TResponse>(string id, int versionNumber)
        {
            return Post<TResponse>($"/interaction/v1/interactions/stopAsync/{Uri.EscapeDataString(id)}?versionNumber={versionNumber.ToString()}", null);
        }

        /// <summary>
        /// Stops a running interaction asynchronously.
        /// </summary>
        private TResponse StopAsyncByKey<TResponse>(string key, int versionNumber)
        {
            return Post<TResponse>($"/interaction/v1/interactions/stopAsync/key:{Uri.EscapeDataString(key)}?versionNumber={versionNumber.ToString()}", null);
        }

        /// <summary>
        /// Retrieve goal statistics for an interaction
        /// </summary>
        private TResponse GetGoalStatistics<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/goalstatistics/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Clear goal statistics for an interaction
        /// </summary>
        private TResponse PostGoalStatistics<TResponse>(string id, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/goalstatistics/clear/{Uri.EscapeDataString(id)}", content);
        }

        /// <summary>
        /// Retrieve an interaction.
        /// </summary>
        /// <param name="id">ID of version 1 of the journey in the form of a GUID (UUID). Required if not using a key.</param>
        /// <param name="versionNumber"></param>
        /// <param name="extras"></param>
        private TResponse GetJourney<TResponse>(string id, int versionNumber = 0, string extras = null )
        {
            return Get<TResponse>($"/interaction/v1/interactions/{Uri.EscapeDataString(id)}",
                new Dictionary<string, string>
                {
                    { "versionNumber", versionNumber >= 0 ? versionNumber.ToString() : null },
                    { "extra", extras }
                });
        }
        private TResponse GetJourneyByKey<TResponse>(string key, int versionNumber = 0, string extras = null)
        {
            return Get<TResponse>($"/interaction/v1/interactions/key:{Uri.EscapeDataString(key)}",
                new Dictionary<string, string>
                {
                    { "versionNumber", versionNumber >= 0 ? versionNumber.ToString() : null },
                    { "extra", extras }
                });
        }

        /// <summary>
        /// Retrieves summarized data for a journey (across all versions). Activity counts are for active activities only.
        /// </summary>
        private TResponse GetInteractionSummary<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/interactions/{Uri.EscapeDataString(id)}/summary");
        }

        /// <summary>
        /// Delete a Journey.
        /// </summary>
        public void DeleteJourney(string id, int versionNumber = -1)
        {
            Delete<object>($"/interaction/v1/interactions/{Uri.EscapeDataString(id)}",
                new Dictionary<string,string>{
                    { "versionNumber", versionNumber >= 0 ? versionNumber.ToString() : null }
                });
        }
        public void DeleteJourneyByKey(string key, int versionNumber = -1)
        {
            Delete<object>($"/interaction/v1/interactions/key:{Uri.EscapeDataString(key)}",
                new Dictionary<string, string>{
                    { "versionNumber", versionNumber >= 0 ? versionNumber.ToString() : null }
                });
        }

        /// <summary>
        /// Delete an interaction.
        /// </summary>
        private TResponse DeleteInteractionByKey<TResponse>(string key)
        {
            return Delete<TResponse>($"/interaction/v1/interactions/key:{Uri.EscapeDataString(key)}");
        }

        /// <summary>
        /// Retrieve a collection of interactions.
        /// </summary>
        private TResponse SearchJourneys<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/interactions", query);
        }

        /// <summary>
        /// Retrieve wait activity counts for an interaction
        /// </summary>
        private TResponse GetWaitStatistics<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/waitstatistics/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Search for contact history
        /// </summary>
        private TResponse SearchJourneyHistory<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/journeyhistory/search", content);
        }

        /// <summary>
        /// Get the list of unique journeys, activity types and status for given criteria
        /// </summary>
        private TResponse GetJourneyHistoryFilter<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/journeyhistory/filter", content);
        }

        /// <summary>
        /// Get summary for all journeys
        /// </summary>
        private TResponse GetJourneyHistorySummary<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/journeyhistory/summary", content);
        }

        /// <summary>
        /// Retrieve a collection of definition logs.
        /// </summary>
        private TResponse GetDefinitionLog<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/definitionlogs/search", content);
        }

        /// <summary>
        /// Download Journey history
        /// </summary>
        private TResponse DownloadJourneyHistoryPost<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/journeyhistory/download", content);
        }

        /// <summary>
        /// Estimate Journey history size
        /// </summary>
        private TResponse JourneyHistoryEstimatePost<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/journeyhistory/estimate", content);
        }

        /// <summary>
        /// Get Journey history freshness
        /// </summary>
        private TResponse JourneyHistoryFreshnessGet<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/interactions/journeyhistory/freshness", query);
        }

        /// <summary>
        /// Search history for contact
        /// </summary>
        private TResponse GetContactKey<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/journeyhistory/contactkey", content);
        }

        /// <summary>
        /// Get execution summary by activity and definition
        /// </summary>
        private TResponse GetSummaryByActivity<TResponse>(string definitionId, string activityId, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/interactions/{Uri.EscapeDataString(definitionId)}/activities/{Uri.EscapeDataString(activityId)}/summary", content);
        }

        /// <summary>
        /// Get timeSeries for all journeys
        /// </summary>
        private TResponse GetJourneyHistoryTimeSeries<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/journeyhistory/timeSeries", content);
        }

        /// <summary>
        /// Get execution time series by activity and definition
        /// </summary>
        private TResponse GetActivityTimeSeries<TResponse>(string definitionId, string activityId, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/interactions/{Uri.EscapeDataString(definitionId)}/activities/{Uri.EscapeDataString(activityId)}/timeseries", content);
        }

        /// <summary>
        /// Get wait summary by activity and definition
        /// </summary>
        private TResponse GetWaitSummaryByActivity<TResponse>(string definitionId, string activityId, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/interactions/{Uri.EscapeDataString(definitionId)}/activities/{Uri.EscapeDataString(activityId)}/waitexpire/summary", content);
        }

        /// <summary>
        /// Get wait time series by activity and definition
        /// </summary>
        private TResponse GetActivityWaitTimeSeries<TResponse>(string definitionId, string activityId, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/interactions/{Uri.EscapeDataString(definitionId)}/activities/{Uri.EscapeDataString(activityId)}/waitexpire/timeseries", content);
        }

        /// <summary>
        /// Retrieve trigger statistics
        /// </summary>
        private TResponse GetTriggerStatistics<TResponse>(string eventDefinitionID)
        {
            return Get<TResponse>($"/interaction/v1/triggerstats/{Uri.EscapeDataString(eventDefinitionID)}");
        }

        /// <summary>
        /// Retrieve trigger statistics by Journey
        /// </summary>
        private TResponse GetTriggerStatisticsByJourney<TResponse>(string eventDefinitionID, string definitionID)
        {
            return Get<TResponse>($"/interaction/v1/triggerstats/{Uri.EscapeDataString(eventDefinitionID)}/{Uri.EscapeDataString(definitionID)}");
        }

        /// <summary>
        /// Create a trigger test
        /// </summary>
        private TResponse CreateTriggerTest<TResponse>(string eventDefinitionId, object content = null)
        {
            return Post<TResponse>($"/interaction/v1/interactions/triggerTest/{Uri.EscapeDataString(eventDefinitionId)}", content);
        }

        /// <summary>
        /// Update a trigger test
        /// </summary>
        private TResponse UpdateTriggerTest<TResponse>(string eventDefinitionId, object content = null)
        {
            return Put<TResponse>($"/interaction/v1/interactions/triggerTest/{Uri.EscapeDataString(eventDefinitionId)}", content);
        }

        /// <summary>
        /// Delete a trigger test
        /// </summary>
        private TResponse DeleteTriggerTest<TResponse>(string eventDefinitionId)
        {
            return Delete<TResponse>($"/interaction/v1/interactions/triggerTest/{Uri.EscapeDataString(eventDefinitionId)}");
        }

        /// <summary>
        /// Create an event definition
        /// </summary>
        private TResponse CreateEventDefinition<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/eventDefinitions", content);
        }

        /// <summary>
        /// Update an event definition by ID
        /// </summary>
        private TResponse UpdateEventDefinitionById<TResponse>(string id, object content)
        {
            return Put<TResponse>($"/interaction/v1/eventDefinitions/{Uri.EscapeDataString(id)}", content);
        }

        /// <summary>
        /// Update an event definition by key
        /// </summary>
        private TResponse UpdateEventDefinitionByKey<TResponse>(string key, object content)
        {
            return Put<TResponse>($"/interaction/v1/eventDefinitions/key:{Uri.EscapeDataString(key)}", content);
        }

        /// <summary>
        /// Get an event definition by key
        /// </summary>
        private TResponse GetEventDefinitionByKey<TResponse>(string key)
        {
            return Get<TResponse>($"/interaction/v1/eventDefinitions/key:{Uri.EscapeDataString(key)}");
        }

        /// <summary>
        /// Get an event definition by id
        /// </summary>
        private TResponse GetEventDefinitionById<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/eventDefinitions/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Retrieve a collection of  event definitions.
        /// </summary>
        private TResponse GetEventDefinitions<TResponse>( string name, int page = 0, int pageSize = 0 )
        {
            return Get<TResponse>("/interaction/v1/eventDefinitions", new Dictionary<string, string>
            {
                { "name", name  },
                { "page", page > 0 ? page.ToString() : null },
                { "pageSize", pageSize > 0 ?  pageSize.ToString() : null }
            });
        }

        /// <summary>
        /// Delete an event definition by id
        /// </summary>
        private TResponse DeleteEventDefinitionById<TResponse>(string id)
        {
            return Delete<TResponse>($"/interaction/v1/eventDefinitions/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Delete an event definition by key
        /// </summary>
        private TResponse DeleteEventDefinitionByKey<TResponse>(string key)
        {
            return Delete<TResponse>($"/interaction/v1/eventDefinitions/key:{Uri.EscapeDataString(key)}");
        }

        /// <summary>
        /// Post an event to interactions
        /// </summary>
        private TResponse PostInteractionsEvents<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/events", content);
        }

        /// <summary>
        /// Post a batch of events to interactions
        /// </summary>
        private TResponse InsertContactsIntoJourney<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/async/events", content);
        }

        /// <summary>
        /// Get batch events status from interactions
        /// </summary>
        private TResponse GetInsertContactsIntoJourneyStatus<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/async/events/status", query);
        }

        /// <summary>
        /// Retrieve a collection of history.
        /// </summary>
        private TResponse GetHistory<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/history", query);
        }

        /// <summary>
        /// Retrieve a collection of Audit logs.
        /// </summary>
        private TResponse GetAuditLogById<TResponse>(string id, string action, Dictionary<string, string> query = null)
        {
            return Get<TResponse>($"/interaction/v1/interactions/{Uri.EscapeDataString(id)}/audit/{Uri.EscapeDataString(action)}", query);
        }

        /// <summary>
        /// Retrieve a collection of Audit logs.
        /// </summary>
        private TResponse GetAuditLogByKey<TResponse>(string key, string action, Dictionary<string, string> query = null)
        {
            return Get<TResponse>($"/interaction/v1/interactions/key:{Uri.EscapeDataString(key)}/audit/{Uri.EscapeDataString(action)}", query);
        }

        /// <summary>
        /// Retrieve a collection of Audit logs.
        /// </summary>
        private TResponse GetAuditLog<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/audit", content);
        }

        /// <summary>
        /// Retrieve a collection of developer logs by member id.
        /// </summary>
        private TResponse GetDeveloperLogs<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/developerLogs", query);
        }

        /// <summary>
        /// To verify that activity log coming via kafka
        /// </summary>
        private TResponse IsActivityLogComingViaKafka<TResponse>(string definitionID, string contactKey)
        {
            return Get<TResponse>($"/interaction/v1/isActivityLogComingViaKafka/{Uri.EscapeDataString(definitionID)}/{Uri.EscapeDataString(contactKey)}");
        }

        /// <summary>
        /// To verify that definition log coming via kafka
        /// </summary>
        private TResponse IsDefinitionLogComingViaKafka<TResponse>(string definitionID, string contactKey)
        {
            return Get<TResponse>($"/interaction/v1/isDefinitionLogComingViaKafka/{Uri.EscapeDataString(definitionID)}/{Uri.EscapeDataString(contactKey)}");
        }

        /// <summary>
        /// Retrieve journey Summary Counts.
        /// </summary>
        private TResponse GetJourneySummaryCounts<TResponse>(string id, string version, Dictionary<string, string> query = null)
        {
            return Get<TResponse>($"/interaction/v1/journeys/{Uri.EscapeDataString(id)}/versions/{Uri.EscapeDataString(version)}/summary/counts", query);
        }

        /// <summary>
        /// Retrieve journey Summary Contacts.
        /// </summary>
        private TResponse GetJourneySummaryContactsByEventType<TResponse>(string id, string version, string type, Dictionary<string, string> query = null)
        {
            return Get<TResponse>($"/interaction/v1/journeys/{Uri.EscapeDataString(id)}/versions/{Uri.EscapeDataString(version)}/summary/contacts/{Uri.EscapeDataString(type)}", query);
        }

        /// <summary>
        /// Retrieve journey Summary Contacts.
        /// </summary>
        private TResponse GetJourneySummaryContacts<TResponse>(string id, string version, string type, string status, Dictionary<string, string> query = null)
        {
            return Get<TResponse>($"/interaction/v1/journeys/{Uri.EscapeDataString(id)}/versions/{Uri.EscapeDataString(version)}/summary/contacts/{Uri.EscapeDataString(type)}/{Uri.EscapeDataString(status)}", query);
        }

        /// <summary>
        /// Retrieve a Definition Template
        /// </summary>
        private TResponse GetDefinitionTemplate<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/definitionTemplates/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Retrieve Definition Template collection
        /// </summary>
        private TResponse GetDefinitionTemplateCollection<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/definitionTemplates", query);
        }

        /// <summary>
        /// Create an interaction definition template
        /// </summary>
        private TResponse PostDefinitionTemplate<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/definitionTemplates", content);
        }

        /// <summary>
        /// Update an interaction definition template
        /// </summary>
        private TResponse UpdateDefinitionTemplate<TResponse>(string id, object content)
        {
            return Put<TResponse>($"/interaction/v1/definitionTemplates/{Uri.EscapeDataString(id)}", content);
        }

        /// <summary>
        /// Pause an interaction by DefinitionId
        /// </summary>
        private TResponse PauseJourneyById<TResponse>(string id)
        {
            return Post<TResponse>($"/interaction/v1/interactions/pause/{Uri.EscapeDataString(id)}", null);
        }

        /// <summary>
        /// Pause an interaction by Key
        /// </summary>
        private TResponse PauseJourneyByKey<TResponse>(string key)
        {
            return Post<TResponse>($"/interaction/v1/interactions/pause/key:{Uri.EscapeDataString(key)}", null);
        }

        /// <summary>
        /// Get status of an interaction by DefinitionId
        /// </summary>
        private TResponse GetInteractionStatusById<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/interactions/status/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Get status of an interaction by Key
        /// </summary>
        private TResponse GetInteractionStatusByKey<TResponse>(string key)
        {
            return Get<TResponse>($"/interaction/v1/interactions/status/key:{Uri.EscapeDataString(key)}");
        }

        /// <summary>
        /// Resume an interaction by DefinitionId
        /// </summary>
        private TResponse ResumeJourneyById<TResponse>(string id, int versionNumber = -1)
        {
            return Post<TResponse>($"/interaction/v1/interactions/resume/{Uri.EscapeDataString(id)}?{(versionNumber >= 0 ? $"versionNumber=" + versionNumber.ToString() : "allVersions=true")}", null );
        }

        /// <summary>
        /// Resume an interaction by Key
        /// </summary>
        private TResponse ResumeJourneyByKey<TResponse>(string key, int versionNumber = -1)
        {
            return Post<TResponse>($"/interaction/v1/interactions/resume/key:{Uri.EscapeDataString(key)}?{(versionNumber >= 0 ? $"versionNumber=" + versionNumber.ToString() : "allVersions=true")}", null);
        }

        /// <summary>
        /// Accepts list of contacts for contact exit processing
        /// </summary>
        private TResponse RemoveContactFromJourney<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/contactexit", content);
        }

        /// <summary>
        /// Accepts list of contacts for returning contact exit status
        /// </summary>
        private TResponse GetRemoveContactFromJourneyStatus<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/contactexit/status", content);
        }

        /// <summary>
        /// Retrieve a random sample of DE contacts for the provided eventDefinitionId
        /// </summary>
        private TResponse GetSimulationContacts<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/simulation/contacts", query);
        }

        /// <summary>
        /// Retrieve an existing Simulation object
        /// </summary>
        private TResponse GetSimulation<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/simulation/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Initiates a simulation based on the journey under test and the test configuration provided in the request body
        /// </summary>
        private TResponse StartSimulation<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/simulation/", content);
        }

        /// <summary>
        /// Deletes the simulation entities and stops and deletes all related test journeys
        /// </summary>
        private TResponse DeleteSimulation<TResponse>(string id)
        {
            return Delete<TResponse>($"/interaction/v1/simulation/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Creates a new Transaction Send for the provided Journey DefinitionId
        /// </summary>
        private TResponse CreateTransactionalMessage<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/transactional/create", content);
        }

        /// <summary>
        /// Updates status to InActive for the Transactional Send associated with the provided Journey DefinitionId
        /// </summary>
        private TResponse PauseTransactionalMessage<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/transactional/pause", content);
        }

        /// <summary>
        /// Clear queue for the Transactional Send associated with the provided Journey DefinitionId
        /// </summary>
        private TResponse ClearQueueForTransactionalMessage<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/transactional/clearQueue", content);
        }

        /// <summary>
        /// Updates status to Active for the Transactional Send associated with the provided Journey DefinitionId
        /// </summary>
        private TResponse ResumeTransactionalMessage<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/transactional/resume", content);
        }

        /// <summary>
        /// Assigns a categoryId to the DefinitionInfo for a list of DefinitionIds
        /// </summary>
        private TResponse AssociateCategoryWithDefinitions<TResponse>(string id, object content)
        {
            return Post<TResponse>($"/interaction/v1/interactions/categories/{Uri.EscapeDataString(id)}", content);
        }

        /// <summary>
        /// Returns the number of child categories and their journey counts by status
        /// </summary>
        private TResponse GetCategoryDefinitionCounts<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/interactions/categories/{Uri.EscapeDataString(id)}/counts");
        }

        /// <summary>
        /// Deletes a journey category and its associated journeys
        /// </summary>
        private TResponse DeleteCategoryAndDefinitions<TResponse>(string id)
        {
            return Delete<TResponse>($"/interaction/v1/interactions/categories/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Accepts list of contacts and returns mapping between contacts and active/paused journeys.
        /// </summary>
        private TResponse GetContactMembershipByKey<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/contactMembership", content);
        }

        /// <summary>
        /// Get the dashboard configuration for the given user
        /// </summary>
        private TResponse GetDashboardUserConfig<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/dashboard/config", query);
        }

        /// <summary>
        /// Save a new dashboard configuration for the given user
        /// </summary>
        private TResponse SaveDashboardUserConfig<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/dashboard/config", content);
        }

        /// <summary>
        /// Ad Hoc execute an activity via test harness
        /// </summary>
        private TResponse ActivityAdHocExecute<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/activity/execute", content);
        }

        /// <summary>
        /// Ad Hoc execute an activity to test activity setup and instance creation.
        /// </summary>
        private TResponse ActivitySetupTest<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/activity/setup/test", content);
        }

        /// <summary>
        /// Get ABnTest activity details by Activity Id
        /// </summary>
        private TResponse ABnTestGetDetails<TResponse>(string id)
        {
            return Get<TResponse>($"/interaction/v1/abntest/{Uri.EscapeDataString(id)}");
        }

        /// <summary>
        /// Update ABnTest activity details by Activity Id
        /// </summary>
        private TResponse ABnTestUpdate<TResponse>(string id, object content)
        {
            return Patch<TResponse>($"/interaction/v1/abntest/{Uri.EscapeDataString(id)}", content);
        }

        /// <summary>
        /// Returns engine settings
        /// </summary>
        private TResponse GetEngineSettings<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/engine-settings", query);
        }

        /// <summary>
        /// Returns published list of SalesCloud Events
        /// </summary>
        private TResponse SalesCloudEvents<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/SalesCloudEvents", query);
        }

        /// <summary>
        /// Returns published list of SalesCloud Events using CDC
        /// </summary>
        private TResponse SalesCloudEventsCDC<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/SalesCloudEvents/CDC", query);
        }

        /// <summary>
        /// Returns SalesCloud Event Usage
        /// </summary>
        private TResponse SalesCloudEventUsage<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/interactions/salescloudeventusage", query);
        }

        /// <summary>
        /// Return Journeys Executions Overview
        /// </summary>
        private TResponse JourneysExecutionsOverview<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/system-overview", query);
        }

        /// <summary>
        /// Return Journeys Executions Overview in time series
        /// </summary>
        private TResponse JourneysExecutionsOverviewTimeSeries<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/system-overview/timeseries", query);
        }

        /// <summary>
        /// Return Activities Executions Overview
        /// </summary>
        private TResponse ActivitiesExecutionsOverview<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/system-overview/activities", query);
        }

        /// <summary>
        /// Return journey and activity level recommendations
        /// </summary>
        private TResponse Recommendations<TResponse>(string id, Dictionary<string, string> query = null)
        {
            return Get<TResponse>($"/interaction/v1/recommendations/{Uri.EscapeDataString(id)}", query);
        }

        /// <summary>
        /// Return list of recommendation types
        /// </summary>
        private TResponse RecommendationType<TResponse>(Dictionary<string, string> query = null)
        {
            return Get<TResponse>("/interaction/v1/recommendationTypes", query);
        }

        /// <summary>
        /// Updates journey priority
        /// </summary>
        private TResponse UpdateJourneyPriority<TResponse>(object content)
        {
            return Put<TResponse>("/interaction/v1/journeys/priority", content);
        }

        /// <summary>
        /// Updates TriggeredSend TrackingSubscription Type.
        /// </summary>
        private TResponse UpdateTriggeredSendSubscriptionType<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/triggeredsends/trackingsubscriptiontype", content);
        }

        /// <summary>
        /// Saves Journey Campaign relationship
        /// </summary>
        private TResponse SaveInteractionCampaign<TResponse>(object content)
        {
            return Post<TResponse>("/interaction/v1/interactions/interactionCampaign", content);
        }
    }
}
