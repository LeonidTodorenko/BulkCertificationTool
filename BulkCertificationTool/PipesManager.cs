using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace BulkCertificationTool
{
    class PipesManager : IDisposable
    {
        /// <summary>
        /// Create instance for pipe server
        /// </summary>
        /// <param name="pipeName">Name of pipe</param>
        /// <param name="fileNames">filenames collection</param>
        public PipesManager(string pipeName)
        {
            _pipeName = pipeName;
        }

        /// <summary>
        /// Start pipe server to get messages from client
        /// </summary>
        /// <param name="fileNames">Collection of filenames received from clients</param>
        public void StartServer(ConcurrentStack<string> fileNames)
        {
            _fileNames = fileNames;

            try
            {
                //create pipe
                server = new NamedPipeServerStream(_pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
                server.BeginWaitForConnection(new AsyncCallback(WaitForConnectionCallBack), server);
            }
            catch { }
        }

        /// <summary>
        /// Create pipe client and send message to server
        /// </summary>
        /// <param name="message"></param>
        public void SendClientMessage(string message)
        {
            using (var pipe = new NamedPipeClientStream(".", _pipeName,
                PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.Impersonation))
            {
                pipe.Connect();

                using (StreamWriter writer = new StreamWriter(pipe))
                {
                    writer.WriteLine(message);
                    writer.Flush();
                }
            }
        }

        private void WaitForConnectionCallBack(IAsyncResult iar)
        {
            try
            {
                // Get the pipe
                NamedPipeServerStream pipeServer = (NamedPipeServerStream)iar.AsyncState;
                // End waiting for the connection
                pipeServer.EndWaitForConnection(iar);

                //while there is no stop command - write data
                if(!_stop)
                {
                    using (StreamReader reader = new StreamReader(pipeServer))
                    {
                        var fileName = reader.ReadLine();
                        _fileNames.Push(fileName);
                    }
                }

                // Kill original sever and create new wait server
                pipeServer.Close();
                pipeServer = null;

                //if there is no stop command - start new server
                if(!_stop)
                {
                    pipeServer = new NamedPipeServerStream(_pipeName, PipeDirection.InOut,
                       1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);

                    // Recursively wait for the connection again and again....
                    pipeServer.BeginWaitForConnection(
                       new AsyncCallback(WaitForConnectionCallBack), pipeServer);
                }
            }
            catch  { }
        }

        public void Dispose()
        {
            if(server != null)
            {
                if(!server.IsConnected)
                {
                    _stop = true;
                    ConnectSelf();  
                }

                server.Close();
                server = null;
            }
        }

        private void ConnectSelf()
        {
            using (var pipe = new NamedPipeClientStream(".", _pipeName,
                PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.Impersonation))
            {
                pipe.Connect();
            }
        }

        private bool _stop = false;
        private readonly string _pipeName;
        private ConcurrentStack<string> _fileNames;
        private NamedPipeServerStream server;
    }
}
