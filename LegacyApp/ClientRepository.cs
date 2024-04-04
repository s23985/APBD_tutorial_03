﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp;

public class ClientRepository
{
    /// <summary>
    /// This collection is used to simulate remote database
    /// </summary>
    public static readonly Dictionary<int, Client> Database = new()
    {
        {1, new Client{Id = 1, Name = "Kowalski", Address = "Warszawa, Złota 12", Email = "kowalski@wp.pl", Type = ClientType.NormalClient}},
        {2, new Client{Id = 2, Name = "Malewski", Address = "Warszawa, Koszykowa 86", Email = "malewski@gmail.pl", Type = ClientType.VeryImportantClient}},
        {3, new Client{Id = 3, Name = "Smith", Address = "Warszawa, Kolorowa 22", Email = "smith@gmail.pl", Type = ClientType.ImportantClient}},
        {4, new Client{Id = 4, Name = "Doe", Address = "Warszawa, Koszykowa 32", Email = "doe@gmail.pl", Type = ClientType.ImportantClient}},
        {5, new Client{Id = 5, Name = "Kwiatkowski", Address = "Warszawa, Złota 52", Email = "kwiatkowski@wp.pl", Type = ClientType.NormalClient}},
        {6, new Client{Id = 6, Name = "Andrzejewicz", Address = "Warszawa, Koszykowa 52", Email = "andrzejewicz@wp.pl", Type = ClientType.NormalClient}}
    };
        
    public ClientRepository()
    {
    }

    /// <summary>
    /// Simulating fetching a client from remote database
    /// </summary>
    /// <returns>Returning client object</returns>
    internal Client GetById(int clientId)
    {
        var randomWaitTime = new Random().Next(2000);
        Thread.Sleep(randomWaitTime);

        if (Database.TryGetValue(clientId, out var value))
            return value;

        throw new ArgumentException($"User with id {clientId} does not exist in database");
    }
}