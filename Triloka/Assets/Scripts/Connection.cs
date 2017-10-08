using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System;

public class Connection : MonoBehaviour {
	TcpClient client;
	Stream s;
	StreamReader sr;
	StreamWriter sw;
	Thread recvT;
    public bool ready = false;
    public bool running = true;

	string temp;
	// Use this for initialization
	void Start () {
        new Thread(initThread).Start();
	}
	
    public void initThread()
    {
        TcpClient client = new TcpClient("127.0.0.1", 4599);
        s = client.GetStream();
        sr = new StreamReader(s);
        sw = new StreamWriter(s);
        sw.AutoFlush = true;

        recvT = new Thread(recvThread);
        recvT.Start();

        ready = true;
    }

	public void send(string message){
		temp = message;
		new Thread (sendThread).Start ();
	}

	public void sendThread(){
		try{
			string name = temp;
			sw.WriteLine(name);
		}
		catch{
		}
	}

	public void recvThread(){
        print("RecvThread started..");
		while (running) {
            try {
                string message = sr.ReadLine();
                print("Recieved : " + message);
                WindowsVoice.theVoice.speak(message);
            }
            catch(Exception e)
            {
                print(e);
            }
        }
	}

    public void OnDestroy()
    {
        running = false;
    }
}
