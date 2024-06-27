import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { User } from '../_models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
//provide http service
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:7070/api/';
  currentUser = signal<User | null>(null);

  login(model:any) {
    return this.http.post<User>(this.baseUrl+'Account/login',model).pipe(
      map(user => {
        if (user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUser.set(user)
        }
          return user;
      })
    )

  }

  register(model:any) {
    return this.http.post<User>(this.baseUrl+'Account/register',model).pipe(
      map(user => {
        if (user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUser.set(user);

        }
        return user;
      })
    )

  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }

}
