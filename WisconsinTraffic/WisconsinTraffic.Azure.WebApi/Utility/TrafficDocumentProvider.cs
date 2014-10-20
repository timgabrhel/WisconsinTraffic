using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Data.Edm.Csdl;
using WisconsinTraffic.Azure.WebApi.Models;

namespace WisconsinTraffic.Azure.WebApi.Utility
{
    // https://github.com/Azure/azure-documentdb-net/blob/master/tutorials/get-started/src/Program.cs

    public class TrafficDocumentProvider : IDisposable
    {
        private readonly DocumentClient _client;
        private Database _database;
        private DocumentCollection _collection;

        private const string DatabaseName = "witrafficdb";
        private const string DocumentCollectionName = "witrafficcoll";

        public DocumentClient Client
        {
            get { return _client; }
        }

        public string EndpointUrl
        {
            get { return Environment.GetEnvironmentVariable("APPSETTING_endpointurl"); }
        }

        public string AuthorizationKey
        {
            get { return Environment.GetEnvironmentVariable("APPSETTING_authorizationkey"); }
        }

        public TrafficDocumentProvider()
        {
            _client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
        }

        public async Task Init()
        {
            _database = await CreateDatabaseIfDoesntExist(DatabaseName);
            _collection = await CreateCollectionIfDoesntExist(DocumentCollectionName);
        }

        public async Task Save(object o)
        {
            dynamic newObj = o;
            string id = newObj.Id;
            Document doc = _client.CreateDocumentQuery<Document>(_collection.DocumentsLink).Where(d => d.Id == id).AsEnumerable().FirstOrDefault();

            if (doc == null)
            {
                await _client.CreateDocumentAsync(_collection.DocumentsLink, o);
            }
            else
            {
                await _client.ReplaceDocumentAsync(doc.SelfLink, o);
            }
        }

        public object Get(string name)
        {
            var test = _client.CreateDocumentQuery(_collection.DocumentsLink).AsEnumerable().ToList();
            Debug.WriteLine(test.Count);
            return _client.CreateDocumentQuery(_collection.DocumentsLink).Where(d => d.Id == name).AsEnumerable().FirstOrDefault();
        }
        
        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }

        private async Task<Database> CreateDatabaseIfDoesntExist(string name)
        {
            var database = _client.CreateDatabaseQuery().Where(db => db.Id == name).AsEnumerable().FirstOrDefault();
            if (database == null)
            {
                database = await _client.CreateDatabaseAsync(
                    new Database
                    {
                        Id = name
                    });
            }
            return database;
        }

        private async Task<DocumentCollection> CreateCollectionIfDoesntExist(string name)
        {
            var documentCollection = _client.CreateDocumentCollectionQuery(_database.CollectionsLink).Where(c => c.Id == name).AsEnumerable().FirstOrDefault();

            if (documentCollection == null)
            {
                documentCollection = await _client.CreateDocumentCollectionAsync(_database.CollectionsLink,
                    new DocumentCollection
                    {
                        Id = name
                    });
            }
            return documentCollection;
        }
    }
}