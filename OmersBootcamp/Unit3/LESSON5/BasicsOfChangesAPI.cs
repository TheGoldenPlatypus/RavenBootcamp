using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OmersBootcamp.Unit3.LESSON5
{
    /*
    UNIT 3 LESSON 5 EX 1.
    Notes:
        Changes API - a feature that allows us to receive messages from the server about the events occurred there.
                      Using the Changes API, you will get notified by the server whenever an event you are interested is fired without polling.
                      ( 'polling' is the continuous checking of other programs by one program to see what state they are in)
        Reactive Programming -  reactive programming describes a design paradigm that relies on asynchronous programming. 
                                consider the following example:

                                var b = 1                            |          var b = 1
                                var c = 2                            |          var c = 2
                                var a = b + c                        |          var a $= b + c // '$=' is an imaginary operator that changes the value of a variable
                                b = 10                               |                         // not only when explicitly initialized, but also when referenced variables
                                console.log(a) // 3                  |                         // are changed
                                                                     |          b = 10
                                                                     |          console.log(a) // 12

    */
    using static Console;

    class BasicsOfChangesAPI
    {
        static void Main(string[] args)
        {
            // Now every time something changes a document in the server, your application will get notified.
            // note that the change notification include the document (or index) ID and the type of the operation performed.
            var subscription = DocumentStoreHolder.Store
                .Changes()
                .ForAllDocuments()
                .Subscribe(change =>
                    WriteLine($"{change.Type} on document {change.Id}"));

            WriteLine("Press any key to exit...");
            ReadKey();

            subscription.Dispose();
        }
    }
}
