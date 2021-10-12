import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { User } from 'src/app/_models';

@Injectable({ providedIn: 'root' })
export class AccountService {
  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  baseUrl: string;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private router: Router,
    private http: HttpClient
  ) {
    this.baseUrl = baseUrl +"api";
    this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
    this.user = this.userSubject.asObservable();
  }

  public get userValue(): User {
    return this.userSubject.value;
  }

  login(username, password) {
    return this.http.post<User>(`${this.baseUrl}/users/authenticate`, { username, password })
      .pipe(map(user => {
        console.log(user);
        localStorage.setItem('user', JSON.stringify(user));
        this.userSubject.next(user);
        return user;
      }));
  }

  logout() {    
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/account/login']);
  }

  register(user: User) {
    return this.http.post(`${this.baseUrl}/users`, user);
  }

  getPagedUsers(pageNo, usersPerPaage) {
    return this.http.get<User[]>(`${this.baseUrl}/Users/${pageNo}/${usersPerPaage}`);
  }

  getAllUsersCount() {
    return this.http.get(`${this.baseUrl}/Users/UserCount`);
  }
  getAll() {
    return this.http.get<User[]>(`${this.baseUrl}/users`);
  }

  getById(id: string) {
    return this.http.get<User>(`${this.baseUrl}/users/${id}`);
  }

  update(id, params) {
    return this.http.put(`${this.baseUrl}/users/${id}`, params)
      .pipe(map(x => {
        // update stored user if the logged in user updated their own record
        if (id == this.userValue.id) {
          // update local storage
          const user = { ...this.userValue, ...params };
          localStorage.setItem('user', JSON.stringify(user));

          // publish updated user to subscribers
          this.userSubject.next(user);
        }
        return x;
      }));
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/users/${id}`)
      .pipe(map(x => {
        // auto logout if the logged in user deleted their own record
        if (id == this.userValue.id) {
          this.logout();
        }
        return x;
      }));
  }
}
