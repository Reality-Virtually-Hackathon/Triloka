import socket

socket_connection = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
socket_connection.bind(('localhost',4589))
message = 'This is the message.'
socket_connection.listen(1)
while True:
    client_connection, client_address = socket_connection.accept()
    try:
        print("Receiving from ", client_address)
        data = client_connection.recv(16)
        if data:
            print(data)
        else:
            break
    finally:
        client_connection.close()

