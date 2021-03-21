using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Blog;

namespace Homework5
{
    class Server
    {
        static int localPort;
        static Socket listeningSocket;
        static List<IPEndPoint> clients = new List<IPEndPoint>();
        static ConcurrentQueue<(string, IPEndPoint)> queue = new ConcurrentQueue<(string, IPEndPoint)>();
        static Context context = new Context();
        static bool flager = true;
        static async Task Main(string[] args)
        {
            Console.WriteLine("SERVER");
            localPort = Int32.Parse("8080");
            Console.WriteLine();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.SaveChanges();
            try
            {
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                Task runqueueTask = RequestsAsync();
                Task broadcastTask = BroadcastAsync();
                Console.WriteLine("EXIT INPUT");
                string a = Console.ReadLine();
                if (a == "exit")
                {
                    flager = false;
                    context.Dispose();
                    Close();
                }
                await runqueueTask;
                await broadcastTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        private static void Listen()
        {
            try
            {
                IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), localPort);
                listeningSocket.Bind(localIP);

                while (flager)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];
                    EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);
                    do
                    {
                        bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (listeningSocket.Available > 0);
                    IPEndPoint remoteFullIp = remoteIp as IPEndPoint;
                    Console.WriteLine("{0}:{1} - {2}", remoteFullIp.Address.ToString(), remoteFullIp.Port, builder.ToString()); // выводим сообщение
                    queue.Enqueue((builder.ToString(), remoteFullIp));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void BroadcastMessage()
        {
            IPEndPoint client = null;
            string message = null;
            string resultdata = "SELECT RESULT";
            (string, IPEndPoint) result = new (message, client);
            while (flager)
            {
                if (queue.TryDequeue(out result))
                {
                    if (result.Item1 == "1")
                    {
                        
                        Console.WriteLine(result);
                        IQueryable<PersonalMessages> users_messages = from usersMessage in context.UsersMessages
                            select usersMessage;
                        List <PersonalMessages> list_1 = users_messages.ToList();
                        foreach (PersonalMessages usersMessage in users_messages)
                        {
                            resultdata = resultdata + usersMessage.Id.ToString() + "\n";
                        }
                    }
                    else if (result.Item1 == "2")
                    {
                        
                        Console.WriteLine(result);
                        IQueryable<Post> posts = from post in context.Posts
                            select post;
                        List <Post> list_2 = posts.ToList();
                        foreach (Post post in posts)
                        {
                            resultdata = resultdata + post.Id.ToString() + "\n";
                        }
                    }
                    byte[] data = Encoding.Unicode.GetBytes(resultdata);
                    listeningSocket.SendTo(data, result.Item2);
                    resultdata = "SELECT RESULT";
                }
            }
        }
        
        static async Task RequestsAsync()
        {
            await Task.Run(() => Listen());
        }
        
        static async Task BroadcastAsync()
        {
            await Task.Run(() => BroadcastMessage());
        }
        private static void Close()
        {
            if (listeningSocket != null)
            {
                listeningSocket.Shutdown(SocketShutdown.Both);
                listeningSocket.Close();
                listeningSocket = null;
            }

            Console.WriteLine("Сервер остановлен!");
        }
    }

    
}