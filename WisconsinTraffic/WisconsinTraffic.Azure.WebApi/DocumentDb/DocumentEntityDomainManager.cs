﻿using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.WindowsAzure.Mobile.Service;

namespace WisconsinTraffic.Azure.WebApi.DocumentDb
{
    public class DocumentEntityDomainManager<TDocument> where TDocument : Resource
    {
        public ApiServices Services { get; set; }
        
        private string _collectionId;
        private string _databaseId;
        private Database _database;
        private DocumentCollection _collection;
        private DocumentClient _client;

        public DocumentEntityDomainManager(string databaseId, string collectionId, ApiServices services)
        {
            Services = services;
            _collectionId = collectionId;
            _databaseId = databaseId;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var doc = GetDocument(id);


                if (doc == null)
                {
                    return false;
                }

                await Client.DeleteDocumentAsync(doc.SelfLink);

                return true;


            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public bool Exists(string id)
        {
            try
            {
                var doc = GetDocument(id);

                return doc != null;
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Document> InsertAsync(TDocument data)
        {
            try
            {
                return await Client.CreateDocumentAsync(Collection.SelfLink, data);
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public SingleResult<TDocument> Lookup(string id)
        {
            try
            {
                return SingleResult.Create<TDocument>(
                    Client.CreateDocumentQuery<TDocument>(Collection.DocumentsLink)
                    .Where(d => d.Id == id)
                    .Select<TDocument, TDocument>(d => d)
                    );
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public IQueryable<TDocument> Query()
        {
            try
            {
                return Client.CreateDocumentQuery<TDocument>(Collection.DocumentsLink);
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> ReplaceAsync(string id, TDocument item)
        {

            if (item == null || id != item.Id)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            try
            {
                var doc = GetDocument(id);

                if (doc == null)
                {
                    return false;
                }

                await Client.ReplaceDocumentAsync(doc.SelfLink, item);

                return true;
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        private Document GetDocument(string id)
        {
            return Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                        .Where(d => d.Id == id)
                        .AsEnumerable()
                        .FirstOrDefault();
        }

        #region DocumentDBClient

        private DocumentClient Client
        {
            get
            {
                if (_client == null)
                {
                    string endpoint = ConfigurationManager.AppSettings["WITrafficDocumentDbUrl"];
                    string authKey = ConfigurationManager.AppSettings["WITrafficDocumentAuthKey"];
                    Uri endpointUri = new Uri(endpoint);
                    _client = new DocumentClient(endpointUri, authKey);
                }

                return _client;
            }
        }

        private DocumentCollection Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = ReadOrCreateCollection(Database.SelfLink);
                }

                return _collection;
            }
        }

        private Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = ReadOrCreateDatabase();
                }

                return _database;
            }
        }

        private DocumentCollection ReadOrCreateCollection(string databaseLink)
        {
            var col = Client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(c => c.Id == _collectionId)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (col == null)
            {
                col = Client.CreateDocumentCollectionAsync(databaseLink, new DocumentCollection { Id = _collectionId }).Result;
            }

            return col;
        }

        private Database ReadOrCreateDatabase()
        {
            var db = Client.CreateDatabaseQuery()
                            .Where(d => d.Id == _databaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            if (db == null)
            {
                db = Client.CreateDatabaseAsync(new Database { Id = _databaseId }).Result;
            }

            return db;
        }
        #endregion

    }
}
