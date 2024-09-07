import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { SignalrService } from '../services/signalr.service'
import {ChatModel} from "../Interfaces/ChatModel";
import {MessageModel} from "../Interfaces/MessageModel";
import {throwError} from "rxjs";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public chats: ChatModel [] = [];
  public selectedChat: ChatModel |undefined;

  public username : string | undefined;
  newChatName: string | undefined;
  usernameSet: boolean = false;

  //public chatMessages : MessageModel[] = [];

  private _baseUrl : string;
  public newMessage: string = '';

  constructor(public SignalrService : SignalrService,
              private http: HttpClient,
              @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;//.replace("https", 'wss');
  }

  ngOnInit() {
    this.SignalrService.startConnection();
    this.SignalrService.addChatListener();
    this.SignalrService.addGetMessageListener();
  }

  setUsername() {
    this.usernameSet = true;
  }

  createChat() {
    this.http.post(this._baseUrl + 'api/chat', { Name: this.newChatName } )
      .subscribe((res) => {
        this.newChatName = '';
      })
  }

  selectChat(chat: ChatModel) {
    this.selectedChat = chat;
    this.SignalrService.cleanMessages();
    this.SignalrService.subscribeOnChat(chat.id);
  }


  sendMessage() {
    const message = {
      chatId: this.selectedChat?.id,
      from: this.username,
      body: this.newMessage
    }
    this.http.post(this._baseUrl + 'api/message',message)
      .subscribe((res) => {
        this.newMessage = '';
      });
  }
}
