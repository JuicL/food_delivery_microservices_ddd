﻿global using System.Net.Sockets;
global using System.Text;
global using System.Text.Json;
global using FoodDelivery.EventBus.Abstractions;
global using FoodDelivery.EventBus.Events;
global using Microsoft.Extensions.Logging;
global using Polly;
global using RabbitMQ.Client;
global using RabbitMQ.Client.Events;
global using RabbitMQ.Client.Exceptions;
