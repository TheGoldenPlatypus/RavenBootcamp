using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace OmersBootcamp.Unit3.LESSON6
{
    /*
    UNIT 3 LESSON 6 EX 1.
    Notes:
        Data subscriptions -  Data subscriptions are consumed by clients, called subscription workers. In any given moment, only one worker can be connected to a data subscription. 
                              A worker connected to a data subscription receives a batch of documents and gets to process it. When it's done it informs the server about the 
                              progress and the server is ready to send the next batch.    
    
    */
    using static Console;
    using static Raven.Client.Constants;

    class Subscriber
    {
        static void Main(string[] args)
        {
            var subscriptionWorker = DocumentStoreHolder.Store
            .Subscriptions
            .GetSubscriptionWorker<Order>("Big Orders");

            // Documents that match a predefined criteria are sent in batches from the server to the client.
            var subscriptionRuntimeTask = subscriptionWorker.Run(batch =>
            {
                foreach (var order in batch.Items)
                {
                    // business logic here.
                    Console.WriteLine(order.Id);
                }
            });

            WriteLine("Press any key to exit...");
            ReadKey();
        }
    }
}
