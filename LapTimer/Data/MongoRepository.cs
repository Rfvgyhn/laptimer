using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using LapTimer.Infrastructure.Extensions;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace LapTimer.Data
{
    public class MongoRepository : IRepository
    {
        MongoDatabase db;

        public MongoRepository(string connectionString, string databaseName)
        {
            db = MongoServer.Create(connectionString).GetDatabase(databaseName);
        }

        public TEntity Single<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = Query.EQ("_id", BsonValue.Create(key));
            var entity = collection.FindOneAs<TEntity>(query);

            if (entity == null)
                throw new NullReferenceException("Document with key '" + key + "' not found.");

            return entity;
        }

        public IEnumerable<TEntity> All<TEntity>() where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var entity = collection.FindAllAs<TEntity>();

            return entity;
        }

        public bool Exists<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = Query.EQ("_id", BsonValue.Create(key));
            var entity = collection.FindOneAs<TEntity>(query);

            return entity != null;
        }

        public void Save<TEntity>(TEntity item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();

            collection.Save(item);
        }

        public void Delete<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = Query.EQ("_id", BsonValue.Create(key));

            collection.Remove(query);
        }

        private MongoCollection<TEntity> GetCollection<TEntity>()
        {
            var collectionName = typeof(TEntity).Name.ToLower().Pluralize();

            return db.GetCollection<TEntity>(collectionName);
        }
    }
}