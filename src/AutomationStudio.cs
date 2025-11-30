using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Yokoins.Salesforce.MC;

namespace Yokins.Salesforce.MCE
{
    public class AutomationStudio : APIClientBase
    {
        public AutomationStudio( AccessToken accessToken ) : base( accessToken )
        {
        }

        public PageableListContainer<Automation> GetAutomations( string filter = null, string orderBy = null, string view = null, int page = 0, int pagesize = 0)
        {
            return Get<PageableListContainer<Automation>>("/automation/v1/automations", new Dictionary<string, string>
            {
                {"$page", page > 0 ? page.ToString() : null },
                {"$pagesize", pagesize > 0 ? pagesize.ToString() : null },
                {"$filter", filter },
                {"$orderBy", orderBy },
                {"view", view }
            });
        }
        void CreateAutomation()
        {
            throw new NotImplementedException();
        }
        public Automation GetAutomation( string id )
        {
            return Get<Automation>($"/automation/v1/automations/{Uri.UnescapeDataString(id)}");
        }

        public void ExecuteAutomationActivities( string id)
        {
            Post<object>($"/automation/v1/automations/{Uri.UnescapeDataString(id)}/actions/runallonce", null);
        }
        void UpdateAutomationFileTrigger()
        {
            throw new NotImplementedException();
        }
        void UpdateAutomationFileTriggerByKey()
        {
            throw new NotImplementedException();
        }
        public void ExecuteAutomationActivitiesByKey( string key)
        {
            Post<object>($"/automation/v1/automations/key:{Uri.UnescapeDataString(key)}/actions/runallonce", null);
        }

        public AutomationTriggerStatus GetTriggerStatus( string requestId)
        {
            return Get<AutomationTriggerStatus>($"/automation/v1/automations/trigger/status/{Uri.EscapeDataString(requestId)}");
        }

        void GetFileTransferActivities()
        {
            throw new NotImplementedException();
        }
        void CreateFileTransferActivity()
        {
            throw new NotImplementedException();
        }
        void DeleteFileTransferActivity()
        {
            throw new NotImplementedException();
        }
        void GetFileTransferActivity()
        {
            throw new NotImplementedException();
        }
        void UpdateFileTransferActivity()
        {
            throw new NotImplementedException();
        }
        void StartFileTransferActivity()
        {
            throw new NotImplementedException();
        }
        void GetAutomationFolders()
        {
            throw new NotImplementedException();
        }
        void GetFtpLocations()
        {
            throw new NotImplementedException();
        }
        void GetAutomationScriptActivities()
        {

        }
        void ExecuteScriptActivity()
        {

        }
    }
}
