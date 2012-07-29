using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using LapTimer.Infrastructure.Extensions;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using LapTimer.Infrastructure;

namespace LapTimer.Data
{
    public class MongoRepository : IRepository
    {
        MongoDatabase db;

        public MongoRepository(string connectionString)
        {
            db = MongoDatabase.Create(connectionString);
        }

        public IEnumerable<TEntity> All<TEntity>() where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var entities = collection.FindAllAs<TEntity>();

            return entities;
        }

        public TEntity Single<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = Query.EQ("_id", GetId(key));
            var entity = collection.FindOneAs<TEntity>(query);

            if (entity == null)
                throw new NullReferenceException("Document with key '" + key + "' not found.");

            return entity;
        }

        public TEntity Single<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var entity = collection.AsQueryable().Where(predicate);

            return entity.Single();
        }

        public IEnumerable<TEntity> Find<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var entities = collection.AsQueryable().Where(predicate);

            return entities;
        }

        public void Save<TEntity>(TEntity item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();

            collection.Save(item);
        }

        public void Delete<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = Query.EQ("_id", GetId(key));

            collection.Remove(query);
        }

        private MongoCollection<TEntity> GetCollection<TEntity>()
        {
            var collectionName = typeof(TEntity).Name.ToLower().Pluralize();

            return db.GetCollection<TEntity>(collectionName);
        }

        private ObjectId GetId(object key)
        {
            ObjectId objectId;
            var byteId = key as byte[];

            if (byteId != null)
                objectId = new ObjectId(byteId);
            else
                objectId = new ObjectId(key.ToString());

            return objectId;
        }
    }
}