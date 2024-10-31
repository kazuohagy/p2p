using System.Net.Sockets;
using System.Text;

public class Peer {
    public TopClient client;

    public string address;

    public List<string> Blockchain = new List<string>();

    public Peer(TopClient client, string address) {
        this.client = client;
        this.address = address;
    }

    public void Listen() {
        NetworkStream stream = client.GetStream();
        byte[] data = new byte[256];
        int byte_count;

        while ((byte_count = stream.Read(data, 0, data.Length)) != 0) {
            string message = Encoding.ASCII.GetString(data, 0, byte_count);
            Blockchain.Add(message);
        }
    }

    private void ProcessData(string data) {
        Blockchain.Add(data);
        if (data == "REquesting Blockchain"){
            SendData(string.Join(",", Blockchain));
        }
    }

    public void SendData(string data) {
        NetworkStream stream = client.GetStream();
        byte[] data_bytes = Encoding.ASCII.GetBytes(data);
        stream.Write(data_bytes, 0, data_bytes.Length);
        Console.WriteLine("Sent: " + data);
    }
}