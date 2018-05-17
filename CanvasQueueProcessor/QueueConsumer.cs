﻿using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.IO;
using System.Configuration;
using CanvasQueueProcessor.Domain.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using CanvasQueueProcessor.Service;

namespace CanvasQueueProcessor
{
    public class QueueConsumerExample
    {

        string queueName = ConfigurationSettings.AppSettings.Get("queueName");
        string userName = ConfigurationSettings.AppSettings.Get("userName");
        string password = ConfigurationSettings.AppSettings.Get("password");
        string hostName = ConfigurationSettings.AppSettings.Get("hostName");
        string virtualHost = ConfigurationSettings.AppSettings.Get("virtualHost");
        string fileToGenerate = ConfigurationSettings.AppSettings.Get("fileToGenerate");
        int port = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("port"));

        public static void Main(string[] args)
        {
            /* PRODUCTIVE */
            
            QueueConsumerExample qConsumer = new QueueConsumerExample();
            //qConsumer.GetMessage();
            qConsumer.Register();
            
            /* TESTING (local JSON) */
            //using (StreamReader r = new StreamReader("../../Domain/TestingJSON/entry_list_full.json"))
            //{
            //    string jsonString = r.ReadToEnd();
            //    EntryDTO jsonObject = JsonConvert.DeserializeObject<EntryDTO>(jsonString);
            //
            //    EntryService.CreateNotaEntry(jsonObject.entries);
            //}
        }

        public void Register()
        {
            //Setup the connection with the message broker

            //////////////////
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            /////////////////

            ConnectionFactory factory = new ConnectionFactory();
            IProtocol protocol = Protocols.AMQP_0_9_1;
            factory.VirtualHost = virtualHost;
            factory.UserName = userName;
            factory.Password = password;
            factory.HostName = hostName;
            factory.Port = port;
            factory.Protocol = protocol;
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();

            uint messageCount = GetMessageCount(factory, queueName);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body;
                string message = Encoding.UTF8.GetString(body);
                EntryDTO result = JsonConvert.DeserializeObject<EntryDTO>(message);

                EntryService.CreateAuditoria(result.entries);

                if (EntryService.CreateNotaEntry(result.entries))
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            };

            String consumerTag = channel.BasicConsume(queueName, false, consumer);
        }
        
        public uint GetMessageCount(ConnectionFactory factory, string queueName)
        {
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                return channel.MessageCount(queueName);
            }
        }
    }
}
