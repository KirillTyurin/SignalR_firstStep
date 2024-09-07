import {Inject, Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { ChatModel } from "../Interfaces/ChatModel"
import { MessageModel } from "../Interfaces/MessageModel"

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection?: signalR.HubConnection | undefined;
  private _baseUrl : string;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;//.replace('https', 'wss');
  }

  public chats : ChatModel[] = [];
  public messages: MessageModel[] = []

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this._baseUrl + 'chatHub',
        {
          // skipNegotiation: true,
          // transport: signalR.HttpTransportType.WebSockets
        })
      .configureLogging(signalR.LogLevel.Debug)
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addGetMessageListener = () => {
    this.hubConnection?.on('GetMessage', (message) => {
      this.messages.push(message);
      console.log(message);
    })
  }

  public subscribeOnChat (chatId : string) {
    const connection = { chatId : chatId }
    this.hubConnection?.invoke('joinToChat', connection);
  }

  public addChatListener = () => {
    this.hubConnection?.on('addchat', (chat) => {
      this.chats.push(chat);
      console.log(chat)
    })
  }

  public cleanMessages() {
    this.messages = [];
  }
}
