# .NET client library for Salesforce Marketing Cloud Engagement REST API

**WARNING: This libary is not Not Fully Implemented** .

- Easy access to **Salesforce Marketing Cloud Engagement REST API** from .NET applications.
- Compatible with **.NET Standard 2.0** and **.NET Framework 4.6.1+**
- Additional easy handling of Data Extensions with .NET **LINQ** and **DataTable**s. 
- **Not Fully Implemented** This library is a work in progress and does not yet cover all endpoints of the Salesforce Marketing Cloud Engagement REST API.

# Usage

## 1. Get AccessToken

Make sure Auth endpoint URI or {subdomain}, Member ID (account ID), ClientID and ClientSecret are given from Salesforce Marketing Cloud Engagement.

Auth endpoint URI looks like `https://{subdomain}.auth.marketingcloudapis.com/v2/token`.
`{subdomain}` can be used instead of full endpoint URI.

```csharp
using Yokins.Salesforce.MCE;
var auth = new Auth("YOUR_AUTH_ENDPOINT_OR_DOMAIN");
var accessToken = auth.GetAccessToken("YOUR_MEMBER_ID", "YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET" );
```

Obtained access token has limited life time. You may need to refresh it when it expires and store at a certain place like memory or file storage for persistency.

```csharp
if( !accessToken.IsValid )
{
	accessToken = auth.GetAccessToken();
}
```

## 2. Initialize API client

Along with the API document sections, multiple API clients are defined per purpose.

`DataExtensions` for Data Extentions section, DataExtentionRows for Data Extension Rows section, and so on.

implemented API clients are:


| API section | Class Name | Implementation |
|----|---------------------|:--:|
| Auth | Auth | 〇 |
| Event Notification | EventNotification | 〇 |
| Data Extensions | DataExtensions | 〇 |
| Data Extension Data | DataExtensionData | 〇 |
| Data Extension Rows | DataExtensionRows | 〇 |
| Data Extension Imports | DataExtensionImports | 〇 |

```csharp

var de = new DataExtensions(accessToken);
var listContainer = de.GetDataExtensions("SERACH_KEYWORD");
foreach( DataExtension de in listContainer.Items ){
	;
}
```

`PageableListContainer<DataExtension>` listContainer contains the all response from API including page number
and page size information, which can be used for pagenation that reuiqres multiple API calls later.

## 3. Additional support for .NET LINQ and DataTable

`DataExtensionItemListContainer` supports extra .NET programability.
Using LINQ or IDataReader, you can easily consume Data Extension rows over the page size limit (2500)
, where multiple API calls are made internally to fetch all rows.

### DataExtension => Local container (IEnumerable or DataTable)

```c#
var dd = new DataExtensionData(accessToken);
var container = dd.GetData("[Data extension ID]");

// consume as IEnumerable<DataExtensionItem>

foreach( var row in container.AsEnumerable()){
	row.Keys; // as Dictionary<string,string>
	row.Values; // as Dictionary<string,string>
}

// consume as IDataReader

var table = new DataTable();
table.Load( container.CreateDataReader() );
```

IDataReader provides data type for each column based on the Data Extension field definition.

### Local container (DataTable) => DataExtension

```c#
var table = new DataTable();
// fill table with data ans set PrimaryKey column(s)
var dd = new DataExtensionData(accessToken);
dd.UpsertRowSetByKey("[Data extension ID]", table.CreateDataReader());
```

# Author

Jake Y. Yoshimura [Yokinsoft](https://www.yo-ki.com)

# License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

# Copyright

&copy; 2025 Yokinsoft, Jake Y.Yoshimura under 

# Revision History

- 2025-12-02 some fixes and additions
- 2025-11-30 Initial version