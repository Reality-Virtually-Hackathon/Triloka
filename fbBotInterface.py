import socket
import wysabot
import _thread
import time

wysa = wysabot.wysaBot()
socket_connection = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
socket_connection.bind(('18.111.9.226', 4599))
message = 'This is the message.'
socket_connection.listen(1)
client_connection, client_address = socket_connection.accept()


def receive_from_wysa():
    while True:
        try:
            time.sleep(0.5)
            messages = wysa.retrieve_messages()
            if len(messages)!=0:
                print("Sending to client: ", messages)
                messages = messages + "\n\r"
                client_connection.send(messages.encode())
        except:
            client_connection.close()


_thread.start_new_thread(receive_from_wysa, ())

while True:
    try:
        print("Receiving from ", client_address)
        data = client_connection.recv(1028)
        if data:
            data = data.decode().rstrip("\n\r")
            print("Received from client: ", data, ", length: ", len(data))
            if len(data)!=0:
                wysa.send_message(data)
        else:
            break

    finally:
        pass
