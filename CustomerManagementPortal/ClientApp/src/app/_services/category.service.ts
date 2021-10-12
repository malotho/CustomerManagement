import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { Category} from 'src/app/_models';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private categorySubject: BehaviorSubject<Category>;
  public category: Observable<Category>;
  baseUrl: string;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private router: Router,
    private http: HttpClient
  ) {
    this.baseUrl = baseUrl +"api";
    this.categorySubject = new BehaviorSubject<Category>(JSON.parse(localStorage.getItem('user')));
    this.category = this.categorySubject.asObservable();
  }

  public get userValue(): Category {
    return this.categorySubject.value;
  }
    
  add(category: Category) {
    return this.http.post(`${this.baseUrl}/categories`, category);
  }

  getAll() {
    return this.http.get<Category[]>(`${this.baseUrl}/categories`);
  }

  getById(id: string) {
    return this.http.get<Category>(`${this.baseUrl}/categories/${id}`);
  }

  update(id, params) {
    console.log(params)
    return this.http.put(`${this.baseUrl}/categories/${id}`, params)
      .pipe(map(x => {
        // update stored user if the logged in user updated their own record
        if (id == this.userValue.id) {
          // update local storage
          const user = { ...this.userValue, ...params };
          localStorage.setItem('user', JSON.stringify(user));

          // publish updated user to subscribers
          this.categorySubject.next(user);
        }
        return x;
      }));
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/categories/${id}`)
      .pipe(map(x => {
        // auto logout if the logged in user deleted their own record
        if (id == this.userValue.id) {
          //this.logout();
        }
        return x;
      }));
  }
}
