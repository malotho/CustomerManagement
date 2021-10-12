import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from './layout.component';
import { CategoryComponent} from './category.component';
import { AddEditComponent } from './add-edit.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
          { path: '', component: CategoryComponent },
            { path: 'add', component: AddEditComponent },
            { path: 'edit/:id', component: AddEditComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoryRoutingModule { }
