using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.multiplayer
{
    public class Client
    {
        HubConnection _connection;
        public void Connection()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/session")
                .Build();
            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }
    }
}
