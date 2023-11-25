﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }

    [BsonElement("Name")] public string Name { get; set; }

    public string Catalog { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }
}