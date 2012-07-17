﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using LapTimer.Infrastructure.Extensions;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace LapTimer.Data
{
    public class MongoRepository : IRepository
    {
        MongoDatabase db;

        public MongoRepository(string connectionString, string databaseName)
        {
            db = MongoServer.Create(connectionString).GetDatabase(databaseName);
        }

        public IEnumerable<TEntity> All<TEntity>() where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var entities = collection.FindAllAs<TEntity>();

            return entities;
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