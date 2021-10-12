import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';


import { ProductService } from 'src/app/_services/';
import { Product } from '../_models';

@Component({ templateUrl: 'product-list.component.html' })
export class ProductListComponent implements OnInit {

  public mainProductsSource: Product[];
  products = null;
  page = 1;
  pageSize = 10;
  collectionSize: number;

  constructor(private productService: ProductService) {
  }

  refresh() {
    this.products = this.mainProductsSource
      .map((p, i) => ({ id: i + 1, ...p }))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  ngOnInit() {

    this.productService.getAll()
      .pipe(first())
      .subscribe(products => {
        this.mainProductsSource = products;
        this.collectionSize = this.mainProductsSource.length;
        this.refresh();
      });
  }

  deleteProduct(id: number) {
    const product = this.products.find(x => x.id === id);
    product.isDeleting = true;
    this.productService.delete(id)
      .pipe(first())
      .subscribe(() => {
        this.products = this.products.filter(x => x.id !== id)
      });
  }
}
