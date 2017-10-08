from fbchat import Client
from fbchat.models import *


class wysaBot:
    def __init__(self):
        with open("mseal", "rb") as f:
            mseal = f.read().decode()
        f.close()
        self.client = Client("raghav0296@gmail.com", password=mseal)
        self.thread_id = self.client.searchForPages("Wysa - happiness chatbot therapy")[0].uid
        self.prev_msg = ""
        self.retrieve_messages()

    def send_message(self, message):
        self.client.sendMessage(message, thread_id=self.thread_id, thread_type=ThreadType.USER)
        self.prev_msg = message

    def retrieve_messages(self):
        all_messages = self.client.fetchThreadMessages(thread_id=self.thread_id, limit=10)
        message_texts = []
        for i in all_messages:
            if i.text == self.prev_msg:
                break
            i.text = self.filter_messages(i.text)
            message_texts.append(i.text)
        self.prev_msg = all_messages[0].text
        message_texts = str.join("", message_texts[::-1])
        return message_texts

    def filter_messages(self, message):
        message = str(message)
        message.replace("Raghav","")
        message.replace("Please choose an option from below.","")
        if "Here are some commands I understand" in message:
            message = ""
        if "Say #MOOD or #ANGRY" in message:
            message = ""
        return message
