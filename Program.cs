// Урок 2. Синхронизации: многопоточность, создание и завершение потоков
// Добавьте возможность ввести слово Exit в чате клиента, чтобы можно было завершить его работу. 
// В коде сервера добавьте ожидание нажатия клавиши, чтобы также прекратить его работу.

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 8888);
        server.Start();
        
        Console.WriteLine("Server started. Waiting for clients...");

        TcpClient client = server.AcceptTcpClient();
        NetworkStream stream = client.GetStream();
        byte[] data = new byte[256];
        int bytes;

        while (true)
        {
            bytes = stream.Read(data, 0, data.Length);
            string message = Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine("Client: " + message);

            if (message.ToLower() == "exit")
            {
                Console.WriteLine("Client disconnected.");
                break;
            }

            Console.Write("Server: ");
            string response = Console.ReadLine();
            data = Encoding.UTF8.GetBytes(response);
            stream.Write(data, 0, data.Length);
        }

        server.Stop();
        Console.WriteLine("Server stopped. Press any key to exit...");
        Console.ReadKey();
    }
}
 

// В данном коде мы создали сервер, который принимает клиентское подключение, обрабатывает введенные сообщения и отправляет ответ обратно. 
// Если клиент вводит слово "exit", сервер закрывает соединение. 
// После остановки сервера ожидается нажатие клавиши для завершения работы программы.