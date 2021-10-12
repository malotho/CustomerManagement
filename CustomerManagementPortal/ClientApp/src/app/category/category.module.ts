import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

import { CategoryRoutingModule } from './category-routing.module';
import { LayoutComponent } from './layout.component';
import { CategoryComponent } from './category.component';
import { AddEditComponent } from './add-edit.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CategoryRoutingModule,
    NgbPaginationModule
  ],
  declarations: [
    LayoutComponent,
    CategoryComponent,
    AddEditComponent
  ]
})
export class CategoryModule { }
