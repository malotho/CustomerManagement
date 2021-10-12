import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule,  } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_helpers';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { AlertComponent } from './_components';


const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const usersModule = () => import('./users/users.module').then(x => x.UsersModule);
const productModule = () => import('./product/product.module').then(x => x.ProductModule);
const categoryModule = () => import('./category/category.module').then(x => x.CategoryModule);

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AlertComponent    
  ],
  imports: [
    NgbModule,
    ReactiveFormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'users', loadChildren: usersModule, canActivate: [AuthGuard] },
      { path: 'product', loadChildren: productModule, canActivate: [AuthGuard] },
      { path: 'category', loadChildren: categoryModule, canActivate: [AuthGuard] },
      { path: 'account', loadChildren: accountModule },
      { path: '**', redirectTo: '' }
    ], { relativeLinkResolution: 'legacy' })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
