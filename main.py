import asyncio
import socket

async def receive_message(sock):
    loop = asyncio.get_running_loop()
    while True:
        data, server = await loop.run_in_executor(None, sock.recvfrom, 1024)  # Buffer size is 1024 bytes
        print(f"Received from {server}: {data.decode('utf-8')}")

async def send_message(sock, server_address):
    loop = asyncio.get_running_loop()
    while True:
        message = await loop.run_in_executor(None, input, "input: ")
        if message.lower() == 'exit':
            break
        sock.sendto(message.encode('utf-8'), server_address)
        print(f"Sent: {message}")

async def main():
    local_address = ("127.0.0.1", 54125)  # 本地监听的地址和端口
    remote_address = ("127.0.0.1", 54123)  # 发送的目标地址和端口

    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.bind(local_address)

    await asyncio.gather(
        receive_message(sock),
        send_message(sock, remote_address)
    )

if __name__ == "__main__":
    asyncio.run(main())