using System;
using System.Collections.Generic;
using System.Text;

namespace Yokinsoft.Salesforce.MCE
{
    public class Campaigns : APIClientBase
    {
        public Campaigns( AccessToken accessToken) : base( accessToken )
        {
        }

        // GET /v1/campaigns/
        public PageableListContainer<Campaign> GetCampaigns( int page = 0, int pageSize = 0, string orderBy = null )
            => Get<PageableListContainer<Campaign>>("/hub/v1/campaigns/", new Dictionary<string, string>
            {
                { "$page", page > 0 ? page.ToString() : null },
                { "$pageSize", pageSize > 0 ? pageSize.ToString() : null },
                { "$orderBy", !string.IsNullOrEmpty(orderBy) ? orderBy : null }
            });
    
        // POST /v1/campaigns/
        public Campaign CreateCampaign(CampaignToCreate campaign)
            => Post<Campaign>("/hub/v1/campaigns/", campaign);

        // GET /v1/campaigns/{id}
        public Campaign GetCampaign(string id)
            => Get<Campaign>($"/hub/v1/campaigns/{Uri.EscapeDataString(id)}");

        // PATCH /v1/campaigns/{id}
        public Campaign UpdateCampaign<TResponse>(string id, CampaignToCreate update)
            => Patch<Campaign>($"/hub/v1/campaigns/{Uri.EscapeDataString(id)}", update);

        // DELETE /v1/campaigns/{id}
        public void DeleteCampaign<TResponse>(string id)
            => Delete<object>($"/hub/v1/campaigns/{Uri.EscapeDataString(id)}");

        // GET /v1/campaigns/{id}/assets
        public PageableListContainer<CampaignAsset> GetCampaignAssets(string id)
            => Get<PageableListContainer<CampaignAsset>>($"/hub/v1/campaigns/{Uri.EscapeDataString(id)}/assets");

        // POST /v1/campaigns/{id}/assets
        public List<CampaignAsset> AddAssetToCampaign<TResponse>(string id, IEnumerable<string> ids, string type )
            => Post<List<CampaignAsset>>($"/hub/v1/campaigns/{Uri.EscapeDataString(id)}/assets", new { ids, type });

        // GET /v1/campaigns/{id}/assets/{assetId}
        public CampaignAsset GetCampaignAsset<TResponse>(string id, string assetId)
            => Get<CampaignAsset>($"/hub/v1/campaigns/{Uri.EscapeDataString(id)}/assets/{Uri.EscapeDataString(assetId)}");

        // DELETE /v1/campaigns/{id}/assets/{assetId}
        public void RemoveAssetFromCampaign<TResponse>(string id, string assetId)
            => Delete<object>($"/hub/v1/campaigns/{Uri.EscapeDataString(id)}/assets/{Uri.EscapeDataString(assetId)}");
    }
}
