using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.IO;

namespace CanvasQueueProcessor
{
    class TopicConsumer
    {
        private const string EXCHANGE_NAME = "amq.topic";

        private static void pMain(string[] args)
        {
            string topicName = "ilumno.lms.grades.exchange";

            ConnectionFactory factory = new ConnectionFactory();
            factory.VirtualHost = "/";
            factory.UserName = "ilumno_guest";
            factory.Password = "Ilumno2017";
            factory.HostName = "10.70.51.31";
            factory.Port = 5672;

            IConnection conn = factory.CreateConnection();

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {

                    channel.ExchangeDeclare(topicName, "topic", true, false);

                    string queueName = "ilumno.lms.grades";

                    channel.QueueBind(queueName, topicName, queueName);

                    Console.WriteLine("Waiting for messages");

                    QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, true, consumer);


                    while (true)
                    {
                        BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        Console.WriteLine(Encoding.ASCII.GetString(e.Body));

                    }
                }
            }
        }
    }
}
