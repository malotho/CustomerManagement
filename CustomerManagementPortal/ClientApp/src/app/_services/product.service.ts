import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { Product } from 'src/app/_models';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private productSubject: BehaviorSubject<Product>;
  public product: Observable<Product>;
  baseUrl: string;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private router: Router,
    private http: HttpClient
  ) {
    this.baseUrl = baseUrl +"api";
    this.productSubject = new BehaviorSubject<Product>(JSON.parse(localStorage.getItem('user')));
    this.product = this.productSubject.asObservable();
  }

  public get userValue(): Product {
    return this.productSubject.value;
  }
    
  add(product: Product) {
    return this.http.post(`${this.baseUrl}/products`, product);
  }

  getAll() {
    return this.http.get<Product[]>(`${this.baseUrl}/products`);
  }

  getById(id: string) {
    return this.http.get<Product>(`${this.baseUrl}/products/${id}`);
  }

  update(id, params) {
    console.log(params)
    return this.http.put(`${this.baseUrl}/products/${id}`, params)
      .pipe(map(x => {
        // update stored user if the logged in user updated their own record
        if (id == this.userValue.id) {
          // update local storage
          const user = { ...this.userValue, ...params };
          localStorage.setItem('user', JSON.stringify(user));

          // publish updated user to subscribers
          this.productSubject.next(user);
        }
        return x;
      }));
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/products/${id}`)
      .pipe(map(x => {
        // auto logout if the logged in user deleted their own record
        if (id == this.userValue.id) {
          //this.logout();
        }
        return x;
      }));
  }
}
