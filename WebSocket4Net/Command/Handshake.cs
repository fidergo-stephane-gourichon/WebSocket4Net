using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WebSocket4Net.Command
{
    public class Handshake : WebSocketCommandBase
    {
        public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
        {
            string description;
            Debug.WriteLine("ExecuteCommand: key=" + commandInfo.Key + " text=[" + commandInfo.Text + "]");
            if (!session.ProtocolProcessor.VerifyHandshake(session, commandInfo, out description))
            {
                session.FireError(new Exception(description));
                session.Close(session.ProtocolProcessor.CloseStatusCode.ProtocolError, description);
                return;
            }

            session.OnHandshaked();
        }

        public override string Name
        {
            get { return OpCode.Handshake.ToString(); }
        }
    }
}
