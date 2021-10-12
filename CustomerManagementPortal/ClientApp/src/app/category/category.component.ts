import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { CategoryService } from 'src/app/_services/';
import { Category } from '../_models';


@Component({
  selector: 'category',
  templateUrl: './category.component.html'
})
export class CategoryComponent implements OnInit {

  public mainCategorySource: Category[];
  categories = null;
  page = 1;
  pageSize = 10;
  collectionSize: number;

  constructor(private categoryService: CategoryService) {
  }

  refresh() {
    this.categories = this.mainCategorySource
      .map((p, i) => ({ id: i + 1, ...p }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  ngOnInit() {

    this.categoryService.getAll()
      .pipe(first())
      .subscribe(categories => {
        this.mainCategorySource = categories;
        this.collectionSize = this.mainCategorySource.length;
        this.refresh();
      });
  }

  deleteCategory(id: number) {
    const category = this.categories.find(x => x.id === id);
    category.isDeleting = true;
    this.categoryService.delete(id)
      .pipe(first())
      .subscribe(() => {
        this.categories = this.categories.filter(x => x.id !== id)
      });
  }
}
