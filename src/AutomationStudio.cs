using System;
using System.Collections.Generic;

namespace Yokinsoft.Salesforce.MCE
{
    public class AutomationStudio : APIClientBase
    {
        public AutomationStudio(AccessToken accessToken) : base(accessToken)
        {
        }

        public PageableListContainer<Automation> GetAutomations(string filter = null, string orderBy = null, string view = null, int page = 0, int pagesize = 0)
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
        public Automation GetAutomation(string id)
        {
            return Get<Automation>($"/automation/v1/automations/{Uri.UnescapeDataString(id)}");
        }

        public void ExecuteAutomationActivities(string id)
        {
            Post<object>($"/automation/v1/automations/{Uri.UnescapeDataString(id)}/actions/runallonce", null);
        }
        public void UpdateAutomationFileTrigger(string id, bool isActive)
        {
            Patch<object>($"/automation/v1/automations/filetrigger/{id}", new { isActive });
        }
        public void UpdateAutomationFileTriggerByKey(string key, bool isActive)
        {
            Patch<object>($"/automation/v1/automations/filetrigger/key:{key}", new { isActive });
        }
        public void ExecuteAutomationActivitiesByKey(string key)
        {
            Post<object>($"/automation/v1/automations/key:{Uri.UnescapeDataString(key)}/actions/runallonce", null);
        }
        public TriggeredAutomationResult ExecuteTriggeredAutomation()
        {
            return Post<TriggeredAutomationResult>("/automation/v1/automations/trigger", null);
        }
        public AutomationTriggerStatus GetTriggerStatus(string requestId)
        {
            return Get<AutomationTriggerStatus>($"/automation/v1/automations/trigger/status/{Uri.EscapeDataString(requestId)}");
        }

        public void UpdateAutomationTrigger(string id, bool isActive)
        {
            Patch<object>($"/automation/v1/automations/trigger/{id}", new { isActive });
        }

        public AutomationTriggerStatus GetTriggerStatus(string requestId, string subdomain)
        {
            return Get<AutomationTriggerStatus>($"/automation/v1/automations/trigger/status/{requestId}", new Dictionary<string, string> { { "subdomain", subdomain } });
        }

        public PageableListContainer<FileTransferActivitySimplified> GetFileTransferActivities(string subdomain, int page = 0, int pagesize = 0, string filter = null, string orderBy = null)
        {
            return Get<PageableListContainer<FileTransferActivitySimplified>>("/automation/v1/fileTransfers", new Dictionary<string, string>
            {
                {"subdomain", subdomain },
                {"$filter", filter },
                {"$orderBy", orderBy },
                {"$page", page > 0 ? page.ToString() : null},
                {"$pagesize", pagesize > 0 ? pagesize.ToString() : null }
            });
        }
        public FileTransferActivity CreateFileTransferActivity(FileTransferActivityToCreate fileTransferActivity)
        {
            return Post<FileTransferActivity>("/automation/v1/fileTransfers", fileTransferActivity);
        }
        public void DeleteFileTransferActivity(string fileTransferActivityId)
        {
            Delete<object>($"/automation/v1/fileTransfers/{fileTransferActivityId}", null);
        }
        public FileTransferActivity GetFileTransferActivity(string fileTransferActivityId)
        {
            return Get<FileTransferActivity>($"/automation/v1/fileTransfers/{fileTransferActivityId}");
        }
        public FileTransferActivity UpdateFileTransferActivity(string fileTransferActivityId, FileTransferActivityToCreate activity)
        {
            return Patch<FileTransferActivity>($"/automation/v1/fileTransfers/{fileTransferActivityId}", activity);
        }
        public void StartFileTransferActivity(string fileTransferActivityId, string subdomain)
        {
            Post<object>($"/automation/v1/fileTransfers/{fileTransferActivityId}/start",
                new { subdomain });
        }
        public IList<AutomationFolder> GetAutomationFolders(string subdomain, string filter)
        {
            return Get<PageableListContainer<AutomationFolder>>("/automation/v1/folders", new Dictionary<string, string>
            {
                {"subdomain", subdomain },
                {"$filter", filter }
            }).Items;

        }
        public IList<AutomationFtpLocation> GetFtpLocations(string subdomain)
        {
            return Get<PageableListContainer<AutomationFtpLocation>>("/automation/v1/ftpLocations", new Dictionary<string, string>
            {
                {"subdomain", subdomain }
            }).Items;
        }
        public PageableListContainer<AutomationSsjsActivity> GetAutomationScriptActivities(string subdomain, string filter = null, string orderBy = null, int page = 0, int pagesize = 0)
        {
            return Get<PageableListContainer<AutomationSsjsActivity>>("/automation/v1/scripts", new Dictionary<string, string>
            {
                {"subdomain", subdomain },
                {"$filter", filter },
                {"$orderBy", orderBy },
                {"$page", page > 0 ? page.ToString() : null},
                {"$pagesize", pagesize > 0 ? pagesize.ToString() : null }
            });
        }
        public void ExecuteScriptActivity(string ssjsId, string subdomain)
        {
            Post<object>($"/automation/v1/scripts/{ssjsId}/start", new { subdomain });
        }
    }
}
