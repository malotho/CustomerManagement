import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { ProductService, AlertService, CategoryService } from 'src/app/_services';
import { Category } from '../_models';

@Component({ templateUrl: 'add-edit.component.html' })
export class AddEditComponent implements OnInit {
  form: FormGroup;

  categories = null;
  selectedCategory: Category;

  id: string;
  isAddMode: boolean;
  loading = false;
  submitted = false;

  url: any

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private categoryService: CategoryService,
    private alertService: AlertService
  ) { }

  selectFile(event: any) { //Angular 11, for stricter type
    if (!event.target.files[0] || event.target.files[0].length == 0) {
      return;
    }
    console.log(event.target.files[0])
    
    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);

    reader.onload = (_event) => {
      this.url = reader.result;
      this.form.get('image').setValue(this.url);
    }
  }

  onCategoryChanged(event: any) {
    this.selectedCategory = this.categories.find(n => n.categoryName == event.target.value);
    this.setFormControlValues(this.selectedCategory.id);
  }

  setFormControlValues(catId: number) {
    if (catId == 0) {
      this.form.get('category').setValue("Select Category");
    }
    else {
      this.selectedCategory = this.categories.find(i => i.id == catId);
      this.form.get('category').setValue(this.selectedCategory.categoryName);
    }
  }

  ngOnInit() {

    this.categoryService.getAll()
      .pipe(first())
      .subscribe(categories => this.categories = categories);

    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;

    this.form = this.formBuilder.group({
      id: 0,
      productName: ['', Validators.required],
      productCode: [''],
      description: [''],
      category: ['', Validators.required],
      price: ['', Validators.required],
      image: [''],
    });

    if (!this.isAddMode) {
      this.productService.getById(this.id)
        .pipe(first())
        .subscribe(x => {
          this.f.id.setValue(x.id);
          this.f.productName.setValue(x.productName);
          this.f.productCode.setValue(x.productCode);
          this.f.description.setValue(x.description);
          this.f.category.setValue(x.category.categoryName);
          this.f.price.setValue(x.price);
          this.f.image.setValue(x.image);
          this.f.image.setValue(x.image);
        });
    }
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {

    console.log(this.form)
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.form.get('category').setValue(this.selectedCategory);
    if (this.isAddMode) {
      this.createProduct();
    } else {
      this.updateUser();
    }
  }

  private createProduct() {
    this.productService.add(this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Product added!', { keepAfterRouteChange: true });
          this.router.navigate(['.', { relativeTo: this.route }]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

  private updateUser() {
    console.log(this.form.value)
    this.productService.update(this.id, this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Update successful', { keepAfterRouteChange: true });
          this.router.navigate(['..', { relativeTo: this.route }]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }
}
