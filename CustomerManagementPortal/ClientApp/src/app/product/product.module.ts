import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

import { ProductRoutingModule } from './product-routing.module';
import { LayoutComponent } from './layout.component';
import { ProductListComponent } from './product-list.component';
import { AddEditComponent } from './add-edit.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ProductRoutingModule,
    NgbPaginationModule
  ],
  declarations: [
    LayoutComponent,
    ProductListComponent,
    AddEditComponent
  ]
})
export class ProductModule { }
